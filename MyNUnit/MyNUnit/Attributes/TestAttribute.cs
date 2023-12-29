namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class TestAttribute : Attribute
{
    public TestAttribute() { }
    
    public TestAttribute(Type? expected) 
    {
        Expected = expected;
    }

    public TestAttribute(string ignored)
    {
        Ignored = ignored;
    }

    public TestAttribute(Type? expected, string? ignored)
    {
        Expected = expected;
        Ignored = ignored;
    }

    public Type? Expected { get; set; }

    public string? Ignored { get; set; }
}