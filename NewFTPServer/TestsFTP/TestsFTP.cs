namespace TestsFTP;
using SimpleFTP;

public class Tests
{
    Server server = new SimpleFTP.Server(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsFTP"), 8888);
    Client client = new Client(8888, "localhost");

    [SetUp]
    public void SetUp()
    {
        if (server == null || client == null)
        {
            return;
        }
        var serverTask = Task.Run(server.Start);
    }

    [Test]
    public async Task GettingInformationFromANonExistentFile()
    {
        var result = await client.Get("./test.txt");
        Assert.That(result, Is.EqualTo("-1"));
    }

    [Test]
    public async Task GettingInformationFromExistentFile()
    {
        var result = await client.Get("./testForGet.txt");
        Assert.That(result, Is.EqualTo("3 123"));
    }

    [Test]
    public async Task GettingInformationFromNonExistentDirectory()
    {
        var result = await client.List("./testForLister");
        Assert.That(result, Is.EqualTo("-1"));
    }

    [Test]
    public async Task GettingInformationFromExistentDirectory()
    {
        var result = await client.List("./testForList");
        Assert.That(result, Is.EqualTo("3 test3 true test1.txt false test2.txt false"));
    }

    [TearDown]
    public void Teardown()
    {
        server.Stop();
    }
}