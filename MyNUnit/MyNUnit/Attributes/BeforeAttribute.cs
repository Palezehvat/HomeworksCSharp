namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeAttribute : Attribute
{
    public BeforeAttribute() { }


    [BeforeAttribute]
    public void Method()
    {
        ;
    }
}