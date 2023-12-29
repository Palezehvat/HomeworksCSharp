namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeClassAttribute : Attribute
{
    public BeforeClassAttribute() { }
}