using System.Linq.Expressions;
using System.Reflection;

namespace Kr3;

public class Reflector
{
    private readonly string filePath;

    public Reflector(string filePath) => this.filePath = filePath;

    private static string GetVisibilityFromClass(Type someClass)
    {
        if (someClass.IsNestedPrivate)
        {
            return "private ";
        }
        else if (someClass.IsNestedPublic)
        {
            return "public ";
        }
        else if (someClass.IsNestedFamily)
        {
            return "protected ";
        }
        else if (someClass.IsNestedAssembly)
        {
            return "internal ";
        }
        return "";
    }

    private static string GetStaticOrNotFromClass(Type someClass)
    {
        return someClass.IsAbstract && someClass.IsSealed ? "static " : "";
    }

    private static string GetVisibilityFromField(FieldInfo someField)
    {
        if (someField.IsPrivate)
        {
            return "private ";
        }
        else if (someField.IsPublic)
        {
            return "public ";
        }
        else if (someField.IsFamily)
        {
            return "protected ";
        }
        else if (someField.IsAssembly)
        {
            return "internal ";
        }
        return "";
    }

    private static string GetStaticOrNotFromField(FieldInfo someField)
    {
        return someField.IsStatic ? "static " : "";
    }

    private static string GetStaticOrNotFromMethod(MethodInfo someMethod)
    {
        return someMethod.IsStatic ? "static " : "";
    }

    private static string GetVisibilityFromMethod(MethodInfo someMethod)
    {
        if (someMethod.IsPrivate)
        {
            return "private ";
        }
        else if (someMethod.IsPublic)
        {
            return "public ";
        }
        else if (someMethod.IsFamily)
        {
            return "protected ";
        }
        else if (someMethod.IsAssembly)
        {
            return "internal ";
        }
        return "";
    }

    private static string GetVisibilityFromConstructor(ConstructorInfo constructorInfo)
    {
        if (constructorInfo.IsPrivate)
        {
            return "private ";
        }
        else if (constructorInfo.IsPublic)
        {
            return "public ";
        }
        else if (constructorInfo.IsFamily)
        {
            return "protected ";
        }
        else if (constructorInfo.IsAssembly)
        {
            return "internal ";
        }
        return "";
    }

    private void WriteParameters(StreamWriter writer, ParameterInfo[] parameters)
    {
        var first = true;
        foreach (var parameter in parameters)
        {
            if (first)
            {
                writer.Write($"{parameter.ParameterType} {parameter.Name}");
                first = false;
            }
            else
            {
                writer.Write($" ,{parameter.Name}");
            }
        }
    }

    public void PrintStructure(Type someClass)
    {
        string className = someClass.Name;
        string file = $"{filePath}\\{className}.cs";

        using var writer = new StreamWriter(file);

        var visibility = GetVisibilityFromClass(someClass);
        var staticOrNotStatic = GetStaticOrNotFromClass(someClass);
        var interfaces = someClass.GetInterfaces();
        if (interfaces.Length == 0)
        {
            writer.WriteLine($"{visibility}{staticOrNotStatic}class {className}");
        }
        else
        {
            writer.Write($"{visibility}{staticOrNotStatic}class {className} : ");
            var first = true;
            foreach (var element in interfaces)
            {
                if (first)
                {
                    writer.Write($"{element.Name}");
                    first = false;
                }
                else
                {
                    writer.Write($" : {element.Name}");
                }
            }
            writer.WriteLine();
        }
        writer.WriteLine("{");

        var constructors = someClass.GetConstructors();
        foreach (var constructor in constructors)
        {
            writer.Write($"\t{GetVisibilityFromConstructor(constructor)}{className}(");
            WriteParameters(writer, constructor.GetParameters());
            writer.WriteLine(") {}");
        }
        var fields = someClass.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            writer.WriteLine($"\t{GetVisibilityFromField(field)}{GetStaticOrNotFromField(field)}{field};");
        }

        var methods = someClass.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        foreach (var method in methods)
        {
            writer.Write($"\t{GetVisibilityFromMethod(method)}{GetStaticOrNotFromMethod(method)}{method.Name} (");
            WriteParameters(writer, method.GetParameters());
            writer.WriteLine(") {throw new OperationCanceledException()}");
        }

        var nestedClasses = someClass.GetNestedTypes();
        foreach (var nestedClass in nestedClasses)
        {
            PrintStructure(nestedClass);
        }

        writer.WriteLine("}");
    }

    private static bool CheckIfThereIsMethodFromOneClassInAnother(MethodInfo[] methodsB, MethodInfo methodA)
    {
        var methodB = methodsB.FirstOrDefault(m => m.Name == methodA.Name && m.GetParameters().
            Select(p => p.ParameterType).SequenceEqual(methodA.GetParameters().Select(p => p.ParameterType)));
        return methodB != null;
    }

    private static void WriteDifferentMethods(StreamWriter writer, 
        MethodInfo[] methodsFirst, MethodInfo[] methodsSecond)
    {
        foreach (var methodA in methodsFirst)
        {
            if (!CheckIfThereIsMethodFromOneClassInAnother(methodsSecond, methodA))
            {
                writer.Write($"{methodA.Name}\n");
            }
        }
    }
    private static void WriteDifferentFields(StreamWriter writer,
        FieldInfo[] fieldsFirst, FieldInfo[] fieldsSecond)
    {
        foreach (var fieldA in fieldsFirst)
        {
            if (!CheckIfThereIsFieldFromOneClassInAnother(fieldsSecond, fieldA))
            {
                writer.Write($"{fieldA.Name}\n");
            }
        }
    }

    private static bool CheckIfThereIsFieldFromOneClassInAnother(FieldInfo[] fieldsB, FieldInfo fieldA)
    {
        var fieldB = fieldsB.FirstOrDefault(f => f.Name == fieldA.Name && f.FieldType == fieldA.FieldType);
        return fieldB != null;
    }

    public void DiffClasses(Type a, Type b)
    {
        string file = $"{filePath}/difference.txt";

        using var writer = new StreamWriter(file);

        WriteDifferentFields(writer, a.GetFields(), b.GetFields());
        WriteDifferentFields(writer, b.GetFields(), a.GetFields());
        WriteDifferentMethods(writer, a.GetMethods(), b.GetMethods());
        WriteDifferentMethods(writer, b.GetMethods(), a.GetMethods());

        var nestedClassesA = a.GetNestedTypes();
        var nestedClassesB = b.GetNestedTypes();
        foreach (var nestedClassA in nestedClassesA)
        {
            foreach (var nestedClassB in nestedClassesB)
            {
                DiffClasses(nestedClassA, nestedClassB);
            }
        }
    }
}