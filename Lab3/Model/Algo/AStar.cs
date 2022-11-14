namespace Lab3.Model;

public class AStar : PathSearcher
{
    public AStar(FieldModel field, AdjacencyType adjacency) : base(field, adjacency) { }

    public override bool FindPath()
    {
        PriorityQueue<Cell, int> queue = new PriorityQueue<Cell, int>();
        Cell current = Field[Field.StartPoint.Item1, Field.StartPoint.Item2];
        current.MinDistanceFromStart = 0;
        queue.Enqueue(current, GetPriority(current));
        while (queue.Count>0)
        {
            current = queue.Dequeue();
            if (current.IsPassed) continue;
            current.MakePassed();
            if ((current.Y, current.X) == Field.EndPoint)
            {
                Field.MarkAsSolved();
                return true;
            }
            Cell[] adjacent = GetAdjacentCells(current);
            for (int i = 0; i < adjacent.Length; i++)
            {
                int newAdjacentMinDistance =
                    current.MinDistanceFromStart + ((adjacent[i].X == current.X || adjacent[i].Y == current.Y) ? 10 : 14);
                if (newAdjacentMinDistance < adjacent[i].MinDistanceFromStart)
                {
                    adjacent[i].MinDistanceFromStart = newAdjacentMinDistance;
                    adjacent[i].PreviousCellYXCoordinates = (current.Y, current.X);
                    queue.Enqueue(adjacent[i], GetPriority(current));
                }
            }
        }
        Field.MarkAsSolved();
        return false;
    }

    private int GetPriority(Cell cell)
    {
        return (int)(cell.MinDistanceFromStart + Math.Sqrt(Math.Pow((Field.EndPoint.Item1 - cell.Y) * 10, 2) +
                                                           Math.Pow((Field.EndPoint.Item2 - cell.X) * 10, 2)));
    }
}