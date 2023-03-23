using System;
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
            newFile[newFile.Length - 1] = 't';
            newFile[newFile.Length - 2] = 'x';
            newFile[newFile.Length - 3] = 't';
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

    private bool CodeFile(string fileName)
    {
        char[] newFileArray = new char[fileName.Length];
        Array.Copy(fileName.ToCharArray(), newFileArray, fileName.Length);
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

        // Запись алфавита в бор
        byte[] bufferIn = File.ReadAllBytes(fileName);
        int j = 0;
        char[] textFromFile = new char[bufferIn.Length];
        char[] firstAddedAlphabet = new char[bufferIn.Length];
        var fakeBor = new Bor();
        var bor = new Bor();
        int pointer = 0;
        for (byte i = 0; i < bufferIn.Length; ++i)
        {
            textFromFile[j] = Convert.ToChar(bufferIn[i]);
            var (isFirstAdded, _) = bor.Add(bor, textFromFile, j, j);
            var (_, _) = fakeBor.Add(bor, textFromFile, j, j);
            if (isFirstAdded)
            {
                firstAddedAlphabet[pointer] = textFromFile[j];
                ++pointer;
            }
            ++j;
        }

        // Проход LZW алгоритма по строке
        int walker = 0;
        for (int i = 0; i < textFromFile.Length; i++)
        {
            if (!bor.Contains(textFromFile, walker, i))
            {
                var (_, _) = fakeBor.Add(bor, textFromFile, walker, i);
                walker = i;
            }
        }

        int maxFlow = fakeBor.HowManyStringsInBor();
        fakeBor = null;

        // Добавление размера алфавита + символы + ' '
        Array.Resize(ref firstAddedAlphabet, pointer);
        char[] alphabet = pointer.ToString().ToCharArray();
        int lastAlphabetSize = alphabet.Length;
        char[] maxFlowArray = maxFlow.ToString().ToCharArray();
        Array.Resize(ref alphabet, pointer + alphabet.Length + 2 + maxFlowArray.Length);
        alphabet[lastAlphabetSize] = ' ';
        int p = 0;
        int l = lastAlphabetSize + 1;
        while (p < maxFlowArray.Length)
        {
            alphabet[l] = maxFlowArray[p];
            ++l;
            ++p;
        }
        alphabet[l] = ' ';
        ++l;
        for (int k = 0; k < firstAddedAlphabet.Length; ++k)
        {
            alphabet[l] = firstAddedAlphabet[k];
            ++l;
        }

        // Запись алфавита
        var filestream = new FileStream(newFile, FileMode.OpenOrCreate);
        StreamWriter filestreamwriter = new StreamWriter(filestream);
        filestreamwriter.Write(alphabet, 0, alphabet.Length);
        filestreamwriter.Close();
        filestream.Close();

        // Добавление потоков в файл
        walker = 0;
        for (int i = 0; i < textFromFile.Length; i++)
        {
            if (!bor.Contains(textFromFile, walker, i))
            {
                var (_, flow) = bor.Add(bor, textFromFile, walker, i);
                walker = i;
                var bufferOut = new byte[maxFlow];
                byte[] bytes = BitConverter.GetBytes(flow);
                for (int t = 0; t < bytes.Length; ++t)
                {
                    bufferOut[t] = bytes[t];
                }
                var fstreamNew = new FileStream(newFile.ToString(), FileMode.Append);
                fstreamNew.WriteAsync(bufferOut, 0, bufferOut.Length);
                fstreamNew.Close();
            }
        }

        // Добавление последнего потока в файл
        var flowLast = bor.ReturnFlowByCharArray(textFromFile, bufferIn.Length - 1, bufferIn.Length - 1);
        var bufferOutLast = new byte[maxFlow];
        byte[] bytesLast = BitConverter.GetBytes(flowLast);
        for (int t = 0; t < bytesLast.Length; ++t)
        {
            bufferOutLast[t] = bytesLast[t];
        }
        var LastFstreamNew = new FileStream(newFile.ToString(), FileMode.Append);
        LastFstreamNew.WriteAsync(bufferOutLast, 0, bufferOutLast.Length);
        LastFstreamNew.Close();
        return true;
    }

    private bool DecodeFile(string fileName)
    {
        byte[] bufferIn = File.ReadAllBytes(fileName);
        char[] stringWithCodes = Encoding.UTF8.GetString(bufferIn).ToCharArray();
        int k = 1;
        int sizeAlphabet = 0;
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

        while (stringWithCodes[i] != 32)
        {
            sizeAlphabet = sizeAlphabet * k + stringWithCodes[i] - 48;
            k = 10;
            ++i;
        }
        ++i;
        int sizeSymbols = 0;
        k = 1;
        while (stringWithCodes[i] != 32)
        {
            sizeSymbols = sizeSymbols * k + stringWithCodes[i] - 48;
            k = 10;
            ++i;
        }
        ++i;
        var dictionaryForDecode = new Dictionary<int, string>();
        int sizeFromToInBufferIn = i + sizeAlphabet;
        int index = 0;
        for(; i < sizeFromToInBufferIn; ++i)
        {
            dictionaryForDecode.Add(index, ((char)bufferIn[i]).ToString());
            index++;
        }

        int input = 0;
        StringBuilder? previousString = null;
        bool isFirst = true;
        while (i < bufferIn.Length)
        {

            var symbolFromArray = new byte[sizeSymbols];
            int pointerForSymbolsFromOneArray = 0;
            int size = i + sizeSymbols;
            for( ; i < size; ++i)
            {
                symbolFromArray[pointerForSymbolsFromOneArray] = bufferIn[i];
                ++pointerForSymbolsFromOneArray;
            }
            input = BitConverter.ToInt32(symbolFromArray, 0);
            if (!isFirst)
            {
                if (!dictionaryForDecode.ContainsKey(input))
                {
                    previousString.Append(previousString[0]);
                    dictionaryForDecode.Add(index, previousString.ToString());
                }
                var stringByIndex = dictionaryForDecode[input];
                if (index != input)
                {
                    previousString.Append(stringByIndex[0]);
                    dictionaryForDecode.Add(index, previousString.ToString());
                }
                File.AppendAllText(newFile, stringByIndex);
                previousString = new StringBuilder(stringByIndex);
                ++index;
            }
            else
            {
                var stringByIndex = dictionaryForDecode[input];
                previousString = new StringBuilder(stringByIndex);
                isFirst = false;
                File.AppendAllText(newFile, stringByIndex);
            }
        }
        return true;
    }

    public bool LzwAlgorithm(string fileName, string parametr)
    {
        if (parametr.Length == 2 && parametr[0] == '-' && parametr[1] == 'c')
        {
            return CodeFile(fileName);
        }
        else if (parametr.Length == 2 && parametr[0] == '-' && parametr[1] == 'u')
        {
            return DecodeFile(fileName);
        }
        else
        {
            return false;
        }
    }
}