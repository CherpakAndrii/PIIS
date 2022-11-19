namespace Lab4_3.Model;

public class GraphFactory
{
    public int[,] GetGraph(List<string> inputs, int graphSize)
    {
        int[,] graph = new int[graphSize, graphSize];
        for (int i = 0; i < graphSize; i++)
        {
            for (int j = 0; j < graphSize; j++)
            {
                graph[i, j] = i == j ? 0 : int.MaxValue / 2;
            }
        }

        foreach (string input in inputs)
        {
            int i = input[0] - 49;
            int j = input[1] - 49;
            int value = int.Parse(input[3..]);
            graph[i, j] = graph[j, i] = Math.Min(value, graph[i, j]);
        }

        return graph;
    }
}