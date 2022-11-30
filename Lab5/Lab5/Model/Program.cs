using Lab5.View;

namespace Lab5.Model;

public static class Program
{
    public static void Main()
    {
        //input
        
        // double[,] A = new double[,] { { 2, 0, 1, 0, 1 }, { 2, 0, 0, 1, 0 }, { 5, 1, 1, 0, 3} };
        // double[] B = new double[] { 5, 3, 8 };
        // double[] C = new double[] { -2, 0, 1, -1, -2 };
        
        //
        // double[,] A = new double[,] { { 0, 3, 1, 1, 1 }, { 4, 3, 0, 1, 0 }, {3, -2, 0, 0, 1 } };
        // double[] B = new double[] { 20, 12, 6 };
        // double[] C = new double[] { -1, -7, -2, -1, 1 };
        //
        
        double[,] A = new double[,] { { 3, -1, 0, 6, 1 }, { 1, 0, 5, 0, -7 }, { 1, 0, 3, 1, 0 } };
        double[] B = new double[] { 6, 6, 6 };
        double[] C = new double[] { 0, -6, -1, 1, 0 };
        
        SimplexMethod algo = new SimplexMethod();
        (double, double[])? result = algo.Solve(A, B, C);
        new ResultOutput().PrintResult(result);
    }
}