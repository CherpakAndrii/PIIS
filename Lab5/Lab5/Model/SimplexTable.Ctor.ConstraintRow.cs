namespace Lab5.Model;

public partial class SimplexTable
{
    private class ConstraintRow : IComparable
    {
        private byte _index;
        public byte _dependentFreeVariables;
        
        public ConstraintRow(double[,] matrix, List<byte> freeVariables, byte i)
        {
            _index = i;
            _dependentFreeVariables = 0;
            foreach (var variable in freeVariables)
            {
                if (matrix[i, variable] != 0) _dependentFreeVariables++;
            }
        }

        public int CompareTo(object? obj)
        {
            return this._dependentFreeVariables.CompareTo(((ConstraintRow)obj!)._dependentFreeVariables);
        }

        public static implicit operator byte(ConstraintRow row) => row._index;
    }
}