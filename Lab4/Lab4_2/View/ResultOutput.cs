using Lab4_2.Model;

namespace Lab4_2.View;

public class ResultOutput
{
    public void PrintResult(Node[] vertices, int start)
    {
        Console.WriteLine($"Distances from {start} to other vertices:");
        foreach (var v in vertices)
        {
            Console.WriteLine($"{v.Name}: " + (v.DistanceFromStart==int.MaxValue/2?"no route":v.DistanceFromStart));
        }
    }
}