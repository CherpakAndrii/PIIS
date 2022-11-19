namespace Lab4_2.Model;

public class Dijkstra
{
    private int[,] _graph;
    public Dijkstra(int[,] graph)
    {
        _graph = graph;
    }

    public Node[] GetAllDistances(int startVerticeName)
    {
        Node[] vertices = new NodeFactory().GetVerticeList(_graph, startVerticeName);
        PriorityQueue<Node, int> queue = new PriorityQueue<Node, int>();
        queue.Enqueue(vertices[startVerticeName-1], 0);
        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            if (current.Passed) continue;
            current.MakePassed();
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
                if (_graph[current.Name - 1, i] < int.MaxValue / 2 && _graph[current.Name - 1, i]+current.DistanceFromStart < vertices[i].DistanceFromStart)
                {
                    vertices[i].Previous = current;
                    vertices[i].DistanceFromStart = _graph[current.Name - 1, i] + current.DistanceFromStart;
                    queue.Enqueue(vertices[i], vertices[i].DistanceFromStart);
                }
            }
        }

        return vertices;
    }
}