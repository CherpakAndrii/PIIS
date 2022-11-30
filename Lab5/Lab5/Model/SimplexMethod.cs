namespace Lab5.Model;

public class SimplexMethod
{
    public (double, double[])? Solve(double[,] A, double[] B, double[] C)
    {
        SimplexTable table = new SimplexTable(A, B, C);
        int i, j = table.GetPivotColumnIndex();

        while (j > 0)
        {
            i = table.GetPivotRowIndex(j);
            if (i < 0) return null;
            table = table.GetNextSimplexTable(i, j);
            j = table.GetPivotColumnIndex();
        }

        return ParseSolutionFromTable(table);
    }

    private (double, double[]) ParseSolutionFromTable(SimplexTable table)
    {
        double[] resultVector = new double[table.BaseVariableIndexes.Length+table.FreeVariableIndexes.Length];
        for (int i = 0; i < table.SimplexMatrix.Length - 1; i++)
        {
            resultVector[table.BaseVariableIndexes[i]] = table.SimplexMatrix[i + 1][0];
        }

        return (table.SimplexMatrix[0][0], resultVector);
    }
}