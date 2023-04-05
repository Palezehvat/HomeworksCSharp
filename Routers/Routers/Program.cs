namespace ListAndUniqueList;

using Routers;

class Program
{
    public static void Main(string[] args)
    {
        var routers = new Routers();
        Console.WriteLine("Enter the file path with double slashes");
        var filePath = Console.ReadLine();
        try
        {
            routers.WorkWithFile(filePath);
        }
        catch (NullPointerException)
        {
            Console.WriteLine("Problems with pointers, an incorrect example in the file is possible");
        }
        catch(InvalidFileException)
        {
            Console.WriteLine("Problems with the path to the file or the contents of the file");
        }
    }
}