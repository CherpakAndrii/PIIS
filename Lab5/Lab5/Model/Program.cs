using Lab5.View;

namespace Lab5.Model;

public static class Program
{
    public static void Main()
    {
        //input
        
        // as in the lecture
        /*double[,] A = new double[,] { { 2, 0, 1, 0, 1 }, 
                                      { 2, 0, 0, 1, 0 }, 
                                      { 5, 1, 1, 0, 3} };
        double[] B = new double[] { 5, 3, 8 };
        double[] C = new double[] { -2, 0, 1, -1, -2 };*/
        
        //
        double[,] A = { { 0,  3, 1, 1, 1 },
                        { 4,  3, 0, 1, 0 }, 
                        { 3, -2, 0, 0, 1 } };
        double[] B = { 20, 12, 6 };
        double[] C = { -1, -7, -2, -1, 1 };
        //
        
        
        SimplexMethod algo = new SimplexMethod();
        (double, double[])? result = algo.Solve(A, B, C);
        new ResultOutput().PrintResult(result);
    }
}