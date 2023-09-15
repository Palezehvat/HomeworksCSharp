namespace TestsForMatrixMultiplication;

using parallelMatrixMultiplication;

public class Tests
{
    [Test]
    public void MultiplyingMatricesOfSizeThreeByThree()
    {
        var sizeThreads = 3;
        var listOfValues = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};
        var listOfCorrectValues = new List<int> {30, 36, 42, 66, 81, 96, 102, 126, 150};
        var firstMatrix = Matrix.CreateMatrix(3, 3, listOfValues);
        var secondMatrix = Matrix.CreateMatrix(3, 3, listOfValues);
        var correctMatrix = Matrix.CreateMatrix(3, 3, listOfCorrectValues);

        var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
        Assert.True(Matrix.MatrixComparison(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyingMatricesOfBigSize()
    {
        var sizeThreads = 50;

        var listOfValuesFirstMatrix = new List<int> {};
        var listOfValuesSecondMatrix = new List<int> {};
        var listOfCorrectValues = new List<int> {};

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

        var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
        Assert.True(Matrix.MatrixComparison(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyingMatricesOfDifferentSizes()
    {
        var sizeThreads = 3;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 3 };
        var listOfCorrectValues = new List<int> { 14, 32, 50 };
        var firstMatrix = Matrix.CreateMatrix(3, 3, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(3, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(3, 1, listOfCorrectValues);

        var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
        Assert.True(Matrix.MatrixComparison(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyingOfNotSquareMatrices()
    {
        var sizeThreads = 10;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 3, 4, 5 };
        var listOfCorrectValues = new List<int> { 55, 130 };
        var firstMatrix = Matrix.CreateMatrix(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);

        var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
        Assert.True(Matrix.MatrixComparison(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyingMatricesWithNegativeNumbers()
    {
        var sizeThreads = 10;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, -3, 4, 5, 6, -7, 8, 9, 10 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 3, -4, 5 };
        var listOfCorrectValues = new List<int> { 5, 30 };
        var firstMatrix = Matrix.CreateMatrix(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);

        var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
        Assert.True(Matrix.MatrixComparison(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyingMatricesWithZero()
    {
        var sizeThreads = 10;
        var listOfValuesFirstMatrix = new List<int> { 1, 2, 0, 4, 5, 6, 0, 8, 9, 10 };
        var listOfValuesSecondMatrix = new List<int> { 1, 2, 0, 0, 5 };
        var listOfCorrectValues = new List<int> { 30, 56 };
        var firstMatrix = Matrix.CreateMatrix(2, 5, listOfValuesFirstMatrix);
        var secondMatrix = Matrix.CreateMatrix(5, 1, listOfValuesSecondMatrix);
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);

        var resultMatrix = Matrix.MatrixMultiplication(firstMatrix, secondMatrix, sizeThreads);
        Assert.True(Matrix.MatrixComparison(resultMatrix, correctMatrix));
    }

    [Test]
    public void MultiplyingMatricesWithWrongData()
    {
        var sizeThreads = 10;
        var listOfCorrectValues = new List<int> { 30, 56 };
        var correctMatrix = Matrix.CreateMatrix(2, 1, listOfCorrectValues);
        Assert.Throws<InvalidFileException>(() => Matrix.MatrixMultiplicationControlFunction(Path.Combine(TestContext.CurrentContext.TestDirectory,
                                           "TestsForMatrix", "firstCorrectMatrix.txt"),
                                           Path.Combine(TestContext.CurrentContext.TestDirectory,
                                           "TestsForMatrix", "incorrectMatrix.txt"),
                                           Path.Combine(TestContext.CurrentContext.TestDirectory,
                                           "TestsForMatrix", "resultMatrix.txt"),
                                           sizeThreads
                                           ));
    }
}