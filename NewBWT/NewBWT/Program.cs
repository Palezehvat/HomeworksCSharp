namespace Sort;

using System;
using System.Text;

enum WhichStringIsBigger
{
    First,
    Second,
    Same,
    Error
}
class Program
{
    public const int NumberOfCharacters = 65536;

    // A method of comparing two rows using indexes indicating their beginning
    public static WhichStringIsBigger CompareStrings(string stringToBWT, int positionFirst, int positionSecond)
    {
        int cyclicalFirstStringPosition = positionFirst;
        int cyclicalSecondStringPosition = positionSecond;
        int comparedSymbolsCount = 0;
        while (comparedSymbolsCount < stringToBWT.Length)
        {
            if (stringToBWT[cyclicalFirstStringPosition % stringToBWT.Length] > stringToBWT[cyclicalSecondStringPosition % stringToBWT.Length])
            {
                return WhichStringIsBigger.First;
            }
            else if (stringToBWT[cyclicalFirstStringPosition % stringToBWT.Length] < stringToBWT[cyclicalSecondStringPosition % stringToBWT.Length])
            {
                return WhichStringIsBigger.Second;
            }

            ++cyclicalFirstStringPosition;
            ++cyclicalSecondStringPosition;
            ++comparedSymbolsCount;
        }
        return WhichStringIsBigger.Same;
    }

    //  Sorting by inserts
    public static void InsertSort(string stringToBWT, int[] arrayPositions, int startArray, int endArray)
    {
        for (int i = startArray + 1; i <= endArray; ++i)
        {
            int j = i;
            while (j >= startArray + 1 && CompareStrings(stringToBWT, (arrayPositions[j - 1] + 1) % arrayPositions.Length,
                  (arrayPositions[j] + 1) % arrayPositions.Length) == WhichStringIsBigger.First)
            {
                (arrayPositions[j - 1], arrayPositions[j]) = (arrayPositions[j], arrayPositions[j - 1]);

                --j;
            }
        }
    }

    // Finding a reference element
    public static int Partition(string stringToBWT, int[] arrayPositions, int startArray, int endArray)
    {
        int pivot = arrayPositions[endArray];
        int i = startArray;

        for (int j = startArray; j < endArray; ++j)
        {
            WhichStringIsBigger result = CompareStrings(stringToBWT, (arrayPositions[j] + 1) % arrayPositions.Length, (pivot + 1) % arrayPositions.Length);
            if (result == WhichStringIsBigger.Second || result == WhichStringIsBigger.Same)
            {
                (arrayPositions[j], arrayPositions[i]) = (arrayPositions[i], arrayPositions[j]);

                ++i;
            }
        }
        (arrayPositions[i], arrayPositions[endArray]) = (arrayPositions[endArray], arrayPositions[i]);
        return i;
    }

    // Quick sort, which sorts an array of indexes by comparing rows obtained by cyclic permutations
    public static void QSort(string stringToBWT, int[] arrayPositions, int startArray, int endArray)
    {
        if (endArray - startArray + 1 <= 10)
        {
            InsertSort(stringToBWT, arrayPositions, startArray, endArray);
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
    public static (string result, int firstPosition) BwtConvert(string stringToBWT)
    {
        var arrayPositions = new int[stringToBWT.Length];
        for (int i = 0; i < stringToBWT.Length; ++i)
        {
            arrayPositions[i] = i;
        }

        QuickSort(stringToBWT, arrayPositions);

        var stringAfterBWT = new StringBuilder(stringToBWT);
        for (int i = 0; i < stringToBWT.Length; ++i)
        {
            stringAfterBWT[i] = stringToBWT[arrayPositions[i]];
        }

        int firstPosition = 0;
        for (int i = 0; i < stringToBWT.Length; ++i)
        {
            if (arrayPositions[i] == stringToBWT.Length - 1)
            {
                firstPosition = i;
                return (stringAfterBWT.ToString(), firstPosition);
            }
        }
        return (stringAfterBWT.ToString(), firstPosition);
    }

    // The function receives a string after the Burrows-Wheeler algorithm as input, returns a string before the Burrows-Wheeler algorithm
    public static string BWTReverseСonvert(string stringAfterBWT, int firstPosition)
    {
        var arraySymbols = new int[NumberOfCharacters];
        var arrayPreCalculationTable = new int[stringAfterBWT.Length];

        for (int i = 0; i < stringAfterBWT.Length; ++i)
        {
            ++arraySymbols[stringAfterBWT[i]];
        }

        int summary = 0;
        for (int i = 0; i < NumberOfCharacters; i++)
        {
            summary = summary + arraySymbols[i];
            arraySymbols[i] = summary - arraySymbols[i];
        }

        for (int i = 0; i < stringAfterBWT.Length; ++i)
        {
            int j = i - 1;
            while (j >= 0)
            {
                if (stringAfterBWT[i] == stringAfterBWT[j])
                {
                    arrayPreCalculationTable[i]++;
                }
                --j;
            }
        }
        var stringBeforeBWT = new StringBuilder(stringAfterBWT);
        int positionForNewChar = stringBeforeBWT.Length - 1;
        stringBeforeBWT[positionForNewChar] = stringAfterBWT[firstPosition];
        --positionForNewChar;
        int sum = arrayPreCalculationTable[firstPosition] + arraySymbols[stringAfterBWT[firstPosition]];
        while (positionForNewChar >= 0)
        {
            stringBeforeBWT[positionForNewChar] = stringAfterBWT[sum];
            sum = arrayPreCalculationTable[sum] + arraySymbols[stringAfterBWT[sum]];
            --positionForNewChar;
        }
        return stringBeforeBWT.ToString();
    }

    // Checking compression and unzipping by the Burrows-Wheeler algorithm
    public static bool TestBWT()
    {
        string stringToTest = "ABACABA";
        (var stringAfterBWT, var firstPosition) = BwtConvert(stringToTest);
        if (stringAfterBWT != "BCABAAA")
        {
            return false;
        }
        return BWTReverseСonvert(stringAfterBWT, firstPosition) == "ABACABA";
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
        (var returnedStringFromBWT, var firstPosition) = BwtConvert(stringToBWT);
        Console.WriteLine("String after BWT");
        Console.WriteLine(returnedStringFromBWT);
        var stringBeforeBWT = BWTReverseСonvert(returnedStringFromBWT, firstPosition);
        Console.WriteLine(stringBeforeBWT);
    }
}
