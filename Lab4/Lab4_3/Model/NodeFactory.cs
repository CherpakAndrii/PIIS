namespace Lab4_3.Model;

public class NodeFactory
{
    public Node[] GetVerticeList(int graphSize)
    {
        Node[] vertices = new Node[graphSize];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Node(i + 1);
        }
        
        return vertices;
    }
}