using System.Text.RegularExpressions;

namespace Lab4_3.Controller;

public class UserInput
{
    public (List<string>, int) GetGraphInputsAndSize()
    {
        string input, pattern = @"^\d\d=\d+$";
        List<string> adjacents = new List<string>();
        char maxVerticeNumber = '0';
        Console.WriteLine("Please, enter your graph:");
        while (true)
        {
            input = Console.ReadLine()!.Trim().ToLower();
            if (input == "done") return (adjacents, maxVerticeNumber-48);
            if (input == "clear")
            {
                adjacents.Clear();
            }
            else if (Regex.IsMatch(input, pattern))
            {
                adjacents.Add(input);
                maxVerticeNumber = Max(maxVerticeNumber, input[0], input[1]);
            }
            else Console.WriteLine("Incorrect input!");
        }
    }

    public int GetStart(int limit)
    {
        int startIndex;
        string input;
        do
        {
            Console.Write("Please, choose the startpoint from "+limit+" vertices: ");
            input = Console.ReadLine()!;
        } while (!Int32.TryParse(input, out startIndex) || startIndex > limit);

        return startIndex;
    }

    private char Max(char ch1, char ch2, char ch3) => ch1 >= ch2 && ch1 >= ch3 ? ch1 : ch2 >= ch3 ? ch2 : ch3;
}