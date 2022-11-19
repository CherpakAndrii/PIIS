using Lab4_3.Model;

namespace Lab4_3.View;

public class ResultOutput
{
    public void PrintResult(Node[]? vertices, int weight)
    {
        if (vertices is null)
        {
            Console.WriteLine("This graph has no edges!");
            return;
        }
        Console.WriteLine("Tree weight: " + weight + "\nEdges:");
        foreach (var v in vertices)
        {
            if (v.Previous is null && v.DistanceFromPrevious == 0) continue;
            Console.WriteLine($"{v.Name}" + (v.DistanceFromPrevious==int.MaxValue/2?": no route":" -- "+v.Previous!.Name));
        }
    }
}