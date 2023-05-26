using RoutersByGraph;

var routers = new Routers();
Console.WriteLine("Enter the file path");
var filePath = Console.ReadLine();
Console.Write("Enter file name where to write a new graph");
var fileAfter = Console.ReadLine();
bool isLinkedGraph = true;
try
{
      isLinkedGraph = routers.WorkWithFile(filePath, fileAfter);
}
catch (NullGraphOrGraphComponentsException)
{
     Console.WriteLine("Problems with graph, an incorrect example in the file is possible");
}
     catch (InvalidFileException)
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