namespace ProjectForTest;

using Attributes;

public class ClassForTests
{
    public int counter = 0;

    [Test]
    public void InvalidMethod()
    {
        throw new NotImplementedException();
    }

    [Test]
    public int CorrectMethod()
    {
        return 1;
    }

    [BeforeClass]
    public void BeforeClass()
    {
        counter += 1;
    }

    [AfterClass]
    public void AfterClass()
    {
        counter += 1;
    }

    [After]
    public void BeforeMethod()
    {
        counter += 1;
    }

    [Before]
    public void AfterMethod()
    {
        counter += 1;
    }

    [Test(Ignored = "Ignore")]
    public void IgnoreTest()
    {
        ;
    }

    [Test(Expected = typeof(InvalidCastException))]
    public void InvalidCastException()
    {
        throw new InvalidCastException();
    }

    [Test(Expected = typeof(InvalidOperationException))]
    public void InvalidException()
    {
        throw new InvalidProgramException();
    }
}