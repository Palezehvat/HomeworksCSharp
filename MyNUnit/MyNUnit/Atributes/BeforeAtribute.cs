namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeAtribute : Attribute
{
    public BeforeAtribute() { }


    [BeforeAtribute]
    public void Method()
    {
        ;
    }
}