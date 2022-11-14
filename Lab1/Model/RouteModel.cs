namespace Lab1.Model;

public class RouteModel
{
    public (short, short)[] Route { get; }
    public int PathLength { get; }
    public bool IsCompleted { get; }

    public RouteModel((short, short)[] route, int length, bool isCompleted)
    {
        Route = route;
        PathLength = length;
        IsCompleted = isCompleted;
    }
}