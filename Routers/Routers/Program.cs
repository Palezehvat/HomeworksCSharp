namespace ListAndUniqueList;

using Routers;

class Program
{
    public static int Main(string[] args)
    {
        var routers = new Routers();
        Console.WriteLine("Enter the file path with double slashes");
        var filePath = Console.ReadLine();
        var fileAfter = Console.ReadLine();
        bool isLinkedGraph = true;
        try
        {
            isLinkedGraph = routers.WorkWithFile(filePath, fileAfter);
        }
        catch (NullPointerException)
        {
            Console.WriteLine("Problems with pointers, an incorrect example in the file is possible");
        }
        catch(InvalidFileException)
        {
            Console.WriteLine("Problems with the path to the file or the contents of the file");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Problems with incorrect way to file");
        }
        if (!isLinkedGraph)
        {
            Console.WriteLine("Graph not Linked", Console.Error);
            return -1;
        }
        return 0;
    }
}