using System.Diagnostics;
using System.Reflection;

using MyNUnit.Atributes;

namespace MyNUnit;

public class ApplicationForTests
{
    public readonly List<ResultsTests> listOfResults = new();
    private readonly object locker = new();

    public ApplicationForTests(Assembly assembly)
    {
        var classes = assembly.GetExportedTypes()
                              .Where(t => t.IsClass)
                              .Where(t => t.GetMethods()
                              .Where(m => m.GetCustomAttributes(true)
                              .Any(a => a is TestAttribute))
                              .Any());

        Parallel.ForEach(classes, StartTests);
    }

    private static MethodInfo[]? GetMethodsByAtributeAndClass(Type _class, Type atribute)
    {
        return _class.GetMethods()
                     .Where(m => m.GetCustomAttributes(atribute, false).Length > 0)
                     .ToArray();
    }

    private static void WorkWithClassMethods(Type _class, Type atribute)
    {
        var methods = GetMethodsByAtributeAndClass(_class, atribute);
        LaunchMethods(_class, methods);
    }

    private void WorkWithTestMethods(Type _class)
    {
        var instance = Activator.CreateInstance(_class);
        var methodsBefore = GetMethodsByAtributeAndClass(_class, typeof(BeforeAttribute));
        var methodsAfter = GetMethodsByAtributeAndClass(_class, typeof(AfterAttribute));
        var methods = _class.GetMethods();
        foreach(var method in methods)
        {
            if (method.IsDefined(typeof(TestAttribute), true))
            {
                RunMethodsBeforeAndAfter(methodsBefore, instance);
                RunMethod(_class, method);
                RunMethodsBeforeAndAfter(methodsAfter, instance);
            }
        }
    }
    private void StartTests(Type _class)
    {
        WorkWithClassMethods(_class, typeof(BeforeClassAttribute));
        WorkWithTestMethods(_class);
        WorkWithClassMethods(_class, typeof(AfterClassAttribute));
    }

    private static void LaunchMethods(Type _class, MethodInfo[]? methods)
    {
        if (_class == null || methods == null)
        {
            throw new NullReferenceException();
        }

        foreach (var method in methods)
        {
            if (!method.IsStatic)
            {
                throw new InvalidOperationException();
            }

            method.Invoke(null, null);
        }
    }

    private static void RunMethodsBeforeAndAfter(MethodInfo[]? methods, object? instance)
    {
        if (instance == null || methods == null)
        {
            throw new NullReferenceException();
        }
        foreach(var method in methods)
        {
            method.Invoke(instance, null);
        }
    }

    private void RunMethod(object? instance, MethodInfo? method)
    {
        if (instance == null || method == null || locker == null)
        {
            throw new InvalidOperationException();
        }
        
        var atribute = Attribute.GetCustomAttribute(method, typeof(TestAttribute));

        if (atribute == null)
        {
            throw new InvalidOperationException();
        }

        var testAttribute = (TestAttribute)atribute;

        if (testAttribute.Ignored != null)
        {
            lock (locker)
            {
                listOfResults.Add(new ResultsTests(method.Name, 0, ResultsTests.Status.Ignored));
            }
            return;
        }

        var stopWatch =  new Stopwatch();

        try
        {
            stopWatch.Start();
            method.Invoke(instance, null);
            stopWatch.Stop();
            lock (locker)
            {
                if (testAttribute != null && testAttribute.Expected != null)
                {
                    listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ResultsTests.Status.Failed));
                }
                else
                {
                    listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ResultsTests.Status.Passed));
                }
            }
        }
        catch(Exception ex)
        {
            stopWatch.Stop();
            var exceptionType = ex.GetType();

            if (testAttribute != null && testAttribute.Expected != null && testAttribute.Expected.IsAssignableFrom(exceptionType) ||
               (ex.InnerException != null && testAttribute != null && testAttribute.Expected != null && testAttribute.Expected.IsAssignableFrom(ex.InnerException.GetType())))
            {
                lock (locker)
                {
                    listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ex, ResultsTests.Status.Passed));
                }
            }
            else
            {
                lock (locker)
                {
                    listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ex, ResultsTests.Status.Failed));
                }
            }
        }
    }

}