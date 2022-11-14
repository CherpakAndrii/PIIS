namespace Lab2.Model;

public class Li:PathSearcher
{
    public Li(FieldModel field, AdjacencyType adjacency) : base(field, adjacency) { }
    public override bool FindPath()
    {
        Queue<Cell> queue = new Queue<Cell>();
        Cell current = Field[Field.StartPoint.Item1, Field.StartPoint.Item2];
        current.MinDistanceFromStart = 0;
        queue.Enqueue(current);
        while (queue.Count>0)
        {
            current = queue.Dequeue();
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
                if (adjacent[i].MinDistanceFromStart == Int32.MaxValue/2) queue.Enqueue(adjacent[i]);
                if (newAdjacentMinDistance < adjacent[i].MinDistanceFromStart)
                {
                    adjacent[i].MinDistanceFromStart = newAdjacentMinDistance;
                    adjacent[i].PreviousCellYXCoordinates = (current.Y, current.X);
                }
            }
        }
        Field.MarkAsSolved();
        return false;
    }
}