using Lab2.Controller;
using Lab2.View;

namespace Lab2.Model;

public static class Program
{
    public static void Main()
    {
        UserInput.GetInputs(out ValidatedFileModel filename, out Method method, out AdjacencyType adjacency);
        FieldModel field = FieldFactory.GetField(filename);
        UserInput.GetEntries(out (short, short) startPoint, out (short, short) endPoint, field);
        field.SetEntryPoints(startPoint, endPoint);
        Game game = new Game(field, startPoint, endPoint, adjacency, method, 1);
        Logger.ClearOldLogs();
        MainLogic.Play(game);
        GameResult result = MainLogic.GetGameResult(game);
        Console.WriteLine(result == GameResult.PlayerWins?"Done!":"Failed");
        Console.ReadKey();
    }
}

/*
data.csv


7, 11
1, 13

*/