using System.Diagnostics;
using System.Reflection;

using MyNUnit.Atributes;

namespace MyNUnit;

public class ApplicationForTests
{
    public readonly List<ResultsTests>? listOfResults;
    private readonly object locker = new();

    public ApplicationForTests(string path)
    {
        var listOfResults = new List<ResultsTests>();
        var files = Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
        var classes = files.Select(Assembly.Load)
                           .SelectMany(a => a.ExportedTypes)
                           .Where(t => t.IsClass)
                           .Where(t => t.GetMethods()
                           .Where(t => t.GetCustomAttributes(true)
                           .Any(a => a is TestAtribute))
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
        var methodsBefore = GetMethodsByAtributeAndClass(_class, typeof(BeforeAtribute));
        var methodsAfter = GetMethodsByAtributeAndClass(_class, typeof(AfterAtribute));
        var methods = _class.GetMethods();
        foreach(var method in methods)
        {
            if (method.IsDefined(typeof(TestAtribute), true))
            {
                RunMethodsBeforeAndAfter(methodsBefore, instance);
                RunMethod(_class, method);
                RunMethodsBeforeAndAfter(methodsAfter, instance);
            }
        }
    }
    private void StartTests(Type _class)
    {
        WorkWithClassMethods(_class, typeof(BeforeClassAtribute));
        WorkWithTestMethods(_class);
        WorkWithClassMethods(_class, typeof(AfterClassAtribute));
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
        if (instance == null || method == null || locker == null || listOfResults == null)
        {
            throw new InvalidOperationException();
        }
        
        var atribute = Attribute.GetCustomAttribute(method, typeof(TestAtribute));

        if (atribute == null)
        {
            throw new InvalidOperationException();
        }

        var testAtribute = (TestAtribute)atribute;

        if (testAtribute.Ignored != null)
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
        }
        catch(Exception ex)
        {
            stopWatch.Stop();
            if (ex.InnerException != null && testAtribute != null && ex.InnerException.GetType() == testAtribute.Expected)
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
            return;
        }
        stopWatch.Stop();
        lock (locker)
        {
            if (testAtribute != null && testAtribute.Expected != null)
            {
                listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ResultsTests.Status.Failed));
            }
            else 
            {
                listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ResultsTests.Status.Passed));
            }
        }
    }

}