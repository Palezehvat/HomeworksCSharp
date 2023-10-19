namespace SimpleFTP;

class Program
{
    static void Main()
    {
        var server = new SimpleFTP.Server("C:/", 8888);
    }
}