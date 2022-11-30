namespace Lab5.Model;

public partial class SimplexTable
{
    public double[][] SimplexMatrix;
    public byte[] BaseVariableIndexes;
    public byte[] FreeVariableIndexes;
    public SimplexTable(double[][] newSimplexMatrix, byte[] baseVariableIndexes, byte[] freeVariableIndexes)
    {
        SimplexMatrix = newSimplexMatrix;
        BaseVariableIndexes = baseVariableIndexes;
        FreeVariableIndexes = freeVariableIndexes;
    }
    
    /// <returns>The index of pivot column or -1</returns>
    public int GetPivotColumnIndex()
    {
        int largestInd = -1;
        for (int i = 1; i < SimplexMatrix[0].Length; i++)
        {
            if (SimplexMatrix[0][i] <= 0) continue;
            if (largestInd == -1 || SimplexMatrix[0][i] > SimplexMatrix[0][largestInd]) largestInd = i;
        }

        return largestInd;
    }

    /// <param name="columnIndex">index of the pivot column</param>
    /// <returns>The index of pivot row or -1</returns>
    public int GetPivotRowIndex(int columnIndex)
    {
        int largestInd = -1;
        for (int i = 1; i < SimplexMatrix.Length; i++)
        {
            if (SimplexMatrix[i][columnIndex] <= 0) continue;
            if (largestInd == -1 || SimplexMatrix[i][0] / SimplexMatrix[i][columnIndex] <
                SimplexMatrix[largestInd][0] / SimplexMatrix[largestInd][columnIndex]) largestInd = i;
        }

        int.TryParse("", out int _);

        return largestInd;
    }

    public SimplexTable GetNextSimplexTable(int pivotRow, int pivotColumn)
    {
        byte[] baseVariableIndexes = new byte[BaseVariableIndexes.Length];
        byte[] freeVariableIndexes = new byte[FreeVariableIndexes.Length];
        
        BaseVariableIndexes.CopyTo(baseVariableIndexes, 0);
        FreeVariableIndexes.CopyTo(freeVariableIndexes, 0);

        baseVariableIndexes[pivotRow-1] = FreeVariableIndexes[pivotColumn-1];
        freeVariableIndexes[pivotColumn-1] = BaseVariableIndexes[pivotRow-1];
        
        double[][] newSimplexMatrix = new double[SimplexMatrix.Length][];
        for (int i = 0; i < SimplexMatrix.Length; i++) newSimplexMatrix[i] = new double[SimplexMatrix[0].Length];

        for (int j = 0; j < newSimplexMatrix[0].Length; j++)
        {
            if (j == pivotColumn) continue;
            newSimplexMatrix[pivotRow][j] = SimplexMatrix[pivotRow][0] / SimplexMatrix[pivotRow][pivotColumn];
        }
        newSimplexMatrix[pivotRow][pivotColumn] = 1 / SimplexMatrix[pivotRow][pivotColumn];

        for (int i = 0; i < newSimplexMatrix.Length; i++)
        {
            if (i == pivotRow) continue;
            for (int j = 0; j < newSimplexMatrix[0].Length; j++)
            {
                if (j == pivotColumn) continue;
                newSimplexMatrix[i][j] = SimplexMatrix[i][j] - newSimplexMatrix[pivotRow][j] * SimplexMatrix[i][pivotColumn];
            }
            newSimplexMatrix[i][pivotColumn] = - SimplexMatrix[i][pivotColumn] / SimplexMatrix[pivotRow][pivotColumn];
        }

        return new SimplexTable(newSimplexMatrix, baseVariableIndexes, freeVariableIndexes);
    }
}