using Lab3.Model;
using Lab3.View;

namespace Lab3.Controller;

public static class UserInput
{
    public static void GetInputs(out ValidatedFileModel filename, out Method method, out AdjacencyType adjacency)
    {
        filename = new ValidatedFileModel("data.csv");//GetFileName();
        method = GetMethod();
        adjacency = AdjacencyType.EdgesOnly;//GetAdjacencyType();
    }

    public static void GetEntries(out (short, short) start, out (short, short) finish, FieldModel field)
    {
        start = (7, 11);//GetEntryPoint(field, true);
        finish = (1, 13);//GetEntryPoint(field, false);
    }
    private static ValidatedFileModel GetFileName()
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
    
    public static int GetEnemyNumber()
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
    
    public static EnemyBehaviorPattern GetEnemyBehavior()
    {
        Console.WriteLine("Choose the behavioral pattern of enemies, please.");
        bool firstIsChosen = true;
        while (true)
        {
            MessageOutput.PrintChosenMethod("Random moves", "Searching path to player", firstIsChosen);
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Console.Clear();
                    return firstIsChosen ? EnemyBehaviorPattern.RandomMoves : EnemyBehaviorPattern.PathToPlayer;
                case ConsoleKey.RightArrow: case ConsoleKey.DownArrow:
                    firstIsChosen = false;
                    break;
                case ConsoleKey.LeftArrow: case ConsoleKey.UpArrow:
                    firstIsChosen = true;
                    break;
            }
        }
    }

    private static Method GetMethod()
    {
        Console.WriteLine("Choose the method, please.");
        bool? firstIsChosen = true;
        while (true)
        {
            MessageOutput.PrintChosenMethod("Nega-max", "Nega-Alpha-beta pruning", "Nega-scout", firstIsChosen);
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Console.Clear();
                    return firstIsChosen is null ? Method.AlphaBeta : firstIsChosen.Value? Method.NegaMax : Method.NegaScout;
                case ConsoleKey.RightArrow: case ConsoleKey.DownArrow:
                    if (firstIsChosen is not null && firstIsChosen.Value) firstIsChosen = null;
                    else firstIsChosen = false;
                    break;
                case ConsoleKey.LeftArrow: case ConsoleKey.UpArrow:
                    if (firstIsChosen is not null && !firstIsChosen.Value) firstIsChosen = null;
                    else firstIsChosen = true;
                    break;
            }
        }
    }
    
    private static AdjacencyType GetAdjacencyType()
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

    private static (short, short) GetEntryPoint(FieldModel field, bool isStart)
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