using Lab1.Controller;
using Lab1.View;

namespace Lab1.Model;

public static class Program
{
    public static void Main()
    {
        ValidatedFileModel filename = UserInput.GetFileName();
        Method method = UserInput.GetMethod();
        AdjacencyType adjacency = UserInput.GetAdjacencyType();
        FieldModel field = FieldFactory.GetField(filename);
        (short, short) startPoint = UserInput.GetEntryPoint(field, true);
        (short, short) endPoint = UserInput.GetEntryPoint(field, false);
        field.SetEntryPoints(startPoint, endPoint);
        PathSearcher searcher = method == Method.Li ? new Li(field, adjacency) : new AStar(field, adjacency);
        if (searcher.FindPath())
        {
            RouteModel route = searcher.TraceRoute();
            ResultOutput.OutputResult(field, route);
        }
        else Console.WriteLine("Path is not found!");
    }
}

/*
data.csv


7, 11
1, 13

*/