namespace ProjectForTest;

using MyNUnit.Atributes;

public class ClassForTests
{
    public static int counter = 0;

    [TestAttribute(Expected = typeof(IndexOutOfRangeException))]
    public void InvalidMethod()
    {
        throw new NotImplementedException();
    }

    [TestAttribute(Expected = typeof(FileNotFoundException))]
    public int CorrectMethod()
    {
        throw new FileNotFoundException();
    }

    [BeforeClassAttribute]
    public static void BeforeClass()
    {
        counter += 1;
    }

    [AfterClassAttribute]
    public static void AfterClass()
    {
        counter += 1;
    }

    [AfterAttribute]
    public void BeforeMethod()
    {
        counter += 1;
    }

    [BeforeAttribute]
    public void AfterMethod()
    {
        counter += 1;
    }

    [TestAttribute(Ignored = "Ignore")]
    public void IgnoreTest()
    {
        ;
    }

    [TestAttribute(Expected = typeof(InvalidCastException))]
    public void OneMoreCorrectMethod()
    {
        throw new InvalidCastException();
    }

    [TestAttribute(Expected = typeof(InvalidOperationException))]
    public void OneMoreIncorrectMethod()
    {
        throw new InvalidProgramException();
    }
}