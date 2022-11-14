namespace Lab1.Model;

public abstract class PathSearcher
{
    protected PathSearcher(FieldModel field, AdjacencyType adjacency)
    {
        Adjacency = adjacency;
        Field = field;
    }

    protected FieldModel Field { get; }
    private AdjacencyType Adjacency { get; }
    public abstract bool FindPath();
    
    public RouteModel TraceRoute()
    {
        if (!Field.IsSolved) throw new Exception("Impossible to trace route in not solved field!");
        Stack<Cell> reversedRoute = new Stack<Cell>();
        Cell current;
        (short previousY, short previousX) = Field.EndPoint;
        do
        {
            current = Field[previousY, previousX];
            reversedRoute.Push(current);
            (previousY, previousX) = current.PreviousCellYXCoordinates;
        } while ((previousY, previousX) != (-1, -1));
    
        (short, short)[] route = new (short, short)[reversedRoute.Count];
        for (short counter = 0; counter<route.Length; counter++)
        {
            current = reversedRoute.Pop();
            route[counter] = (current.Y, current.X);
        }
    
        return new RouteModel(route, current.MinDistanceFromStart,
            (route[0] == Field.StartPoint && route[^1] == Field.EndPoint));
    }

    protected Cell[] GetAdjacentCells(Cell cell)
    {
        byte counter = 0;
        Cell[] adjacentCells = new Cell[8];
        int[] yIndexDeltas = {-1, 0, 1, 0};
        int[] xIndexDeltas = {0, 1, 0, -1};
        bool[] isNotWall = new bool[4];
        for (byte i = 0; i < 4; i++)
        {
            isNotWall[i] = TryAddCell((short)(cell.Y + yIndexDeltas[i]), (short)(cell.X + xIndexDeltas[i]), ref adjacentCells,
                ref counter);
        }
        
        if (Adjacency == AdjacencyType.EdgesAndCorners)
        {
            yIndexDeltas = new[] { -1, 1, 1, -1 };
            xIndexDeltas = new[] { 1, 1, -1, -1 };
            for (byte i = 0; i < 4; i++)
            {
                if (isNotWall[i] && isNotWall[(i+1)%4])
                    TryAddCell((short)(cell.Y + yIndexDeltas[i]), (short)(cell.X + xIndexDeltas[i]), ref adjacentCells, ref counter);
            }
        }

        return adjacentCells[..counter];
    }

    private bool TryAddCell(short y, short x, ref Cell[] adjacent, ref byte counter)
    {
        if (y > -1 && y < Field.Height && x > -1 && x < Field.Width)
        {
            Cell cell = Field[y, x];
            if (!(cell.IsPassed || cell.IsWall))
            {
                adjacent[counter] = cell;
                counter++;
            }

            return !cell.IsWall;
        }
        return false;
    }
}