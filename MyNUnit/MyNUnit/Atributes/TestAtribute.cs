namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestAtribute : Attribute
{
    public TestAtribute(Type? expected, string? ignored)
    {
        Expected = expected;
        Ignored = ignored;
    }

    public TestAtribute() { }

    public Type? Expected { get; set; }

    public string? Ignored { get; set; }
}