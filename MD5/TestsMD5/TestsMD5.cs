namespace TestsMD5;

using Md5;

public class Tests
{
    private string pathDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "ForTests");
    private string pathFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "ForTests", "file1.txt");

    [Test]
    public async Task AreTheResultsSameWithDirectories()
    {
        var hashMultiThreads = await MultiThreadMD5.CalculateDirectory(pathDirectory);
        var hashSingleThread = SingleMd5.CalculateDirectory(pathDirectory);

        Assert.That(hashMultiThreads, Is.EquivalentTo(hashSingleThread));
    }

    [Test]
    public async Task AreTheResultsSameWithFiles()
    {
        var hashMultiThreads = await MultiThreadMD5.CalculateFile(pathFile);
        var hashSingleThread = SingleMd5.CalculateFile(pathFile);

        Assert.That(hashMultiThreads, Is.EquivalentTo(hashSingleThread));
    }

    [Test]
    public void IsThrowExceptionWhenIncorrectPathDirrectoryInSingleThread()
    {
        Assert.Throws<DirectoryNotFoundException>(() => SingleMd5.CalculateDirectory(pathFile));
    }

    [Test]
    public void IsThrowExceptionWhenIncorrectPathDirrectoryInMultiThread()
    {
        Assert.ThrowsAsync<DirectoryNotFoundException>(() => MultiThreadMD5.CalculateDirectory(pathFile));
    }

    [Test]
    public void IsThrowExceptionWhenIncorrectPathFileInSingleThread()
    {
        Assert.Throws<FileNotFoundException>(() => SingleMd5.CalculateFile(pathDirectory + "/ttrt.txt"));
    }

    [Test]
    public void IsThrowExceptionWhenIncorrectPathFileInMultiThread()
    {
        Assert.ThrowsAsync<FileNotFoundException>(() => MultiThreadMD5.CalculateFile(pathDirectory + "/ttrt.txt"));
    }
}