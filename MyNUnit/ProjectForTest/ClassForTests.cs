namespace ProjectForTest;

using MyNUnit.Atributes;

public class ClassForTests
{
    public int counter = 0;

    [TestAtribute]
    public void InvalidMethod()
    {
        throw new NotImplementedException();
    }

    [TestAtribute]
    public int CorrectMethod()
    {
        return 1;
    }

    [BeforeClassAtribute]
    public void BeforeClass()
    {
        counter += 1;
    }

    [AfterClassAtribute]
    public void AfterClass()
    {
        counter += 1;
    }

    [AfterAtribute]
    public void BeforeMethod()
    {
        counter += 1;
    }

    [BeforeAtribute]
    public void AfterMethod()
    {
        counter += 1;
    }

    [TestAtribute(Ignored = "Ignore")]
    public void IgnoreTest()
    {
        ;
    }

    [TestAtribute(Expected = typeof(InvalidCastException))]
    public void InvalidCastException()
    {
        throw new InvalidCastException();
    }

    [TestAtribute(Expected = typeof(InvalidOperationException))]
    public void InvalidException()
    {
        throw new InvalidProgramException();
    }
}