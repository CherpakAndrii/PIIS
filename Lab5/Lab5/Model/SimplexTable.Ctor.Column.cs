namespace Lab5.Model;

public partial class SimplexTable
{
    private class Column : IComparable
    {
        private byte _index;
        private bool _isFull;
        private double _sum;

        public Column(double[,] matrix, byte j)
        {
            _index = j;
            _isFull = true;
            _sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, j] == 0) _isFull = false;
                _sum += Math.Abs(matrix[i, j]);
            }
        }
        
        public static implicit operator byte(Column c)
        {
            return c._index;
        }

        public static bool operator <(Column c1, Column c2)
        {
            if (!c1._isFull && c2._isFull) return true;
            return c1._sum < c2._sum;
        }
        
        public static bool operator >(Column c1, Column c2)
        {
            if (c1._isFull && !c2._isFull) return true;
            return c1._sum > c2._sum;
        }
        
        public static bool operator <=(Column c1, Column c2)
        {
            return !(c1 > c2);
        }
        
        public static bool operator >=(Column c1, Column c2)
        {
            return !(c1 < c2);
        }

        public int CompareTo(object? c2)
        {
            return this > (Column)c2! ? 1 : this < (Column)c2! ? -1 : 0;
        }
    }
}