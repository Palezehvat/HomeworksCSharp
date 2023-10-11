namespace parallelMatrixMultiplication;
using System;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// A class for multiplying matrices
/// </summary>
public static class Matrix
{
    /// <summary>
    /// Function for creating matrices
    /// </summary>
    /// <param name="sizeRows">Number of rows</param>
    /// <param name="sizeColumns">Number of columns</param>
    /// <param name="numbersForMatrix">List with data</param>
    /// <returns>Returns the created matrix</returns>
    /// <exception cref="ArgumentException">Throws an exception if the number of items 
    /// in the list does not match the size of the matrix</exception>
    public static int[][] Create(int sizeRows, int sizeColumns, List<int[]> numbersForMatrix)
    {
        if (numbersForMatrix.Count * numbersForMatrix[0].Length != sizeColumns * sizeRows)
        {
            throw new ArgumentException();
        }

        var matrix = new int[sizeRows][];
        for (int i = 0; i < sizeRows; i++)
        {
            matrix[i] = new int[sizeColumns];
        }

        for (int i = 0; i < sizeRows; ++i)
        {
            for (int j = 0; j < sizeColumns; ++j)
            {
                matrix[i][j] = numbersForMatrix[i][j];
            }
        }

        return matrix;
    }

    /// <summary>
    /// The function compares matrices
    /// </summary>
    public static bool AreEquals(int[][] firstMatrix, int[][] secondMatrix)
    {
        if (firstMatrix.Length != secondMatrix.Length)
        {
            return false;
        }

        for (int i = 0; i < firstMatrix.Length; ++i)
        {
            if (firstMatrix[i].Length != secondMatrix[i].Length)
            {
                return false;
            }
            for (int j = 0; j < firstMatrix[i].Length; ++j)
            {
                if (firstMatrix[i][j] != secondMatrix[i][j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Multiplies matrices by a sequential algorithm
    /// </summary>
    /// <returns>The resulting matrix</returns>
    public static int[][] ConsistentMultiply(int[][] firstMatrix, int[][] secondMatrix)
    {
        var resultMatrix = new int[firstMatrix.Length][];

        for (int i = 0; i < firstMatrix.Length; ++i)
        {
            resultMatrix[i] = new int[secondMatrix[0].Length];
        }

        for (int i = 0; i < firstMatrix.Length; ++i)
        {
            for (int j = 0; j < secondMatrix[0].Length; ++j)
            {
                for (var k = 0; k < firstMatrix[0].Length; ++k)
                {
                    resultMatrix[i][j] += firstMatrix[i][k] * secondMatrix[k][j];
                }
            }
        }
        return resultMatrix;
    }

    private static int[][] ConvertMatrixToTransposedOne(int[][] matrix)
    {
        var transposedMatrix = new int[matrix[0].Length][];
        for (int i = 0; i < matrix[0].Length; i++)
        {
            transposedMatrix[i] = new int[matrix.Length];
        }

        for (int i = 0; i < transposedMatrix.Length; ++i)
        {
            for (int j = 0; j < transposedMatrix[i].Length; ++j)
            {
                transposedMatrix[i][j] = matrix[j][i];
            }
        }
        return transposedMatrix;
    }

    private static void Multiply(int start, int end, int[][] firstMatrix, int[][] transposedMatrix, int[][] resultMatrix)
    {
        while (start < end)
        {
            for (int i = 0; i < transposedMatrix.Length; i++)
            {
                for (int k = 0; k < transposedMatrix[0].Length; ++k)
                {    
                    resultMatrix[start][i] += firstMatrix[start][k] * transposedMatrix[i][k];
                }
            }
            start++;
        }
    }

    /// <summary>
    /// Parallel matrix multiplication function
    /// </summary>
    /// <returns>Returns the calculated matrix</returns>
    public static int[][] Multiply(int[][] firstMatrix, int[][] secondMatrix)
    {
        var resultMatrix = new int[firstMatrix.Length][];

        for(int i = 0; i < firstMatrix.Length; ++i)
        {
            resultMatrix[i] = new int[secondMatrix[0].Length];
        }

        var threadCounts = Environment.ProcessorCount;

        if (threadCounts > firstMatrix.Length)
        {
            threadCounts = firstMatrix.Length;
        }

        var numberOfRowsForThreads = firstMatrix.Length / threadCounts;
        var remains = firstMatrix.Length % threadCounts;
        var threads = new Thread[threadCounts];

        int start = 0;
        int end = 0;

        var transposedMatrix = ConvertMatrixToTransposedOne(secondMatrix);

        for (int i = 0; i < threadCounts; i++)
        {
            end = i == 0 ? numberOfRowsForThreads + remains : end + numberOfRowsForThreads;
            int localStart = start;
            int localEnd = end;
            threads[i] = new Thread(() => Multiply(localStart, localEnd, firstMatrix, transposedMatrix, resultMatrix));
            start = end;
        }
        
        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return resultMatrix;
    }

    private static int[][] ReadFromFile(string filePath)
    {
        var allLines = File.ReadAllLines(filePath);
        var matrix = new int[allLines.Length][];
        var sizeColumns = 0;

        for (int i = 0; i < matrix.Length; i++)
        {
            var stringOfDigits = allLines[i].Trim().Split(' ');
            matrix[i] = new int[stringOfDigits.Length];
            var sizeLine = 0;
            for (int j = 0; j < stringOfDigits.Length; ++j)
            {
                var result = 0;
                bool isCorrectResult = int.TryParse(stringOfDigits[j].Trim(), out result);
                if (!isCorrectResult)
                {
                    throw new InvalidFileException();
                }
                matrix[i][j] = result;
                if (i == 0)
                {
                    sizeColumns++;
                }
                sizeLine++;
            }
            if (sizeLine != sizeColumns)
            {
                throw new InvalidFileException();
            }
        }
        return matrix;
    }

    private static void WriteResultToFile(string filePath, int[][] matrix)
    {
        var streamForWrite = new StreamWriter(filePath);
        for (int i = 0; i < matrix.Length; ++i)
        {
            for (int j = 0; j < matrix[i].Length; ++j)
            {
                streamForWrite.Write(matrix[i][j]);
                streamForWrite.Write(' ');
            }
            streamForWrite.Write('\n');
        }
        streamForWrite.Close();
    }

    /// <summary>
    /// A function that receives files with matrices at the input, and a calculated matrix at the output
    /// </summary>
    public static void Multiplication(string firstFile, string secondFile, string resultFile)
    {
        var firstMatrix = ReadFromFile(firstFile);
        var secondMatrix = ReadFromFile(secondFile);
        var resultMatrix = Multiply(firstMatrix, secondMatrix);
        WriteResultToFile(resultFile, resultMatrix);
    }

    /// <summary>
    /// Multiplies matrices by successive multiplication
    /// </summary>
    public static long CompareMultiplication(string firstFile, string secondFile)
    {
        var firstMatrix = ReadFromFile(firstFile);
        var secondMatrix = ReadFromFile(secondFile);

        var stopwatchParallelMultiplication = new Stopwatch();
        stopwatchParallelMultiplication.Start();
        var resultMatrixFromParallelMultiplication = Multiply(firstMatrix, secondMatrix);
        stopwatchParallelMultiplication.Stop();
        
        var result = stopwatchParallelMultiplication.ElapsedMilliseconds;

        var stopwatchConsistentMultiplication = new Stopwatch();
        stopwatchConsistentMultiplication.Start();
        var resultMatrixFromConsistentMultipliaction = ConsistentMultiply(firstMatrix, secondMatrix);
        stopwatchConsistentMultiplication.Stop();

        result -= stopwatchConsistentMultiplication.ElapsedMilliseconds;

        if (!AreEquals(resultMatrixFromConsistentMultipliaction, resultMatrixFromParallelMultiplication))
        {
            throw new MultiplyException();
        }

        return result;
    }
}