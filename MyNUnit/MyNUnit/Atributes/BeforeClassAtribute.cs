namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeClassAtribute : Attribute
{
    public BeforeClassAtribute() { }
}