using System.Text;
using System.Security.Cryptography;

namespace Md5;

public class MultiThreadMD5
{
    /// <summary>
    /// Calculating file using threads
    /// </summary>
    /// <param name="path">path where file</param>
    /// <returns>md5 hash</returns>
    public static async Task<byte[]> CalculateFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }
        var fstream = File.OpenRead(path);
        using var md5 = MD5.Create();
        byte[] buffer = new byte[fstream.Length];
        await fstream.ReadAsync(buffer, 0, buffer.Length);
        return md5.ComputeHash(buffer);
    }

    private static async Task TakeFilesAndDirectoriesFromSubDirectories(string path, List<string> listDirectoriesAndFiles)
    {
        var allDirectories = Directory.GetDirectories(path);
        foreach (string directory in allDirectories)
        {
            listDirectoriesAndFiles.Add(directory);
            var allFiles = Directory.GetFiles(directory);

            foreach (string file in allFiles)
            {
                listDirectoriesAndFiles.Add(file);
            }
            await TakeFilesAndDirectoriesFromSubDirectories(directory, listDirectoriesAndFiles);
        }
    }

    private static async Task TakeFilesAndDirectoriesFromDirectory(string path, List<string> listDirectoriesAndFiles)
    {
        listDirectoriesAndFiles.Add(path);
        foreach (string file in Directory.GetFiles(path))
        {
            listDirectoriesAndFiles.Add(file);
        }
        await TakeFilesAndDirectoriesFromSubDirectories(path, listDirectoriesAndFiles);
    }

    /// <summary>
    /// Calculating md5 hash using threads
    /// </summary>
    public static async Task<byte[]> CalculateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException();
        }

        var listAllDirectoriesAndFliles = new List<string>();
        await TakeFilesAndDirectoriesFromDirectory(path, listAllDirectoriesAndFliles);
        var arrayList = listAllDirectoriesAndFliles.ToArray();
        var arrayWithNumber = new byte[arrayList.Length][];
        Parallel.For(0, arrayList.Length, i =>
        {
            using var md5 = MD5.Create();
            if (File.Exists(arrayList[i]))
            {
                arrayWithNumber[i] = CalculateFile(arrayList[i]).Result;
            }
            else
            {
                arrayWithNumber[i] = md5.ComputeHash(Encoding.UTF8.GetBytes(arrayList[i]));
            }
        });

        var listByte = new List<byte>();

        foreach (var element in arrayWithNumber)
        {
            foreach (var _byte in element)
            {
                listByte.Add(_byte);
            }
        }
        return listByte.ToArray();
    }
}