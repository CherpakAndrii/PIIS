namespace Lab3.Model.Entities;

public struct Position
{ 
    public (short, short) PlayerCoordinates { get; }
    public (short, short)[] EnemiesCoordinates { get; }
    public readonly int[] DistancesToEnemies;
    public readonly int DistanceToFinish;
    

    public Position(Game game, (short, short) playerPosition, (short, short)[] enemiesCoordinates)
    {
        PlayerCoordinates = playerPosition;
        EnemiesCoordinates = enemiesCoordinates;
        DistanceToFinish = game.SearchPathLength(PlayerCoordinates, game.Finish);
        if (DistanceToFinish == -1) throw new ApplicationException("There is no path to target!");
        DistancesToEnemies = new int[enemiesCoordinates.Length];
        for (int i = 0; i < EnemiesCoordinates.Length; i++)
        {
            DistancesToEnemies[i] = game.SearchPathLength(EnemiesCoordinates[i], PlayerCoordinates);
        }
    }

    public readonly int Value()
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