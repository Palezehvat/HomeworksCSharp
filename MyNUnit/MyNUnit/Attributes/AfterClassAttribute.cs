namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterClassAttribute : Attribute
{
    public AfterClassAttribute() { }
}