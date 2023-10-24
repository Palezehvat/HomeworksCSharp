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

        return Math.Round(Math.Sqrt(summaryForStandartDeviation / (n - 1)), 3);
    }

    private static double GetMathExpectation(int[][] firstMatrix, int[][] secondMatrix,
                                       double[] arrayForStandardDeviation, int n, bool isConsistentMultiply)
    {
        double summary = 0;
        for (int i = 0; i < n; i++)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (isConsistentMultiply)
            {
                var resultMatrix = Matrix.ConsistentMultiply(firstMatrix, secondMatrix);
            }
            else
            {
                var resultMatrix = Matrix.ParallelMultiply(firstMatrix, secondMatrix);
            }
            stopWatch.Stop();
            arrayForStandardDeviation[i] = stopWatch.ElapsedMilliseconds;
            summary += arrayForStandardDeviation[i];
        }
        return summary / n;
    }

    private static void MultiplyMatricesOfSizeTwoHundredAndFiftyByTwoHundredAndFifty(string filePath)
    {
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };

        for (int i = 0; i < 250; i++)
        {
            listOfValuesFirstMatrix.Add(new int[250]);
            listOfValuesSecondMatrix.Add(new int[250]);
            for (int j = 0; j < 250; ++j)
            {
                listOfValuesFirstMatrix[i][j] = 1;
                listOfValuesSecondMatrix[i][j] = 1;
            }
        }

        var firstMatrix = Matrix.Create(250, 250, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(250, 250, listOfValuesSecondMatrix);

        var arrayForStandardDeviationMultiThreaded = new double[n];
        var arrayForStandardDeviationSingleThreaded = new double[n];
        double mathExpectationMultiThreaded = GetMathExpectation(firstMatrix, secondMatrix,
                                              arrayForStandardDeviationMultiThreaded, n, false);
        double mathExpectationSingleThreaded = GetMathExpectation(firstMatrix, secondMatrix,
                                              arrayForStandardDeviationSingleThreaded, n, true);
        var standardDeviationMultiThreaded = GetStandartDeviation(n, arrayForStandardDeviationMultiThreaded, mathExpectationMultiThreaded);
        var standardDeviationSingleThreaded = GetStandartDeviation(n, arrayForStandardDeviationSingleThreaded, mathExpectationSingleThreaded);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.Write("250x250\nи 250x250\t\t");
        streamForWrite.Write(mathExpectationMultiThreaded);
        streamForWrite.Write("\t\t      ");
        streamForWrite.Write(standardDeviationMultiThreaded);
        streamForWrite.Write("          ");
        streamForWrite.Write(mathExpectationSingleThreaded);
        streamForWrite.Write("    ");
        streamForWrite.WriteLine(standardDeviationSingleThreaded);
        streamForWrite.Write('\n');
        streamForWrite.Close();
    }

    private static void MultiplyMatricesOfBigSize(string filePath)
    {
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };

        for (int i = 0; i < 10000; i++)
        {
            listOfValuesFirstMatrix.Add(new int[10000]);
            listOfValuesSecondMatrix.Add(new int[1]);
            for (int j = 0; j < 10000; ++j)
            {
                listOfValuesFirstMatrix[i][j] = 1;
            }
            listOfValuesSecondMatrix[i][0] = 1;
        }

        var firstMatrix = Matrix.Create(10000, 10000, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(10000, 1, listOfValuesSecondMatrix);

        var arrayForStandardDeviationMultiThreaded = new double[n];
        var arrayForStandardDeviationSingleThreaded = new double[n];
        double mathExpectationMultiThreaded = GetMathExpectation(firstMatrix, secondMatrix,
                                              arrayForStandardDeviationMultiThreaded, n, false);
        double mathExpectationSingleThreaded = GetMathExpectation(firstMatrix, secondMatrix,
                                              arrayForStandardDeviationSingleThreaded, n, true);
        var standardDeviationMultiThreaded = GetStandartDeviation(n, arrayForStandardDeviationMultiThreaded, mathExpectationMultiThreaded);
        var standardDeviationSingleThreaded = GetStandartDeviation(n, arrayForStandardDeviationSingleThreaded, mathExpectationSingleThreaded);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.Write("10000x10000\nи 10000x1\t\t");
        streamForWrite.Write(mathExpectationMultiThreaded);
        streamForWrite.Write("\t\t      ");
        streamForWrite.Write(standardDeviationMultiThreaded);
        streamForWrite.Write("         ");
        streamForWrite.Write(mathExpectationSingleThreaded);
        streamForWrite.Write("   ");
        streamForWrite.WriteLine(standardDeviationSingleThreaded);
        streamForWrite.Write('\n');
        streamForWrite.Close();
    }

    private static void MultiplyMatricesOfFiveHundredOnFiveHundredSize(string filePath)
    {
        var listOfValuesFirstMatrix = new List<int[]> { };
        var listOfValuesSecondMatrix = new List<int[]> { };

        for (int i = 0; i < 500; i++)
        {
            listOfValuesFirstMatrix.Add(new int[500]);
            listOfValuesSecondMatrix.Add(new int[500]);
            for (int j = 0; j < 500; ++j)
            {
                listOfValuesFirstMatrix[i][j] = 1;
                listOfValuesSecondMatrix[i][j] = 1;
            }
        }

        var firstMatrix = Matrix.Create(500, 500, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.Create(500, 500, listOfValuesSecondMatrix);

        var arrayForStandardDeviationMultiThreaded = new double[n];
        var arrayForStandardDeviationSingleThreaded = new double[n];
        double mathExpectationMultiThreaded = GetMathExpectation(firstMatrix, secondMatrix,
                                              arrayForStandardDeviationMultiThreaded, n, false);
        double mathExpectationSingleThreaded = GetMathExpectation(firstMatrix, secondMatrix,
                                              arrayForStandardDeviationSingleThreaded, n, true);
        var standardDeviationMultiThreaded = GetStandartDeviation(n, arrayForStandardDeviationMultiThreaded, mathExpectationMultiThreaded);
        var standardDeviationSingleThreaded = GetStandartDeviation(n, arrayForStandardDeviationSingleThreaded, mathExpectationSingleThreaded);
        var streamForWrite = new StreamWriter(filePath, true);
        streamForWrite.Write("500x500\nи 500x500\t\t");
        streamForWrite.Write(mathExpectationMultiThreaded);
        streamForWrite.Write("\t\t      ");
        streamForWrite.Write(standardDeviationMultiThreaded);
        streamForWrite.Write("         ");
        streamForWrite.Write(mathExpectationSingleThreaded);
        streamForWrite.Write("    ");
        streamForWrite.WriteLine(standardDeviationSingleThreaded);
        streamForWrite.Write('\n');
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
        streamForWrite.WriteLine("-----------------------------------------------------------------------------");
        streamForWrite.Write("Размеры матриц\tμ+parallelization\t" +
                             "σ+parallelization" +
                             "\tμ\tσ\n");
        streamForWrite.WriteLine("-----------------------------------------------------------------------------");
        streamForWrite.Close();
        MultiplyMatricesOfSizeTwoHundredAndFiftyByTwoHundredAndFifty(filePath);
        MultiplyMatricesOfBigSize(filePath);
        MultiplyMatricesOfFiveHundredOnFiveHundredSize(filePath);
    }
}
