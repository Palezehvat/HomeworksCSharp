using System.Diagnostics;
using System.Reflection;
using Attributes;

namespace MyNUnit;

public class ApplicationForTests
{
    public enum Status
    {
        Passed,
        Failed,
        Ignored
    }

    public class ResultsTests
    {
        public string name { get; }
   
        public long workTime { get; }

        public Exception? reasonFail { get; }

        public Status statusTest { get; }

        public ResultsTests(string name, long workTime, Exception reasonFail, Status statusTest)
        {
            this.name = name;
            this.workTime = workTime;
            this.reasonFail = reasonFail;
            this.statusTest = statusTest;
        }
    }

    public readonly List<ResultsTests>? listOfResults;
    private object? locker;

    public ApplicationForTests(string path)
    {
        var listOfResults = new List<ResultsTests>();
        var files = Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
        var classes = files.Select(Assembly.Load)
                           .SelectMany(a => a.ExportedTypes)
                           .Where(t => t.IsClass)
                           .Where(t => t.GetMethods()
                           .Where(t => t.GetCustomAttributes(true)
                           .Any(a => a is Test))
                           .Any());

        object? locker = new object();

        Parallel.ForEach(classes, StartTests);
    }

    private MethodInfo[]? GetMethodsByAtributeAndClass(Type _class, Type atribute)
    {
        return _class.GetMethods()
                     .Where(m => m.GetCustomAttributes(atribute, false).Length > 0)
                     .ToArray();
    }

    private void WorkWithClassMethods(Type _class, Type atribute)
    {
        var methods = GetMethodsByAtributeAndClass(_class, atribute);
        LaunchMethods(_class, methods);
    }

    private void WorkWithTestMethods(Type _class)
    {
        var instance = Activator.CreateInstance(_class);
        var methodsBefore = GetMethodsByAtributeAndClass(_class, typeof(Before));
        var methodsAfter = GetMethodsByAtributeAndClass(_class, typeof(After));
        var methods = _class.GetMethods();
        foreach(var method in methods)
        {
            if (method.IsDefined(typeof(Test), true))
            {
                RunMethodsBeforeAndAfter(methodsBefore, instance);
                RunMethod(_class, method);
                RunMethodsBeforeAndAfter(methodsAfter, instance);
            }
        }
    }
    private void StartTests(Type _class)
    {
        WorkWithClassMethods(_class, typeof(BeforeClass));
        WorkWithTestMethods(_class);
        WorkWithClassMethods(_class, typeof(AfterClass));
    }

    private void LaunchMethods(Type _class, MethodInfo[]? methods)
    {
        if (_class == null || methods == null)
        {
            throw new ArgumentNullException();
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

    private void RunMethodsBeforeAndAfter(MethodInfo[]? methods, object? instance)
    {
        if (instance == null || methods == null)
        {
            throw new ArgumentNullException();
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
        
        var atribute = Attribute.GetCustomAttribute(method, typeof(Test));

        if (atribute == null)
        {
            throw new InvalidOperationException();
        }

        if (((Test?)atribute).Ignored != null)
        {
            lock (locker)
            {
                listOfResults.Add(new ResultsTests(method.Name, 0, null, Status.Ignored));
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
            if (ex.InnerException != null && ((Test?)atribute) != null && ex.InnerException.GetType() == ((Test?)atribute).Expected)
            {
                lock (locker)
                {
                    listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ex, Status.Passed));
                }
            }
            else
            {
                lock (locker)
                {
                    listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, ex, Status.Failed));
                }
            }
            return;
        }
        stopWatch.Stop();
        lock (locker)
        {
            if (((Test?)atribute) != null && ((Test?)atribute).Expected != null)
            {
                listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, null, Status.Failed));
            }
            else 
            {
                listOfResults.Add(new ResultsTests(method.Name, stopWatch.ElapsedMilliseconds, null, Status.Passed));
            }
        }
    }

}