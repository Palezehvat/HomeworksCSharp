namespace Sort;
using System;
using System.Text;

enum WichStringIsBigger
{
    first,
    second,
    same,
    error
}
class Program
{
    // A method of comparing two rows using indexes indicating their beginning
    public static WichStringIsBigger ComprassionStrings(string stringToBWT, int positionFirst, int positionSecond)
    {
        int i = positionFirst;
        int j = positionSecond;
        int k = 0;
        while (k < stringToBWT.Length)
        {
            if (stringToBWT[i % stringToBWT.Length] > stringToBWT[j % stringToBWT.Length])
            {
                return WichStringIsBigger.first;
            } 
            else if (stringToBWT[i % stringToBWT.Length] < stringToBWT[j % stringToBWT.Length])
            {
                return WichStringIsBigger.second;
            }

            ++i;
            ++j;
            ++k;
        }
        return WichStringIsBigger.same;
    }

    //  Sorting by inserts
    public static void InsertSort(string stringToBWT, int[] arrayPositions, int startArray, int endArray)
    {
        for (int i = startArray + 1; i <= endArray; ++i)
        {
            int j = i;
            while (j >= startArray + 1 && ComprassionStrings(stringToBWT, (arrayPositions[j - 1] + 1) % arrayPositions.Length, (arrayPositions[j] + 1) % arrayPositions.Length) == WichStringIsBigger.first)
            {
                int copyElement = arrayPositions[j];
                arrayPositions[j] = arrayPositions[j - 1];
                arrayPositions[j - 1] = copyElement;
                --j;
            }
        }
    }

    // Finding a reference element
    public static int Partition(string stringToBWT, int[] arrayPositions, int startArray, int endArray)
    {
        int pivot = arrayPositions[endArray];
        int i = startArray;
        int copyElement = 0;

        for (int j = startArray; j < endArray; ++j)
        {
            WichStringIsBigger result = ComprassionStrings(stringToBWT, (arrayPositions[j] + 1) % arrayPositions.Length, (pivot + 1) % arrayPositions.Length);
            if (result == WichStringIsBigger.second || result == WichStringIsBigger.same)
            {
                copyElement = arrayPositions[j];
                arrayPositions[j] = arrayPositions[i];
                arrayPositions[i] = copyElement;
                ++i;
            }
        }

        copyElement = arrayPositions[i];
        arrayPositions[i] = arrayPositions[endArray];
        arrayPositions[endArray] = copyElement;
        return i;
    }

    // Quick sort, which sorts an array of indexes by comparing rows obtained by cyclic permutations
    public static void QSort(string stringToBWT, int[] arrayPositions, int startArray, int endArray)
    {
        if (endArray - startArray + 1 <= 10)
        {
            InsertSort(stringToBWT, arrayPositions,startArray, endArray);
            return;
        }
        if (startArray < endArray)
        {
            int positionElement = Partition(stringToBWT, arrayPositions, startArray, endArray);
            QSort(stringToBWT, arrayPositions, startArray, positionElement - 1);
            QSort(stringToBWT, arrayPositions, positionElement + 1, endArray);
        }
    }

    // Shell for running quick sort
    public static void QuickSort(string stringToBWT, int[] arrayPositions)
    {
        QSort(stringToBWT, arrayPositions, 0, arrayPositions.Length - 1);
    }

    // String compression by the Burrows-Wheeler algorithm
    public static string CompressionBWT(string stringToBWT)
    {
        var arrayPositions = new int[stringToBWT.Length];
        for (int i = 0; i < stringToBWT.Length; ++i)
        {
            arrayPositions[i] = i;
        }

        QuickSort(stringToBWT, arrayPositions);

        StringBuilder stringAfterBWT = new StringBuilder(stringToBWT);
        for (int i = 0; i < stringToBWT.Length; ++i)
        {
           stringAfterBWT[i] = stringToBWT[arrayPositions[i]];
        }
        return stringAfterBWT.ToString(); 
    }

    // Checking compression and unzipping by the Burrows-Wheeler algorithm
    public static bool TestBWT()
    {
        string stringToTest = "ABACABA";
        var stringOutToTest = CompressionBWT(stringToTest);
        if (stringOutToTest != "BCABAAA")
        {
            return false;
        }
        // return Expansion() == "ABACABA";
        return true;
    }

    public static void Main(string[] args)
    {
        if (TestBWT())
        {
            Console.WriteLine("All tests correct");
        }
        else
        {
            Console.WriteLine("Some problems with tests...");
            return;
        }
        Console.WriteLine("Input string");
        var stringToBWT = Console.ReadLine();
        if (stringToBWT == null)
        {
            Console.WriteLine("You input null string or your input is not correct");
            return;
        }
        var returnedStringFromBWT = CompressionBWT(stringToBWT);
        Console.WriteLine("String after BWT");
        Console.WriteLine(returnedStringFromBWT);
    }
}