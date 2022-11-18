using Lab3.Controller;
using Lab3.Model.EnumsAndModels;
using Lab3.Model.Helpers;
using Lab3.View;

namespace Lab3.Model;

public static class Program
{
    public static void Main()
    {
        UserInput.GetInputs(out ValidatedFileModel filename, out Method method, out AdjacencyType adjacency);
        FieldModel field = FieldFactory.GetField(filename);
        UserInput.GetEntries(out (short, short) startPoint, out (short, short) endPoint, field);
        field.SetEntryPoints(startPoint, endPoint);
        Game game = new Game(field, startPoint, endPoint, adjacency, method);
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