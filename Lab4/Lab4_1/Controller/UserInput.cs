using Lab4_1.Model;
using Lab4_1.Model.Helpers;

namespace Lab4_1.Controller;

public class UserInput
{
    public string GetSourceSequence()
    {
        Console.Write("Enter the source sequence: ");
        string? sequence = Console.ReadLine();
        Validator validator = new Validator();
        while (!validator.ValidateInputSequence(sequence))
        {
            Console.Write("Invalid input! Please, enter more than 8 digits without any non-digital characters: ");
            sequence = Console.ReadLine();
        }

        return sequence!;
    }
    
    public string GetTargetSequence()
    {
        Console.Write("Enter the target sequence: ");
        string? sequence = Console.ReadLine();
        Validator validator = new Validator();
        while (!validator.ValidateGoalSequence(sequence))
        {
            Console.Write("Invalid input! Please, enter 6-8 digits without any non-digital characters: ");
            sequence = Console.ReadLine();
        }

        return sequence!;
    }
}