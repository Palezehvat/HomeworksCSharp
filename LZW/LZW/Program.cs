using LZW;


var (isCorrect, compressionRatio) = LZWAlgorithm.LzwAlgorithm(args[0], args[1]);
if (!isCorrect)
{
    Console.WriteLine("Problems...");
    return;
}
Console.WriteLine(compressionRatio);