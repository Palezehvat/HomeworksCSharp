using SimpleFTP;

var client = new Client(8888, "localhost");
var result = await client.Get("../local.txt");
Console.WriteLine(result);