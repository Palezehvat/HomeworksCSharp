namespace TestsForLZW;

using LZW;
using System.Reflection;

public class Tests
{
    LZW lzw;
    [SetUp]
    public void Setup()
    {
        lzw = new LZW();
    }

    [TestCaseSource(nameof(LZW))]
    public void TheLZWShouldWorkCorrectlyToReturnTheCorrectValueOnASimpleExample(LZW lzw)
    {
        Setup();
        var (isCorrect, _) = lzw.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.txt"), "-c");
        if (!isCorrect )
        {
            Assert.Fail();
        }
        (isCorrect, var _) = lzw.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.zipped"), "-u");
        if (!isCorrect)
        {
            Assert.Fail();
        }
        string correctText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.txt"));
        string fromLZWText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.exe"));
        Assert.True(correctText == fromLZWText);
    }

    [TestCaseSource(nameof(LZW))]
    public void LZWCodeingShouldReturnFalseWhenReceivingAFileWithAnIncorrectName(LZW lzw)
    {
        var (isCorrect, _) = lzw.LzwAlgorithm("ds", "-c");
        Assert.False(isCorrect);
    }

    [TestCaseSource(nameof(LZW))]
    public void LZWDecodeingShouldReturnFalseWhenReceivingAFileWithAnIncorrectName(LZW lzw)
    {
        var (isCorrect, _) = lzw.LzwAlgorithm("ds", "-u");
        Assert.False(isCorrect);
    }

    [TestCaseSource(nameof(LZW))]
    public void LZWShouldReturnFalseWhenReceivingAFileWithAnIncorrectParametr(LZW lzw)
    {
        var (isCorrect, _) = lzw.LzwAlgorithm("ds", "-e");
        Assert.False(isCorrect);
    }

    [TestCaseSource(nameof(LZW))]
    public void TheLZWShouldReturnAnEmptyFileEmpty(LZW lzw)
    {
        Setup();
        var (isCorrect, _) = lzw.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.txt"), "-c");
        if (!isCorrect)
        {
            Assert.Fail();
        }
        (isCorrect, var _) = lzw.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.zipped"), "-u");
        if (!isCorrect)
        {
            Assert.Fail();
        }
        string correctText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.txt"));
        string fromLZWText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.exe"));
        Assert.True(correctText == fromLZWText);
    }

    private static IEnumerable<TestCaseData> LZW
    => new TestCaseData[]
    {
        new TestCaseData(new LZW()),
    };
}