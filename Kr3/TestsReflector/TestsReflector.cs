namespace TestsReflector;

using Kr3;

public class Tests
{
    [Test]
    public void TestWithTestPrintStructure()
    {
        var sum = new TestClassSum(0);
        var typeSum = sum.GetType();
        var reflector = new Reflector(Path.Combine(TestContext.CurrentContext.TestDirectory, "forTests"));
        reflector.PrintStructure(typeSum);
        using var reader = new StreamReader((Path.Combine(TestContext.CurrentContext.TestDirectory, "forTests", "CheckTestClassSum.cs")));
        var linesForCheck = reader.ReadToEnd().Split('\n');
        using var anotherReader = new StreamReader((Path.Combine(TestContext.CurrentContext.TestDirectory, "forTests", "TestClassSum.cs")));
        var linesFromReflector = anotherReader.ReadToEnd().Split('\n');

        var i = 0;
        var j = 0;
        while (i < linesForCheck.Length || j < linesFromReflector.Length)
        {
            Assert.That(linesForCheck[i], Is.EqualTo(linesFromReflector[j]));
            ++i;
            ++j;
        }
    }

    [Test]
    public void TestWithTestClassDiffClasses()
    {
        var sum = new TestClassSum(0);
        var typeSum = sum.GetType();
        var anotherSum = new AnotherSum(0);
        var typeAnotherSum= anotherSum.GetType();
        var reflector = new Reflector(Path.Combine(TestContext.CurrentContext.TestDirectory,"forTests"));
        reflector.DiffClasses(typeSum, typeAnotherSum);
        using var reader = new StreamReader((Path.Combine(TestContext.CurrentContext.TestDirectory, "forTests", "difference.txt")));
        var lines = reader.ReadToEnd().Split('\n');
        Assert.That(lines[0], Is.EqualTo("Add"));
        Assert.That(lines[1], Is.EqualTo("AddToSum"));
    }
}