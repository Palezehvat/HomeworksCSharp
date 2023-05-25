namespace LZW;

using Bor;

/// <summary>
/// A class for compressing and decompressing data using the LZW algorithm
/// </summary>
public static class LZWAlgorithm
{
    private static void ChangeFileNameToTheOriginalName(ref char[] newFile)
    {
        if (newFile.Length < 8)
        {
            throw new ArgumentException();
        }
        Array.Resize(ref newFile, newFile.Length - 7);
    }

    private static void ChangeFileNameToZipped(ref char[] newFile)
    {
        int sizeNewFile = newFile.Length;
        Array.Resize(ref newFile, sizeNewFile + 7);
        newFile[sizeNewFile] = '.';
        newFile[sizeNewFile + 1] = 'z';
        newFile[sizeNewFile + 2] = 'i';
        newFile[sizeNewFile + 3] = 'p';
        newFile[sizeNewFile + 4] = 'p';
        newFile[sizeNewFile + 5] = 'e';
        newFile[sizeNewFile + 6] = 'd';
    }

    private static void AddAlphabetToBor(Bor bor)
    {
        var letter = new char[1];
        for (int i = 0; i < 256; ++i)
        {
            letter[0] = (char)i;
            bor.Add(letter, 0, 0);
        }
    }

    private static bool CodeFile(string fileName, ref double compressionRatio)
    {
        char[]? newFileArray = null;
        double sizeForCompressionRatio = 0;
        try
        {
            newFileArray = new char[fileName.Length];
            Array.Copy(fileName.ToCharArray(), newFileArray, fileName.Length);
            FileInfo fileFromMain = new(fileName);
            sizeForCompressionRatio = fileFromMain.Length;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        ChangeFileNameToZipped(ref newFileArray);
        string? newFile = new string(newFileArray);
        if (newFile == null)
        {
            return false;
        }

        File.WriteAllText(newFile, string.Empty);

        var bor = new Bor();
        AddAlphabetToBor(bor);

        byte[] bufferIn = File.ReadAllBytes(fileName);
        if (bufferIn.Length == 0)
        {
            compressionRatio = 1;
            return true;
        }
        int j = 0;
        var textFromFile = new char[bufferIn.Length];
        for (int i = 0; i < bufferIn.Length; ++i)
        {
            textFromFile[j] = Convert.ToChar(bufferIn[i]);
            ++j;
        }

        // Добавление потоков в файл
        int walker = 0;
        int sizeText = textFromFile.Length;
        var fstreamNew = new FileStream(newFile, FileMode.Append);
        for (int i = 0; i < textFromFile.Length; i++)
        {
            if (!bor.Contains(textFromFile, walker, i))
            {
                var (_, flow) = bor.Add(textFromFile, walker, i);
                walker = i;
                byte[] bytes = BitConverter.GetBytes(flow);
                fstreamNew.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        // Добавление последнего потока в файл
        var flowLast = bor.ReturnFlowByCharArray(textFromFile, bufferIn.Length - 1, bufferIn.Length - 1);
        byte[] bytesLast = BitConverter.GetBytes(flowLast);
        fstreamNew.WriteAsync(bytesLast, 0, bytesLast.Length);
        fstreamNew.Close();
        var file = new FileInfo(newFile.ToString());
        double newSizeForCompressionRatio = file.Length;
        compressionRatio = newSizeForCompressionRatio / sizeForCompressionRatio;
        return true;
    }

    private static bool DecodeFile(string fileName)
    {
        byte[] bufferIn = null;
        try
        {
            bufferIn = File.ReadAllBytes(fileName);
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        int i = 0;
        var newFileArray = new char[fileName.Length];
        Array.Copy(fileName.ToCharArray(), newFileArray, fileName.Length);
        ChangeFileNameToTheOriginalName(ref newFileArray);
        string? newFile = new string(newFileArray);
        if (newFile == null)
        {
            return false;
        }

        File.WriteAllText(newFile, string.Empty);

        if (fileName.Length == 0) 
        {
            return true;
        }

        var dictionaryForDecode = new Dictionary<int, char[]>();

        for (int j = 0; j < 256; ++j)
        {
            var stringLetter = new char[1];
            stringLetter[0] = (char)j;
            dictionaryForDecode.Add(j, stringLetter);
        }

        int index = 256;
        int input = 0;
        char[] previousString = null;
        bool isFirst = true;
        while (i < bufferIn.Length)
        {
            var symbolFromArray = new byte[4];
            int pointerForSymbolsFromOneArray = 0;
            int size = i + 4;
            for (; i < size; ++i)
            {
                symbolFromArray[pointerForSymbolsFromOneArray] = bufferIn[i];
                ++pointerForSymbolsFromOneArray;
            }

            input = BitConverter.ToInt32(symbolFromArray, 0);

            if (!isFirst)
            {
                if (!dictionaryForDecode.ContainsKey(input))
                {
                    Array.Resize(ref previousString, previousString.Length + 1);
                    previousString[previousString.Length - 1] = previousString[0];
                    dictionaryForDecode.Add(index, previousString);
                }
                var stringByIndex = dictionaryForDecode[input];
                if (index != input)
                {
                    Array.Resize(ref previousString, previousString.Length + 1);
                    previousString[previousString.Length - 1] = stringByIndex[0];
                    dictionaryForDecode.Add(index, previousString);
                }
                FileStream file = File.Open(newFile, FileMode.Append);
                foreach (var symbol in stringByIndex)
                {
                    file.WriteByte((byte)symbol);
                }
                file.Close();
                previousString = stringByIndex;
                ++index;
            }
            else
            {
                var stringByIndex = dictionaryForDecode[input];
                previousString = stringByIndex;
                isFirst = false;
                FileStream file = File.Open(newFile, FileMode.Append);
                foreach (var symbol in stringByIndex)
                {
                    file.WriteByte((byte)symbol);
                }
                file.Close();
            }
        }
        return true;
    }

    /// <summary>
    /// Function for accepting data
    /// </summary>
    /// <param name="parameter">Shows decompression or compression</param>
    /// <returns>Returns whether it was executed correctly and what is the compression percentage</returns>
    public static (bool, double) LzwAlgorithm(string fileName, string parameter)
    {
        if (parameter.Length == 2 && parameter[0] == '-' && parameter[1] == 'c')
        {
            double compressionRatio = 0;
            var isCorrect = CodeFile(fileName, ref compressionRatio);
            return (isCorrect, compressionRatio);
        }
        else if (parameter.Length == 2 && parameter[0] == '-' && parameter[1] == 'u')
        {
            return (DecodeFile(fileName), 0);
        }
        else
        {
            return (false, 0);
        }
    }
}