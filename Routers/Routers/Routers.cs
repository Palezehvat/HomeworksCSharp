namespace Routers;

public class Routers
{

    private void WriteToFile(Graph graph, string filePath)
    {
        graph.WriteToFile(filePath);
    }

    public bool WorkWithFile(string filePath)
    {
        var file = new StreamReader(filePath);
        // Проверка на файл
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
                        mainVertex = mainVertex * 10 + line[i] - 48;
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

        graph.WriteToFile(filePath);

        return true;
    }
}
