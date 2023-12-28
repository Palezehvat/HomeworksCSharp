namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterClassAtribute : Attribute
{
    public AfterClassAtribute() { }
}