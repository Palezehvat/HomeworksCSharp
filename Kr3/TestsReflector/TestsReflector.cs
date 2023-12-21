namespace TestsReflector;

using Kr3;

public class Tests
{
    [Test]
    public void TestWithTestClassSum()
    {
        var sum = new TestClassSum();
        var typeSum = sum.GetType();
        var reflector = new Reflector("C:\\Users\\User\\source\\repos\\HomeworksCSharp\\Kr3\\Kr3\\obj\\Debug\\net7.0\\forTests");
        reflector.PrintStructure(typeSum);
    }
}