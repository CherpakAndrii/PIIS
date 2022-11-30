namespace Lab5.Model;

public partial class SimplexTable
{
    
    public SimplexTable(double[,] constraintCoefficientsMatrix, double[] freeConstraintMembers, double[] targetFunctionCoefficients)
    {
        EmptyInit(freeConstraintMembers.Length, targetFunctionCoefficients.Length);
        ChooseBaseVariables(constraintCoefficientsMatrix);
        FillSimplexMatrix(constraintCoefficientsMatrix, freeConstraintMembers, targetFunctionCoefficients);
    }

    private void EmptyInit(int bLength, int cLength)
    {
        BaseVariableIndexes = new byte[bLength];
        FreeVariableIndexes = new byte[cLength-bLength];
        SimplexMatrix = new double[bLength+1][];
        for (int i = 0; i < SimplexMatrix.Length; i++)
        {
            SimplexMatrix[i] = new double[FreeVariableIndexes.Length+1];
        }
    }

    private void ChooseBaseVariables(double[,] matrix)
    {
        Column[] columns = new Column[BaseVariableIndexes.Length+FreeVariableIndexes.Length];
        for (byte i = 0; i < columns.Length; i++)
        {
            columns[i] = new Column(matrix, i);
        }
        
        Array.Sort(columns, (c1, c2) => c2.CompareTo(c1));
        int ctr;
        for (ctr = 0; ctr < FreeVariableIndexes.Length; ctr++) FreeVariableIndexes[ctr] = columns[ctr];
        Array.Sort(FreeVariableIndexes);

        for (int ctr2 = 0; ctr2 < BaseVariableIndexes.Length; ctr2++) BaseVariableIndexes[ctr2] = columns[ctr + ctr2];
        Array.Sort(BaseVariableIndexes);
    }

    private void FillSimplexMatrix(double[,] A, double[] B, double[] C)
    {
        List<byte> unusedBaseVariables = new List<byte>(BaseVariableIndexes);
        double functionValue = 0;
        do
        {
            byte bestConstraint = GetBestUnusedConstraint(A, unusedBaseVariables);
            byte boundedVariable = FindBoundedBaseVariable(A, unusedBaseVariables, bestConstraint);
            NormalizeConstraintRow(A, B, bestConstraint, boundedVariable);
            AddConstraintToSimplexMatrix(A, bestConstraint, boundedVariable, B[bestConstraint]);
            ChangeInputs(A, B, C, bestConstraint, boundedVariable, ref functionValue);
            unusedBaseVariables.Remove(boundedVariable);
        } 
        while (unusedBaseVariables.Count > 0);

        AddFunctionToSimplexTable(C, functionValue);
    }

    private void AddFunctionToSimplexTable(double[] C, double functionValue)
    {
        SimplexMatrix[0][0] = functionValue;
        for (int j = 0; j < FreeVariableIndexes.Length; j++)
        {
            SimplexMatrix[0][j + 1] = -C[FreeVariableIndexes[j]];
        }
    }

    private void ChangeInputs(double[,] A, double[] B, double[] C, byte constraintIndex, byte boundedVariableIndex, ref double functionValue)
    {
        if (C[boundedVariableIndex] != 0)
        {
            double multiplier =  C[boundedVariableIndex];
            for (int j = 0; j < C.Length; j++)
            {
                C[j] -= A[constraintIndex, j] * multiplier;
            }
            functionValue += B[constraintIndex] * multiplier;
        }
        
        for (int i = 0; i < A.GetLength(0); i++)
        {
            if (A[i, boundedVariableIndex] != 0 && i != constraintIndex)
            {
                double multiplier = A[i, boundedVariableIndex];
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    A[i, j] -= A[constraintIndex, j] * multiplier;
                }
                B[i] -= B[constraintIndex] * multiplier;
            }
        }
        for (int j = 0; j < A.GetLength(1); j++)
        {
            A[constraintIndex, j] = 0;
        }

        B[constraintIndex] = 0;
    }

    private void NormalizeConstraintRow(double[,] A, double[] B, byte constraintIndex, byte boundedVariableIndex)
    {
        for (int j = 0; j < A.GetLength(1); j++)
        {
            A[constraintIndex, j] /= A[constraintIndex, boundedVariableIndex];
        }
        B[constraintIndex] /= A[constraintIndex, boundedVariableIndex];
    }

    private void AddConstraintToSimplexMatrix(double[,] matrix, byte constraintIndex, byte boundedVariable, double freeConstraintMember)
    {
        int row = new List<byte>(BaseVariableIndexes).IndexOf(boundedVariable)+1;
        SimplexMatrix[row][0] = freeConstraintMember;
        for (int j = 0; j < FreeVariableIndexes.Length; j++)
        {
            SimplexMatrix[row][j+1] = matrix[constraintIndex, FreeVariableIndexes[j]];
        }
    }

    private byte GetBestUnusedConstraint(double[,] matrix, List<byte> unusedBaseVariables)
    {
        ConstraintRow row;
        for (byte i = 0; i < matrix.GetLength(0); i++)
        {
            row = new ConstraintRow(matrix, unusedBaseVariables, i);
            if (row._dependentFreeVariables == 1) return row;
        }
        throw new ApplicationException("Something went wrong with constraints :((");
    }

    private byte FindBoundedBaseVariable(double[,] matrix, List<byte> unusedBaseVariables, byte constraintIndex)
    {
        foreach (byte varIndex in unusedBaseVariables)
        {
            if (matrix[constraintIndex, varIndex] != 0) return varIndex;
        }

        throw new ApplicationException("Seems like there is no base variables in the constraint.");
    }
}