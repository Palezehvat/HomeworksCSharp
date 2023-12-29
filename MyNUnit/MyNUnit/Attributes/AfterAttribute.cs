namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterAttribute : Attribute
{
    public AfterAttribute() { }


    [AfterAttribute]
    public void Method()
    {
        ;
    }
}