namespace ListAndUniqueList;

using Routers;
using System;
using System.Net.WebSockets;

class Program
{
    public static void Main(string[] args)
    {
        var routers = new Routers();
        routers.WorkWithFile("C:\\Users\\User\\Downloads\\graph.txt");
    }
}