namespace Lab2.Model;

public struct Position
{
    private Game _game;
    public (short, short) PlayerCoordinates { get; }
    public (short, short)[] EnemiesCoordinates { get; }
    public readonly int[] DistancesToEnemies;
    public readonly int DistanceToFinish;
    

    public Position(Game game, (short, short) playerPosition, (short, short)[] enemiesCoordinates)
    {
        PlayerCoordinates = playerPosition;
        EnemiesCoordinates = enemiesCoordinates;
        _game = game;
        DistanceToFinish = __searchPath(_game, PlayerCoordinates, _game.Finish);
        if (DistanceToFinish == -1) throw new ApplicationException("There is no path to target!");
        DistancesToEnemies = new int[enemiesCoordinates.Length];
        for (int i = 0; i < EnemiesCoordinates.Length; i++)
        {
            DistancesToEnemies[i] = __searchPath(_game, EnemiesCoordinates[i], PlayerCoordinates);
        }
    }

    private static int __searchPath(Game game, (short, short) start, (short, short) finish)
    {
        FieldModel PathToEndField = new FieldModel(game.Field);
        PathToEndField.SetEntryPoints(start, finish);
        return game.PathSearchAlgo.FindPathLength(PathToEndField);
    }

    public int Value()
    {
        int value = 1;
        foreach (int distance in DistancesToEnemies)
        {
            if (distance>-1)
                value *= distance;
        }

        return DistanceToFinish == 0 ? Int32.MaxValue/2 : DistancesToEnemies[0] == 0? Int32.MinValue/2 : value - 2*DistanceToFinish;
    }
}