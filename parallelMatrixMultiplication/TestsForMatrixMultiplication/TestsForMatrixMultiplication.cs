namespace TestsForMatrixMultiplication;

using parallelMatrixMultiplication;

public class Tests
{
    [Test]
    public void MultiplyMatricesOfSizeThreeByThree()
    {
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

        var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
        Assert.True(Matrix.AreEquals(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyMatricesOfBigSize()
    {
        var listOfValuesFirstMatrix = new List<int[]> {};
        var listOfValuesSecondMatrix = new List<int[]> {};
        var listOfCorrectValues = new List<int[]> {};

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

        var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
        Assert.True(Matrix.AreEquals(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyMatricesOfDifferentSizes()
    {
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

        var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
        Assert.True(Matrix.AreEquals(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyOfNotSquareMatrices()
    {
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

        var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
        Assert.True(Matrix.AreEquals(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyMatricesWithNegativeNumbers()
    {
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

        var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
        Assert.True(Matrix.AreEquals(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyMatricesWithZero()
    {
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

        var resultMatrix = Matrix.Multiply(firstMatrix, secondMatrix);
        Assert.True(Matrix.AreEquals(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyMatricesWithWrongData()
    {
        var listOfCorrectValues = new List<int[]> 
        {
            new int[1] { 30 },
            new int[1] { 56 },
        };

        var correctMatrix = Matrix.Create(2, 1, listOfCorrectValues);
        Assert.Throws<InvalidFileException>(() => Matrix.Multiplication(Path.Combine(TestContext.CurrentContext.TestDirectory,
                                           "TestsForMatrix", "firstCorrectMatrix.txt"),
                                           Path.Combine(TestContext.CurrentContext.TestDirectory,
                                           "TestsForMatrix", "incorrectMatrix.txt"),
                                           Path.Combine(TestContext.CurrentContext.TestDirectory,
                                           "TestsForMatrix", "resultMatrix.txt")));
    }
}