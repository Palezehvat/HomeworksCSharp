namespace MyNUnit.Atributes;

[AttributeUsage(AttributeTargets.Method)]
public class AfterAtribute : Attribute
{
    public AfterAtribute() { }


    [AfterAtribute]
    public void Method()
    {
        ;
    }
}