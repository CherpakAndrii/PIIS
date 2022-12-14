using Lab3.Model.EnumsAndModels;
//using Lab3.Model.Helpers;
using Lab3.View;

namespace Lab3.Controller;

public class UserInput
{
    public void GetInputs(out ValidatedFileModel filename, out Method method, out AdjacencyType adjacency)
    {
        filename = new ValidatedFileModel("data.csv");//GetFileName();
        method = GetMethod();
        adjacency = AdjacencyType.EdgesOnly;//GetAdjacencyType();
    }

    public void GetEntries(out (short, short) start, out (short, short) finish, FieldModel field)
    {
        start = (7, 11);//GetEntryPoint(field, true);
        finish = (1, 13);//GetEntryPoint(field, false);
    }
    /*private ValidatedFileModel GetFileName()
    {
        while (true)
        {
            Console.WriteLine("Enter your filename or \"exit\", please:");
            string? filename = Console.ReadLine();
            
            if (filename == "exit") Environment.Exit(1);
            byte validationResult = new FileValidator().ValidateFile(filename);
            if (validationResult == 0)
            {
                Console.Clear();
                return new(filename!);
            }
            MessageOutput.InputErrorMessageOutput(validationResult);
        }
    }
    
    public int GetEnemyNumber()
    {
        while (true)
        {
            Console.WriteLine("Enter the number of enemies or \"exit\", please:");
            string? enemyNumber = Console.ReadLine();
            
            if (enemyNumber == "exit") Environment.Exit(1);
            if (int.TryParse(enemyNumber, out int n))
            {
                if (n is > -1 and < 5) return n;
                MessageOutput.InputErrorMessageOutput(9);
            }
            else MessageOutput.InputErrorMessageOutput(6);
        }
    }
    
    public EnemyBehaviorPattern GetEnemyBehavior()
    {
        Console.WriteLine("Choose the behavioral pattern of enemies, please.");
        string[] variants = { "Random moves", "Searching path to player" };
        return (EnemyBehaviorPattern)SelectBetweenOptions(variants);
    }*/
    
    private int SelectBetweenOptions(string[] variants)
    {
        int chosen = 0;
        MessageOutput messageOutput = new MessageOutput();
        messageOutput.PrintChosenOption(variants, chosen);
        while (true)
        {
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Console.Clear();
                    return chosen;
                case ConsoleKey.RightArrow: case ConsoleKey.DownArrow:
                    if (chosen < variants.Length - 1)
                    {
                        chosen++;
                        messageOutput.PrintChosenOption(variants, chosen);
                    }
                    break;
                case ConsoleKey.LeftArrow: case ConsoleKey.UpArrow:
                    if (chosen > 0)
                    {
                        chosen--;
                        messageOutput.PrintChosenOption(variants, chosen);
                    }
                    break;
            }
        }
    }

    private Method GetMethod()
    {
        Console.WriteLine("Choose the method, please.");
        string[] variants = { "Nega-max", "Nega-Alpha-beta pruning", "Nega-scout" };
        return (Method)SelectBetweenOptions(variants);
    }

    /*private AdjacencyType GetAdjacencyType()
    {
        Console.WriteLine("Would you like to allow the adjacency on the corners, or only on the edge?");
        string[] variants = { "Only edges", "Edges and corners" };
        return (AdjacencyType)SelectBetweenOptions(variants);
    }

    private (short, short) GetEntryPoint(FieldModel field, bool isStart)
    {
        while (true)
        {
            Console.Write("Enter Y and X coordinates of ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(isStart ? "start" : "finish");
            Console.ResetColor();
            Console.WriteLine(" point separated by comma, like \"1, 5\":");
            string? coordinates = Console.ReadLine();
            if (string.IsNullOrEmpty(coordinates) || coordinates.Split(", ", StringSplitOptions.RemoveEmptyEntries).Length < 2)
            {
                MessageOutput.InputErrorMessageOutput(5);
                continue;
            }

            string[] splitCoordinates = coordinates.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            if (splitCoordinates.Length != 2 || !Int16.TryParse(splitCoordinates[0], out short y) ||
                !Int16.TryParse(splitCoordinates[1], out short x))
            {
                MessageOutput.InputErrorMessageOutput(6);
                continue;
            }

            if (y < 0 || x < 0 || y > field.Height || x > field.Width)
            {
                MessageOutput.InputErrorMessageOutput(7);
                continue;
            }

            if (field[y, x].IsWall)
            {
                MessageOutput.InputErrorMessageOutput(8);
            }
            else
            {
                Console.Clear();
                return (y, x);
            }
        }
    }*/
}