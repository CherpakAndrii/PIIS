namespace Lab4_2.Model;

public class NodeFactory
{
    public Node[] GetVerticeList(int[,] adjacenceMatrix, int start)
    {
        Node[] vertices = new Node[adjacenceMatrix.GetLength(0)];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Node(i + 1);
        }

        vertices[start-1].DistanceFromStart = 0;

        return vertices;
    }
}