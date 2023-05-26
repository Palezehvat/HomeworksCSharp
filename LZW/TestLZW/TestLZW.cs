namespace TestLZW;

using LZW;

public class Tests
{
    [Test]
    public void TheLZWShouldWorkCorrectlyToReturnTheCorrectValueOnASimpleExample()
    {
        var (isCorrect, _) = LZWAlgorithm.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.txt"), "-c");
        Assert.That(!isCorrect, Is.EqualTo(false));
        (isCorrect, var _) = LZWAlgorithm.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.txt.zipped"), "-u");
        Assert.That(!isCorrect, Is.EqualTo(false));
        string correctText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "correctTestAnElementaryExample.txt"));
        string fromLZWText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "testAnElementaryExample.txt"));
        Assert.That(correctText, Is.EqualTo(fromLZWText));
    }

    [Test]
    public void LZWCodeingShouldReturnFalseWhenReceivingAFileWithAnIncorrectName()
    {
        var (isCorrect, _) = LZWAlgorithm.LzwAlgorithm("ds", "-c");
        Assert.False(isCorrect);
    }

    [Test]
    public void LZWDecodeingShouldReturnFalseWhenReceivingAFileWithAnIncorrectName()
    {
        var (isCorrect, _) = LZWAlgorithm.LzwAlgorithm("ds", "-u");
        Assert.False(isCorrect);
    }

    [Test]
    public void LZWShouldReturnFalseWhenReceivingAFileWithAnIncorrectParametr()
    {
        var (isCorrect, _) = LZWAlgorithm.LzwAlgorithm("ds", "-e");
        Assert.False(isCorrect);
    }

    [Test]
    public void TheLZWShouldReturnAnEmptyFileEmpty()
    {
        var (isCorrect, _) = LZWAlgorithm.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.txt"), "-c");
        Assert.That(!isCorrect, Is.EqualTo(false));
        (isCorrect, var _) = LZWAlgorithm.LzwAlgorithm(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.txt.zipped"), "-u");
        Assert.That(!isCorrect, Is.EqualTo(false));
        string fromLZWText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "emptyFile.txt"));
        string correctText = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForLZW", "correctEmptyFile.txt"));
        Assert.That(correctText, Is.EqualTo(fromLZWText));
    }
}