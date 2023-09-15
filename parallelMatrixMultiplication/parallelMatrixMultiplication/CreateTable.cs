using System.Diagnostics;

using parallelMatrixMultiplication;

/// <summary>
/// A class for measuring the standard deviation and mathematical expectation 
/// </summary>
public static class CreateTable
{
    private static int n = 10;

    private static double GetStandartDeviation(int n, double[] arrayForStandardDeviation, double minValue)
    {
        double summaryForStandartDeviation = 0;
        for (int i = 0; i < n; i++)
        {
            summaryForStandartDeviation += Math.Pow(arrayForStandardDeviation[i] - minValue, 2);
        }

        return Math.Sqrt(summaryForStandartDeviation / (n - 1));
    }

    private static double GetMinValue(int[][] firstMatrix, int[][] secondMatrix, int[][] correctMatrix,
                                       double[] arrayForStandardDeviation, int n, int sizeThreads)
    {
        double summary = 0;
        for (int i = 0; i < n; i++)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
            stopWatch.Stop();
            arrayForStandardDeviation[i] = (double)stopWatch.ElapsedMilliseconds / 1000;
            summary += arrayForStandardDeviation[i];
        }
        return summary / n;
    }

    private static void MultiplyingMatricesOfSizeThreeByThree(string filePath)
    {
        var sizeThreads = 3;
        var listOfValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var listOfCorrectValues = new List<int> { 30, 36, 42, 66, 81, 96, 102, 126, 150 };
        var firstMatrix = Matrix.CreateMatrix(3, 3, listOfValues);
        var secondMatrix = Matrix.CreateMatrix(3, 3, listOfValues);
        var correctMatrix = Matrix.CreateMatrix(3, 3, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double minValue = GetMinValue(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, minValue);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 3x3:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(minValue);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyingMatricesOfBigSize(string filePath)
    {
        var sizeThreads = 50;

        var listOfValuesFirstMatrix = new List<int> { };
        var listOfValuesSecondMatrix = new List<int> { };
        var listOfCorrectValues = new List<int> { };

        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < 10000; ++j)
            {
                listOfValuesFirstMatrix.Add(1);
            }
            listOfValuesSecondMatrix.Add(1);
            listOfCorrectValues.Add(10000);
        }

        var firstMatrix = Matrix.CreateMatrix(10000, 10000, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(10000, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(10000, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double minValue = GetMinValue(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, minValue);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 10000x10000 и 10000x1:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(minValue);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyingMatricesOfDifferentSizes(string filePath)
    {
        var sizeThreads = 3;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 3 };
        var listOfCorrectValues = new List<int> { 14, 32, 50 };
        var firstMatrix = Matrix.CreateMatrix(3, 3, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(3, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(3, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double minValue = GetMinValue(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, minValue);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 3x3 и 3x1:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(minValue);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyingOfNotSquareMatrices(string filePath)
    {
        var sizeThreads = 10;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 3, 4, 5 };
        var listOfCorrectValues = new List<int> { 55, 130 };
        var firstMatrix = Matrix.CreateMatrix(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double minValue = GetMinValue(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, minValue);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 2x5 и 5x1:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(minValue);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyingMatricesWithNegativeNumbers(string filePath)
    {
        var sizeThreads = 10;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, -3, 4, 5, 6, -7, 8, 9, 10 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 3, -4, 5 };
        var listOfCorrectValues = new List<int> { 5, 30 };
        var firstMatrix = Matrix.CreateMatrix(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double minValue = GetMinValue(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, minValue);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 2x5 и 5x1 с отрицательными числами:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(minValue);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyingMatricesWithZero(string filePath)
    {
        var sizeThreads = 10;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, 0, 4, 5, 6, 0, 8, 9, 10 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 0, 0, 5 };
        var listOfCorrectValues = new List<int> { 30, 56 };
        var firstMatrix = Matrix.CreateMatrix(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double minValue = GetMinValue(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, minValue);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 2x5 и 5x1 с нулями:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(minValue);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    /// <summary>
    /// The function calculates the mathematical expectation and the standard deviation
    /// </summary>
    /// <param name="filePath">The file where the results are recorded</param>
    public static void CreateTableWithResults(string filePath)
    {
        var streamForWrite = new StreamWriter(filePath);
        streamForWrite.Write("В файле представлены: математическое ожидание и среднеквадратичное отклонение\n");
        streamForWrite.Close();
        MultiplyingMatricesOfSizeThreeByThree(filePath);
        MultiplyingMatricesOfBigSize(filePath);
        MultiplyingMatricesOfDifferentSizes(filePath);
        MultiplyingOfNotSquareMatrices(filePath);
        MultiplyingMatricesWithNegativeNumbers(filePath);
        MultiplyingMatricesWithZero(filePath);
    }
}
