using System.Text;
using System.Security.Cryptography;

namespace Md5;

public class SingleMd5
{
    /// <summary>
    /// Calculating file by path
    /// </summary>
    /// <param name="path">Where file is lies</param>
    /// <returns>md5 hash</returns>
    public static byte[] CalculateFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }
        var fstream = File.OpenRead(path);
        using var md5 = MD5.Create();
        return md5.ComputeHash(fstream);
    }

    private static void TakeFilesAndDirectoriesFromSubDirectories(string path, List<string> listDirectoriesAndFiles)
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
            TakeFilesAndDirectoriesFromSubDirectories(directory, listDirectoriesAndFiles);
        }
    }

    private static void TakeFilesAndDirectoriesFromDirectory(string path, List<string> listDirectoriesAndFiles)
    {
        listDirectoriesAndFiles.Add(path);
        foreach (string file in Directory.GetFiles(path))
        {
            listDirectoriesAndFiles.Add(file);
        }
        TakeFilesAndDirectoriesFromSubDirectories(path, listDirectoriesAndFiles);
    }

    /// <summary>
    /// Calculating md5 hash using one thread
    /// </summary>
    public static byte[] CalculateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException();
        }

        var listAllDirectoriesAndFliles = new List<string>();
        TakeFilesAndDirectoriesFromDirectory(path, listAllDirectoriesAndFliles);
        using var md5 = MD5.Create();
        var result = new byte[0];
        foreach (var fileOrDirectory in listAllDirectoriesAndFliles)
        {
            if (File.Exists(fileOrDirectory))
            {
                result = result.Concat(CalculateFile(fileOrDirectory)).ToArray();
            }
            else
            {
                result = result.Concat(md5.ComputeHash(Encoding.UTF8.GetBytes(fileOrDirectory))).ToArray();
            }
        }
        return result;
    }
}