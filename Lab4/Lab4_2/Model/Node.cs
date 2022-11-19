namespace Lab4_2.Model;

public class Node
{
    public int Name { get; }
    public bool Passed { get; private set; }
    private int _distance = Int32.MaxValue;
    public Node? Previous { get; set; }
    public int DistanceFromStart
    {
        get => _distance;
        set
        {
            if (value >= _distance) throw new ArgumentException("Distance from start can't increase!");
            _distance = value;
        }
    }

    public Node(int name, int distance = Int32.MaxValue/2)
    {
        Name = name;
        DistanceFromStart = distance;
    }

    public void MakePassed()
    {
        Passed = true;
    }
}