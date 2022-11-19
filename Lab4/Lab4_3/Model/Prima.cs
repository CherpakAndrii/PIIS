namespace Lab4_3.Model;

public class Prima
{
    private int[,] _graph;
    public Prima(int[,] graph)
    {
        _graph = graph;
    }

    public Node[]? GetMinCoverTree(out int treeWeight)
    {
        Node[] vertices = new NodeFactory().GetVerticeList(_graph.GetLength(0));
        PriorityQueue<Node, int> queue = new PriorityQueue<Node, int>();
        treeWeight = 0;
        int ind = GetFirstNotIsolatedNodeIndex();
        if (ind == -1) return null;
        vertices[ind].DistanceFromPrevious = 0;
        queue.Enqueue(vertices[ind], 0);
        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            if (current.Passed) continue;
            current.MakePassed();
            treeWeight += current.DistanceFromPrevious;
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
                if (!vertices[i].Passed && _graph[current.Name - 1, i] < vertices[i].DistanceFromPrevious)
                {
                    vertices[i].Previous = current;
                    vertices[i].DistanceFromPrevious = _graph[current.Name - 1, i];
                    queue.Enqueue(vertices[i], vertices[i].DistanceFromPrevious);
                }
            }
        }

        return vertices;
    }

    private int GetFirstNotIsolatedNodeIndex()
    {
        bool hasAdjacency;
        for (int i = 0; i < _graph.GetLength(0); i++)
        {
            hasAdjacency = false;
            for (int j = 0; j < _graph.GetLength(1); j++)
            {
                if (_graph[i, j] is > 0 and < Int32.MaxValue / 2) hasAdjacency = true;
            }

            if (hasAdjacency) return i;
        }

        return -1;
    }
}