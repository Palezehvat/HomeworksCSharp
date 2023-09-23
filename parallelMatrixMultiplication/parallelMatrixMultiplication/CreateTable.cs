using System.Diagnostics;

using parallelMatrixMultiplication;

/// <summary>
/// A class for measuring the standard deviation and mathematical expectation 
/// </summary>
public static class GetStandartDeviationAndMinValue
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
            var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix);
            stopWatch.Stop();
            arrayForStandardDeviation[i] = (double)stopWatch.ElapsedMilliseconds / 1000;
            summary += arrayForStandardDeviation[i];
        }
        return summary / n;
    }

    private static void MultiplyingMatricesOfSizeThreeByThree(string filePath)
    {
        var sizeThreads = Environment.ProcessorCount;
        var listOfValues = new List<int[]> { };
        var listOfCorrectValues = new List<int[]> { };

        listOfValues.Add(new int[3] { 1, 2, 3 });
        listOfValues.Add(new int[3] { 4, 5, 6 });
        listOfValues.Add(new int[3] { 7, 8, 9 });

        listOfCorrectValues.Add(new int[3] { 30, 36, 42 });
        listOfCorrectValues.Add(new int[3] { 66, 81, 96 });
        listOfCorrectValues.Add(new int[3] { 102, 126, 150 });

        var firstMatrix = Matrix.Create(3, 3, listOfValues);
        var secondMatrix = Matrix.Create(3, 3, listOfValues);
        var correctMatrix = Matrix.Create(3, 3, listOfCorrectValues);

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
        var sizeThreads = Environment.ProcessorCount;
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };
        var listOfCorrectValues = new List<int[]> { };

        listOfValuesFirstMatrix.Add(new int[3] { 1, 2, 3 });
        listOfValuesFirstMatrix.Add(new int[3] { 4, 5, 6 });
        listOfValuesFirstMatrix.Add(new int[3] { 7, 8, 9 });

        listOfValuesSecondMatrix.Add(new int[1] { 1 });
        listOfValuesSecondMatrix.Add(new int[1] { 2 });
        listOfValuesSecondMatrix.Add(new int[1] { 3 });

        listOfCorrectValues.Add(new int[1] { 14 });
        listOfCorrectValues.Add(new int[1] { 32 });
        listOfCorrectValues.Add(new int[1] { 50 });

        var firstMatrix = Matrix.Create(3, 3, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(3, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(3, 1, listOfCorrectValues);

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
        var sizeThreads = Environment.ProcessorCount;
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };
        var listOfCorrectValues = new List<int[]> { };

        listOfValuesFirstMatrix.Add(new int[5] { 1, 2, 3, 4, 5 });
        listOfValuesFirstMatrix.Add(new int[5] { 6, 7, 8, 9, 10 });

        listOfValuesSecondMatrix.Add(new int[1] { 1 });
        listOfValuesSecondMatrix.Add(new int[1] { 2 });
        listOfValuesSecondMatrix.Add(new int[1] { 3 });
        listOfValuesSecondMatrix.Add(new int[1] { 4 });
        listOfValuesSecondMatrix.Add(new int[1] { 5 });

        listOfCorrectValues.Add(new int[1] { 55 });
        listOfCorrectValues.Add(new int[1] { 130 });

        var firstMatrix = Matrix.Create(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);

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
        var sizeThreads = Environment.ProcessorCount;
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };
        var listOfCorrectValues = new List<int[]> { };

        listOfValuesFirstMatrix.Add(new int[5] { 1, 2, -3, 4, 5 });
        listOfValuesFirstMatrix.Add(new int[5] { 6, -7, 8, 9, 10 });

        listOfValuesSecondMatrix.Add(new int[1] { 1 });
        listOfValuesSecondMatrix.Add(new int[1] { 2 });
        listOfValuesSecondMatrix.Add(new int[1] { 3 });
        listOfValuesSecondMatrix.Add(new int[1] { -4 });
        listOfValuesSecondMatrix.Add(new int[1] { 5 });

        listOfCorrectValues.Add(new int[1] { 5 });
        listOfCorrectValues.Add(new int[1] { 30 });

        var firstMatrix = Matrix.Create(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);

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
        var sizeThreads = Environment.ProcessorCount;
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };
        var listOfCorrectValues = new List<int[]> { };

        listOfValuesFirstMatrix.Add(new int[5] { 1, 2, 0, 4, 5 });
        listOfValuesFirstMatrix.Add(new int[5] { 6, 0, 8, 9, 10 });

        listOfValuesSecondMatrix.Add(new int[1] { 1 });
        listOfValuesSecondMatrix.Add(new int[1] { 2 });
        listOfValuesSecondMatrix.Add(new int[1] { 0 });
        listOfValuesSecondMatrix.Add(new int[1] { 0 });
        listOfValuesSecondMatrix.Add(new int[1] { 5 });

        listOfCorrectValues.Add(new int[1] { 30 });
        listOfCorrectValues.Add(new int[1] { 56 });

        var firstMatrix = Matrix.Create(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);

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
