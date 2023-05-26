namespace RoutersByGraph;

/// <summary>
/// A class for implementing finding a spanning tree and printing its file by retrieving data from a file
/// </summary>
public class Routers
{
    private void WriteToFile(Graph graph, string filePath, string fileAfter)
    {
        graph.WriteToFile(filePath, fileAfter);
    }

    /// <summary>
    /// Works with the file containing the initial data
    /// </summary>
    /// <param name="filePath">The path to the file</param>
    /// <returns>Returns true if the graph is connected and false if not</returns>
    /// <exception cref="InvalidFileException">Throws an exception if the entry in the file is uncorrected</exception>
    public bool WorkWithFile(string filePath, string fileAfter)
    {
        using var file = new StreamReader(filePath);
        if (file == null)
        {
            throw new InvalidFileException();
        }
        string line = "\0";
        int mainVertex = 0;
        int anotherVertex = 0;
        int sizeArc = 0;
        int theBiggestVertex = 0;
        var graph = new Graph();
        while (!file.EndOfStream)
        {
            line = file.ReadLine();
            bool isFirst = true;
            for(int i = 0; i < line.Length; ++i)
            {
                if (isFirst && Char.IsDigit(line[i]))
                {
                    mainVertex = line[i] - 48;
                    ++i;
                    while (Char.IsDigit(line[i]))
                    {
                        int result = 0;
                        bool isCorrect = int.TryParse(line[i].ToString(), out result);
                        mainVertex = mainVertex * 10 +
                        ++i;
                    }

                    if (line[i] != ':')
                    {
                        file.Close();
                        throw new InvalidFileException();
                    }
                    ++i;
                    if (line[i] != ' ')
                    {
                        file.Close();
                        throw new InvalidFileException();
                    }
                    isFirst = false;
                }
                else if (isFirst)
                {
                    file.Close();
                    throw new InvalidFileException();
                }
                else
                {
                    if (Char.IsDigit(line[i]))
                    {
                        anotherVertex = line[i] - 48;
                        ++i;
                        while (Char.IsDigit(line[i]))
                        {
                            anotherVertex = anotherVertex * 10 + line[i] - 48;
                            ++i;
                        }
                        if (line[i] != ' ')
                        {
                            file.Close();
                            throw new InvalidFileException();
                        }
                        ++i;
                        if (line[i] == '(')
                        {
                            ++i;
                            if (!Char.IsDigit(line[i]))
                            {
                                file.Close();
                                throw new InvalidFileException();
                            }
                            sizeArc = line[i] - 48;
                            ++i;
                            while (Char.IsDigit(line[i]))
                            {
                                sizeArc = sizeArc * 10 + line[i] - 48;
                                ++i;
                            }

                            if (theBiggestVertex < mainVertex)
                            {
                                theBiggestVertex = mainVertex;
                            }

                            if (theBiggestVertex < anotherVertex)
                            {
                                theBiggestVertex = anotherVertex;
                            }

                            graph.AddArcs(mainVertex, anotherVertex, sizeArc);

                            if (line[i] != ')')
                            {
                                file.Close();
                                throw new InvalidFileException();
                            }
                            ++i;
                            if (i < line.Length)
                            {
                                if (line[i] != ',')
                                {
                                    file.Close();
                                    throw new InvalidFileException();
                                }
                                ++i;
                                if (line[i] != ' ')
                                {
                                    file.Close();
                                    throw new InvalidFileException();
                                }
                            }
                        }
                        else
                        {
                            file.Close();
                            throw new InvalidFileException();
                        }
                    }
                    else
                    {
                        file.Close();
                        throw new InvalidFileException();
                    }
                }
                anotherVertex = 0;
                sizeArc = 0;
            }
            mainVertex = 0;
        }
        file.Close();

        graph.AddVertexes(theBiggestVertex);
        if (!graph.KraskalAlgorithm(graph))
        {
            return false;
        }

        graph.WriteToFile(filePath, fileAfter);

        return true;
    }
}
