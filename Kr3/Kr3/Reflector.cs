using System.Reflection;

namespace Kr3;

public class Reflector
{
    private string filePath;

    public Reflector(string filePath) => this.filePath = filePath;

    private string GetVisibilityFromClass(Type someClass)
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

    private string GetStaticOrNotFromClass(Type someClass)
    {
        return someClass.IsAbstract && someClass.IsSealed ? "static " : "";
    }

    private string GetVisibilityFromField(FieldInfo someField)
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

    private string GetStaticOrNotFromField(FieldInfo someField)
    {
        return someField.IsStatic ? "static " : "";
    }

    private string GetStaticOrNotFromMethod(MethodInfo someMethod)
    {
        return someMethod.IsStatic ? "static " : "";
    }

    private string GetVisibilityFromMethod(MethodInfo someMethod)
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

    public void PrintStructure(Type someClass)
    {
        string className = someClass.Name;
        string file = $"{filePath}\\{className}.cs";

        using var writer = new StreamWriter(file);

        var visibility = GetVisibilityFromClass(someClass);
        var staticOrNotStatic = GetStaticOrNotFromClass(someClass);
        writer.WriteLine($"{visibility}{staticOrNotStatic}class {className}");
        writer.WriteLine("{");

        var fields = someClass.GetFields();
        foreach (var field in fields)
        {
            writer.WriteLine($"\t{GetVisibilityFromField(field)}{GetStaticOrNotFromField(field)}{field.ToString()};");
        }

        var methods = someClass.GetMethods();
        foreach (var method in methods)
        {
            writer.WriteLine($"\t{GetVisibilityFromMethod(method)}{GetStaticOrNotFromMethod(method)}{method.ToString()} {{throw OperationCanceledException();}};");
        }

        var nestedClasses = someClass.GetNestedTypes();
        foreach (var nestedClass in nestedClasses)
        {
            PrintStructure(nestedClass);
        }

        writer.WriteLine("}");
    }

    private bool CheckIfThereIsMethodFromOneClassInAnother(MethodInfo[] methodsB, MethodInfo methodA)
    {
        var methodB = methodsB.FirstOrDefault(m => m.Name == methodA.Name && m.GetParameters().
            Select(p => p.ParameterType).SequenceEqual(methodA.GetParameters().Select(p => p.ParameterType)));
        return methodB != null ? true : false;
    }

    private void WriteDifferentMethods(StreamWriter writer, 
        MethodInfo[] methodsFirst, MethodInfo[] methodsSecond)
    {
        foreach (var methodA in methodsFirst)
        {
            if (!CheckIfThereIsMethodFromOneClassInAnother(methodsSecond, methodA))
            {
                writer.WriteLine($"{methodA.Name}");
            }
        }
    }
    private void WriteDifferentFields(StreamWriter writer,
        FieldInfo[] fieldsFirst, FieldInfo[] fieldsSecond)
    {
        foreach (var fieldA in fieldsFirst)
        {
            if (!CheckIfThereIsFieldFromOneClassInAnother(fieldsSecond, fieldA))
            {
                writer.WriteLine($"{fieldA.Name}");
            }
        }
    }

    private bool CheckIfThereIsFieldFromOneClassInAnother(FieldInfo[] fieldsB, FieldInfo fieldA)
    {
        var fieldB = fieldsB.FirstOrDefault(f => f.Name == fieldA.Name && f.FieldType == fieldA.FieldType);
        return fieldB != null ? true : false;
    }

    public void DiffClasses(Type a, Type b)
    {
        string file = $"{filePath}/difference.cs";

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