namespace TestsFTP;

using Microsoft.VisualBasic;
using SimpleFTP;

public class Tests
{
    Server server = new SimpleFTP.Server(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsFTP"), 8888);

    [Test]
    public async Task GettingInformationFromANonExistentFile()
    {
        var client = new Client(8888, "localhost");

        if (server == null)
        {
            Assert.Fail();
            return;
        }

        if (client == null)
        {
            Assert.Fail();
            return;
        }

        var serverThread = new Thread(async () => await server.Start());

        serverThread.Start();

        var result = await client.Get("./test.txt");

        Assert.That(result, Is.EqualTo("-1"));

        serverThread.Join();
    }

    [Test]
    public async Task GettingInformationFromExistentFile()
    {
        var client = new Client(8888, "localhost");

        if (server == null)
        {
            Assert.Fail();
            return;
        }

        if (client == null)
        {
            Assert.Fail();
            return;
        }

        var serverThread = new Thread(async () => await server.Start());

        serverThread.Start();

        var result = await client.Get("./testForGet.txt");

        Assert.That(result, Is.EqualTo("3 123"));

        serverThread.Join();
    }

    [Test]
    public async Task GettingInformationFromNonExistentDirectory()
    {
        var client = new Client(8888, "localhost");

        if (server == null)
        {
            Assert.Fail();
            return;
        }

        if (client == null)
        {
            Assert.Fail();
            return;
        }

        var serverThread = new Thread(async () => await server.Start());

        serverThread.Start();

        var result = await client.List("./testForLister");

        Assert.That(result, Is.EqualTo("-1"));

        serverThread.Join();
    }

    [Test]
    public async Task GettingInformationFromExistentDirectory()
    {
        var client = new Client(8888, "localhost");

        if (server == null)
        {
            Assert.Fail();
            return;
        }

        if (client == null)
        {
            Assert.Fail();
            return;
        }

        var serverThread = new Thread(async () => await server.Start());

        serverThread.Start();

        var result = await client.List("./testForList");

        Assert.That(result, Is.EqualTo("3 test3 true test1.txt false test2.txt false\n"));
        serverThread.Join();
    }
}