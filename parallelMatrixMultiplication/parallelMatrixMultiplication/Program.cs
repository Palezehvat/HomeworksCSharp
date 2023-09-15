using parallelMatrixMultiplication;

Console.WriteLine("Write first file name");
var firstFile = Console.ReadLine();
Console.WriteLine("Write second file name");
var secondFile = Console.ReadLine();
Console.WriteLine("Write name file where will be result");
var resultFile = Console.ReadLine();
Console.WriteLine("Write file path for create table");
var tableFile = Console.ReadLine();

if (tableFile != null && firstFile != null && secondFile != null && resultFile != null)
{
    Matrix.MatrixMultiplicationControlFunction(firstFile, secondFile, resultFile, 8);
    CreateTable.CreateTableWithResults(tableFile);
}