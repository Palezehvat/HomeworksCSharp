using parallelMatrixMultiplication;

Console.WriteLine("Write first file name");
var firstFile = Console.ReadLine();
Console.WriteLine("Write second file name");
var secondFile = Console.ReadLine();
Console.WriteLine("Write name file where will be result");
var resultFile = Console.ReadLine();
Console.WriteLine("Write file path for create table");
var tableFile = Console.ReadLine();

if (tableFile == null)
{
    Console.WriteLine("Неверно прописан путь для tableFile");
    return;
}
if (resultFile == null)
{
    Console.WriteLine("Неверно прописан путь для firstFile");
    return;
}
if (secondFile == null)
{
    Console.WriteLine("Неверно прописан путь для secondFile");
    return;
}
if (firstFile == null)
{
    Console.WriteLine("Неверно прописан путь для resultFile");
    return;
}

Matrix.MatrixMultiplication(firstFile, secondFile, resultFile);
GetStandartDeviationAndMinValue.CreateTableWithResults(tableFile);

var resultCompare = Matrix.CompareMatrixMultiplication(firstFile, secondFile);
if (resultCompare < 0)
{
    Console.Write("Параллельное перемножение матриц медленнее, чем последовательное на ");
    Console.Write(resultCompare);
    Console.WriteLine(" миллисекунд");
}
if (resultCompare > 0)
{
    Console.Write("Последовательное перемножение матриц медленнее, чем параллельное на ");
    Console.Write(resultCompare);
    Console.WriteLine(" миллисекунд");
}