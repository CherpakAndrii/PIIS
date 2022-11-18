using Lab3.Model.EnumsAndModels;
using Lab3.View;

namespace Lab3.Model;

public class MainLogic
{
    private Game _game;

    public MainLogic(Game gm)
    {
        _game = gm;
    }
    public GameResult Play()
    {
        Console.Clear();
        int moveCtr = 0;
        ResultOutput resOut = new();
        while (!_game.DecisionTree.RootIsTerminal())
        {
            if (_game.GetNextMoveCoordinates(out short i, out short j, moveCtr))
            {
                _game.DecisionTree.MakeMove(i, j, moveCtr%2==0);
                moveCtr++;
                Console.Clear();
                resOut.OutputResult(_game.Field, _game.DecisionTree.Root.CurrentState);
                Thread.Sleep(500);
            }
            else
            {
                new MessageOutput().InputErrorMessageOutput(10);
                Console.ReadKey();
                Environment.Exit(10);
            }
        }
        
        if (_game.DecisionTree.Root.CurrentState.DistanceToFinish == 0) return GameResult.PlayerWins;
        foreach (int distance in _game.DecisionTree.Root.CurrentState.DistancesToEnemies)
        {
            if (distance == 0) return GameResult.PlayerLost;
        }

        return GameResult.SomethingWentWrong;
    }
}