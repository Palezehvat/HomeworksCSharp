using System.Diagnostics;

using parallelMatrixMultiplication;

/// <summary>
/// A class for measuring the standard deviation and mathematical expectation 
/// </summary>
public static class StandartDeviationAndMathExpectation
{
    private static int n = 10;

    private static double GetStandartDeviation(int n, double[] arrayForStandardDeviation, double mathExpectation)
    {
        double summaryForStandartDeviation = 0;
        for (int i = 0; i < n; i++)
        {
            summaryForStandartDeviation += Math.Pow(arrayForStandardDeviation[i] - mathExpectation, 2);
        }

        return Math.Sqrt(summaryForStandartDeviation / (n - 1));
    }

    private static double GetMathExpectation(int[][] firstMatrix, int[][] secondMatrix, int[][] correctMatrix,
                                       double[] arrayForStandardDeviation, int n, int sizeThreads)
    {
        double summary = 0;
        for (int i = 0; i < n; i++)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
            stopWatch.Stop();
            arrayForStandardDeviation[i] = (double)stopWatch.ElapsedMilliseconds / 1000;
            summary += arrayForStandardDeviation[i];
        }
        return summary / n;
    }

    private static void MultiplyMatricesOfSizeThreeByThree(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;
        var listOfValues = new List<int[]> 
        {
            new int[3] { 1, 2, 3 },
            new int[3] { 4, 5, 6 },
            new int[3] { 7, 8, 9 },
        };

        var listOfCorrectValues = new List<int[]> 
        {
            new int[3] { 30, 36, 42 },
            new int[3] { 66, 81, 96 },
            new int[3] { 102, 126, 150 },
        };

        var firstMatrix = Matrix.Create(3, 3, listOfValues);
        var secondMatrix = Matrix.Create(3, 3, listOfValues);
        var correctMatrix = Matrix.Create(3, 3, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double mathExpectation = GetMathExpectation(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, mathExpectation);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 3x3:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(mathExpectation);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyMatricesOfBigSize(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;

        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };
        var listOfCorrectValues = new List<int[]> { };

        for (int i = 0; i < 10000; i++)
        {
            listOfValuesFirstMatrix.Add(new int[10000]);
            listOfValuesSecondMatrix.Add(new int[1]);
            listOfCorrectValues.Add(new int[1]);
            for (int j = 0; j < 10000; ++j)
            {
                listOfValuesFirstMatrix[i][j] = 1;
            }
            listOfValuesSecondMatrix[i][0] = 1;
            listOfCorrectValues[i][0] = 10000;
        }

        var firstMatrix = Matrix.Create(10000, 10000, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(10000, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(10000, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double mathExpectation = GetMathExpectation(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, mathExpectation);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 10000x10000 и 10000x1:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(mathExpectation);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyMatricesOfDifferentSizes(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;
        var listOfValuesFirstMatrix = new List<int[]>
        {
            new int[3] { 1, 2, 3 },
            new int[3] { 4, 5, 6 },
            new int[3] { 7, 8, 9 },
        };

        var listOfValuesSecondMatrix = new List<int[]>
        {
            new int[1] { 1 },
            new int[1] { 2 },
            new int[1] { 3 },
        };

        var listOfCorrectValues = new List<int[]>
        {
            new int[1] { 14 },
            new int[1] { 32 },
            new int[1] { 50 },
        };

        var firstMatrix = Matrix.Create(3, 3, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(3, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(3, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double mathExpectation = GetMathExpectation(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, mathExpectation);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 3x3 и 3x1:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(mathExpectation);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyOfNotSquareMatrices(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;

        var listOfValuesFirstMatrix = new List<int[]>
        {
            new int[5] { 1, 2, 3, 4, 5 },
            new int[5] { 6, 7, 8, 9, 10 },
        };

        var listOfValuesSecondMatrix = new List<int[]>
        {
            new int[1] { 1 },
            new int[1] { 2 },
            new int[1] { 3 },
            new int[1] { 4 },
            new int[1] { 5 },
        };

        var listOfCorrectValues = new List<int[]>
        {
            new int[1] { 55 },
            new int[1] { 130 },
        };

        var firstMatrix = Matrix.Create(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double mathExpectation = GetMathExpectation(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, mathExpectation);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 2x5 и 5x1:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(mathExpectation);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyMatricesWithNegativeNumbers(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;

        var listOfValuesFirstMatrix = new List<int[]>
        {
            new int[5] { 1, 2, -3, 4, 5 },
            new int[5] { 6, -7, 8, 9, 10 },
        };

        var listOfValuesSecondMatrix = new List<int[]>
        {
            new int[1] { 1 },
            new int[1] { 2 },
            new int[1] { 3 },
            new int[1] { -4 },
            new int[1] { 5 },
        };

        var listOfCorrectValues = new List<int[]>
        {
            new int[1] { 5 },
            new int[1] { 30 },
        };

        var firstMatrix = Matrix.Create(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double mathExpectation = GetMathExpectation(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, mathExpectation);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 2x5 и 5x1 с отрицательными числами:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(mathExpectation);
        streamForWrite.Write(" Среднеквадратичное отклонение: ");
        streamForWrite.WriteLine(standardDeviation);
        streamForWrite.Close();
    }

    private static void MultiplyMatricesWithZero(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;
        var listOfValuesFirstMatrix = new List<int[]> 
        {
            new int[5] { 1, 2, 0, 4, 5 },
            new int[5] { 6, 0, 8, 9, 10 },
        };

        var listOfValuesSecondMatrix = new List<int[]>
        {
            new int[1] { 1 },
            new int[1] { 2 },
            new int[1] { 0 },
            new int[1] { 0 },
            new int[1] { 5 },
        };

        var listOfCorrectValues = new List<int[]> 
        {
            new int[1] { 30 },
            new int[1] { 56 },
        };

        var firstMatrix = Matrix.Create(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);

        var arrayForStandardDeviation = new double[n];
        double mathExpectation = GetMathExpectation(firstMatrix, secondMatrix, correctMatrix,
                                              arrayForStandardDeviation, n, sizeThreads);
        var standardDeviation = GetStandartDeviation(n, arrayForStandardDeviation, mathExpectation);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.WriteLine("Математическое ожидание и среднеквадратичное отклонение для матриц размера 2x5 и 5x1 с нулями:");
        streamForWrite.Write("Матемматическое ожидание: ");
        streamForWrite.Write(mathExpectation);
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
        MultiplyMatricesOfSizeThreeByThree(filePath);
        MultiplyMatricesOfBigSize(filePath);
        MultiplyMatricesOfDifferentSizes(filePath);
        MultiplyOfNotSquareMatrices(filePath);
        MultiplyMatricesWithNegativeNumbers(filePath);
        MultiplyMatricesWithZero(filePath);
    }
}
