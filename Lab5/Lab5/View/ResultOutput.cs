using System.Text;

namespace Lab5.View;

public class ResultOutput
{
    public void PrintResult((double, double[])? result)
    {
        if (result is null) Console.WriteLine("The function is unbounded from below, so there is no solution!");
        else
        {
            double minValue = Math.Round(result.Value.Item1, 8);
            double[] basisVector = result.Value.Item2;
            StringBuilder sb = new StringBuilder("Minimal value of function: "+minValue+"\nBasis vector: [", 100);
            
            int ctr;
            for (ctr = 0; ctr < basisVector.Length-1; ctr++)
            {
                sb.Append(Math.Round(basisVector[ctr], 3).ToString().Replace(',', '.') + ", ");
            }
            sb.Append(Math.Round(basisVector[ctr], 3).ToString().Replace(',', '.') + "]\n");

            Console.WriteLine(sb);
        }
    }
}