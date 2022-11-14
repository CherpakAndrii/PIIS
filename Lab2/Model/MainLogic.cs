using Lab2.View;

namespace Lab2.Model;

public static class MainLogic
{
    public static void Play(Game gm)
    {
        Console.Clear();
        int moveCtr = 0;
        while (!gm.DecisionTree.RootIsTerminal())
        {
            if (gm.GetNextMoveCoordinates(out short i, out short j, moveCtr))
            {
                MakeMove(i, j, gm, ref moveCtr);
                moveCtr++;
                Console.Clear();
                ResultOutput.OutputResult(gm.Field, gm.DecisionTree.Root.CurrentState);
                Thread.Sleep(5);
            }
            else
            {
                MessageOutput.InputErrorMessageOutput(10);
                Console.ReadKey();
                Environment.Exit(10);
            }
        }
    }
    
    private static void MakeMove(short i, short j, Game gm, ref int moveCtr)
    {
        gm.DecisionTree.MoveIsDone(i, j);
    }

    public static GameResult GetGameResult(Game gm)
    {
        if (gm.DecisionTree.Root.CurrentState.DistanceToFinish == 0) return GameResult.PlayerWins;
        foreach (int distance in gm.DecisionTree.Root.CurrentState.DistancesToEnemies)
        {
            if (distance == 0) return GameResult.PlayerLost;
        }

        return GameResult.SomethingWentWrong;
    }
}