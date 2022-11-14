namespace Lab3.Model;

public class Cell
{
    public short Y { get; }
    public short X { get; }
    public bool IsWall { get; }
    public bool IsPassed { get; private set; }

    private (short, short) _previousCellYxCoordinates;
    public (short, short) PreviousCellYXCoordinates
    {
        get => _previousCellYxCoordinates;
        set
        {
            if (value.Item1 > -1 && value.Item2 > -1) _previousCellYxCoordinates = value;
            else throw new IndexOutOfRangeException("Previous cell coordinates must be greater than -1");
        }
    }

    private int _minDistanceFromStart;
    public int MinDistanceFromStart
    {
        get => _minDistanceFromStart;
        set
        {
            if (value < MinDistanceFromStart) _minDistanceFromStart = value;
            else throw new Exception("It isn't possible to set the minimum distance bigger than it was before!");
        }
    }

    public Cell(short y, short x, bool isWall)
    {
        Y = y;
        X = x;
        IsWall = isWall;
        IsPassed = false;
        _previousCellYxCoordinates = (-1, -1);
        _minDistanceFromStart = Int32.MaxValue/2;
    }

    public void MakePassed()
    {
        if (IsPassed) throw new Exception("The cell has already been passed!");
        IsPassed = true;
    }

    public Cell(Cell source)
    {
        Y = source.Y;
        X = source.X;
        IsWall = source.IsWall;
        IsPassed = false;
        _previousCellYxCoordinates = (-1, -1);
        _minDistanceFromStart = Int32.MaxValue/2;
    }
}