namespace ListAndUniqueList;

using Routers;
using System;
using System.Net.WebSockets;

class Program
{
    public static void Main(string[] args)
    {
        var graph = new Graph();
        graph.AddStandartListWithAllVertexes(10);
        graph.AddVertexWithBandwidthSize(2, 3, 54);
        graph.PrintGraph();
    }
}