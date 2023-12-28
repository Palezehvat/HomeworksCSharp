namespace TestsFTP;
using SimpleFTP;

public class Tests
{
    Server server = new SimpleFTP.Server(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsFTP"), 7777);
    Client client = new Client(7777, "localhost");
    
    [SetUp]
    public void SetUp()
    {
        if (server == null)
        {
            return;
        }
        Task.Run(() => server.Start());
    }

    [Test]
    public void GettingInformationFromANonExistentFile()
    {
        var result = client.Get("./test.txt");
        Assert.That(result.Result, Is.EqualTo("-1"));
    }

    [Test]
    public void GettingInformationFromExistentFile()
    {
        var result = client.Get("./testForGet.txt");
        Assert.That(result.Result, Is.EqualTo("3 123"));
    }

    [Test]
    public void GettingInformationFromNonExistentDirectory()
    {
        var result = client.List("./testForLister");
        Assert.That(result.Result, Is.EqualTo("-1"));
    }

    [Test]
    public void GettingInformationFromExistentDirectory()
    {
        var result = client.List("./testForList");
        Assert.That(result.Result, Is.EqualTo("3 test3 true test1.txt false test2.txt false"));
    }

    [TearDown]
    public void Teardown()
    {
        server.Stop();
    }
}