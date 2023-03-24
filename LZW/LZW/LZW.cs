using System;
using System.Collections;
using System.IO;
using System.Text;

namespace LZW;

public class LZW
{
    private bool ChangeFileNameToTxt(ref char[] newFile)
    {
        if (newFile.Length < 8)
        {
            return false;
        }
        if (newFile[newFile.Length - 1] == 'd'
         && newFile[newFile.Length - 2] == 'e'
         && newFile[newFile.Length - 3] == 'p'
         && newFile[newFile.Length - 4] == 'p'
         && newFile[newFile.Length - 5] == 'i'
         && newFile[newFile.Length - 6] == 'z'
         && newFile[newFile.Length - 7] == '.')
        {
            Array.Resize(ref newFile, newFile.Length - 3);
            newFile[newFile.Length - 1] = 'e';
            newFile[newFile.Length - 2] = 'x';
            newFile[newFile.Length - 3] = 'e';
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ChangeFileNameToZipped(ref char[] newFile)
    {
        if (newFile.Length < 5)
        {
            return false;
        }
        if (newFile[newFile.Length - 1] == 't'
         && newFile[newFile.Length - 2] == 'x'
         && newFile[newFile.Length - 3] == 't'
         && newFile[newFile.Length - 4] == '.'
         || newFile[newFile.Length - 1] == 'e'
         && newFile[newFile.Length - 2] == 'x'
         && newFile[newFile.Length - 3] == 'e'
         && newFile[newFile.Length - 4] == '.')
        {
            Array.Resize(ref newFile, newFile.Length + 3);
            newFile[newFile.Length - 1] = 'd';
            newFile[newFile.Length - 2] = 'e';
            newFile[newFile.Length - 3] = 'p';
            newFile[newFile.Length - 4] = 'p';
            newFile[newFile.Length - 5] = 'i';
            newFile[newFile.Length - 6] = 'z';
            return true;
        }
        else
        {
            return false;
        }
    }

    private void addAlphabetToBor(Bor bor)
    {
        var letter = new char[1];
        for (int i = 0; i < 256; ++i)
        {
            letter[0] = (char)i;
            bor.Add(bor, letter, 0, 0);
        }
    }

    private bool CodeFile(string fileName, ref double compressionRatio)
    {
        char[] newFileArray = new char[fileName.Length];
        Array.Copy(fileName.ToCharArray(), newFileArray, fileName.Length);
        FileInfo file = new System.IO.FileInfo(fileName);
        double sizeForCompressionRatio = file.Length;
        bool isCorrect = ChangeFileNameToZipped(ref newFileArray);
        if (!isCorrect)
        {
            return false;
        }
        string? newFile = new string(newFileArray);
        if (newFile == null)
        {
            return false;
        }
        File.WriteAllText(newFile.ToString(), string.Empty);

        var bor = new Bor();
        addAlphabetToBor(bor);

        byte[] bufferIn = File.ReadAllBytes(fileName);
        int j = 0;
        char[] textFromFile = new char[bufferIn.Length];
        for (int i = 0; i < bufferIn.Length; ++i)
        {
            textFromFile[j] = Convert.ToChar(bufferIn[i]);
            ++j;
        }

        // Добавление потоков в файл
        int walker = 0;
        int sizeText = textFromFile.Length;
        for (int i = 0; i < textFromFile.Length; i++)
        {
            if (!bor.Contains(textFromFile, walker, i))
            {
                var (_, flow) = bor.Add(bor, textFromFile, walker, i);
                walker = i;
                byte[] bytes = BitConverter.GetBytes(flow);
                var fstreamNew = new FileStream(newFile, FileMode.Append);
                fstreamNew.WriteAsync(bytes, 0, bytes.Length);
                fstreamNew.Close();
            }
        }

        // Добавление последнего потока в файл
        var flowLast = bor.ReturnFlowByCharArray(textFromFile, bufferIn.Length - 1, bufferIn.Length - 1);
        byte[] bytesLast = BitConverter.GetBytes(flowLast);
        var LastFstreamNew = new FileStream(newFile.ToString(), FileMode.Append);
        LastFstreamNew.WriteAsync(bytesLast, 0, bytesLast.Length);
        LastFstreamNew.Close();
        file = new FileInfo(newFile.ToString());
        double newSizeForCompressionRatio = file.Length;
        compressionRatio = newSizeForCompressionRatio / sizeForCompressionRatio;
        return true;
    }

    private bool DecodeFile(string fileName)
    {
        byte[] bufferIn = File.ReadAllBytes(fileName);
        int i = 0;
        char[] newFileArray = new char[fileName.Length];
        Array.Copy(fileName.ToCharArray(), newFileArray, fileName.Length);
        bool isCorrect = ChangeFileNameToTxt(ref newFileArray);
        if (!isCorrect)
        {
            return false;
        }
        string? newFile = new string(newFileArray);
        if (newFile == null)
        {
            return false;
        }

        File.WriteAllText(newFile, string.Empty);

        var dictionaryForDecode = new Dictionary<int, char[]>();

        var letter = new char[1];
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

    public (bool, double) LzwAlgorithm(string fileName, string parametr)
    {
        if (parametr.Length == 2 && parametr[0] == '-' && parametr[1] == 'c')
        {
            double compressionRatio = 0;
            var isCorrect = CodeFile(fileName, ref compressionRatio);
            return (isCorrect, compressionRatio);
        }
        else if (parametr.Length == 2 && parametr[0] == '-' && parametr[1] == 'u')
        {
            return (DecodeFile(fileName), 0);
        }
        else
        {
            return (false, 0);
        }
    }
}