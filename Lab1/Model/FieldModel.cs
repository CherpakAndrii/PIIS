namespace Lab1.Model;

public class FieldModel
{
    private readonly Cell[][] _fieldMatrix;
    public short Width { get; }
    public short Height { get; }
    public bool IsSolved { get; private set; }
    public (short, short) StartPoint { get; private set; }
    public (short, short) EndPoint { get; private set; }

    public FieldModel(Cell[][] field)
    {
        _fieldMatrix = field;
        Height = (short)field.Length;
        Width = (short)field[0].Length;
        IsSolved = false;
        StartPoint = EndPoint = (-1, -1);
    }
    
    public void MarkAsSolved() => IsSolved = true;

    public void SetEntryPoints((short, short) start, (short, short) end)
    {
        if (IsSolved) throw new Exception("The field is already solved, so you can't set the entry points.");
        StartPoint = start;
        EndPoint = end;
    }

    public void Clear()
    {
        for (short i = 0; i < Height; i++)
        {
            for (short j = 0; j < Height; j++)
            {
                Cell old = _fieldMatrix[i][j];
                _fieldMatrix[i][j] = new Cell(i, j, old.IsWall);
            }
        }

        IsSolved = false;
        StartPoint = EndPoint = (-1, -1);
    }

    public Cell this[short y, short x]
    {
        get
        {
            if (y > -1 && y < Height && x > -1 && x < Width) return _fieldMatrix[y][x];
            throw new IndexOutOfRangeException();
        }
    }
}