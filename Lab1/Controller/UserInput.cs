using Lab1.Model;
using Lab1.View;

namespace Lab1.Controller;

public static class UserInput
{
    public static ValidatedFileModel GetFileName()
    {
        while (true)
        {
            Console.WriteLine("Enter your filename or \"exit\", please:");
            string? filename = Console.ReadLine();
            
            if (filename == "exit") Environment.Exit(1);
            byte validationResult = FileValidator.ValidateFile(filename);
            if (validationResult == 0)
            {
                Console.Clear();
                return new(filename);
            }
            MessageOutput.InputErrorMessageOutput(validationResult);
        }
    }

    public static Method GetMethod()
    {
        Console.WriteLine("Choose the method, please.");
        bool firstIsChosen = true;
        while (true)
        {
            MessageOutput.PrintChosenMethod("AStar", "Li", firstIsChosen);
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Console.Clear();
                    return firstIsChosen ? Method.AStar : Method.Li;
                case ConsoleKey.RightArrow: case ConsoleKey.DownArrow:
                    firstIsChosen = false;
                    break;
                case ConsoleKey.LeftArrow: case ConsoleKey.UpArrow:
                    firstIsChosen = true;
                    break;
            }
        }
    }
    
    public static AdjacencyType GetAdjacencyType()
    {
        Console.WriteLine("Would you like to allow the adjacency on the corners, or only on the edge?");
        bool firstIsChosen = true;
        while (true)
        {
            MessageOutput.PrintChosenMethod("Only edges", "Edges and corners", firstIsChosen);
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Console.Clear();
                    return firstIsChosen ? AdjacencyType.EdgesOnly : AdjacencyType.EdgesAndCorners;
                case ConsoleKey.RightArrow: case ConsoleKey.DownArrow:
                    firstIsChosen = false;
                    break;
                case ConsoleKey.LeftArrow: case ConsoleKey.UpArrow:
                    firstIsChosen = true;
                    break;
            }
        }
    }

    public static (short, short) GetEntryPoint(FieldModel field, bool isStart)
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
    }
}