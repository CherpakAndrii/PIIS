using System.Threading.Channels;

namespace Lab4_1.View;

public class ResultOutputter
{
    public void PrintResults(List<string> results)
    {
        if (results.Count == 0)
        {
            Console.WriteLine("There is no similar substrings in source string");
            return;
        }

        Console.WriteLine($"We found {results.Count} similar substrings:");
        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }

    public void PrintTargetHash(byte hash) => Console.WriteLine("Target hash: "+hash);
}