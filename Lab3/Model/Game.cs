namespace Lab3.Model;

public class Game
{
    public readonly FieldModel Field;
    public readonly (short, short) Start;
    public readonly (short, short) Finish;
    public PathSearcher PathSearchAlgo;
    public AdjacencyType Adjacency;
    public DecisionTree DecisionTree;

    public Game(FieldModel field, (short, short) start, (short, short) finish, AdjacencyType adjacency, Method method, byte enemyNumber = 1)
    {
        Field = field;
        Start = start;
        Finish = finish;
        Adjacency = adjacency;
        PathSearchAlgo = new AStar(new FieldModel(Field), adjacency);
        (short, short)[] enemies = new (short, short)[enemyNumber];
        for (int i = 0; i < enemyNumber; i++)
            enemies[i] = TryPlaceEnemy(Field, start);

        Position startPosition = new Position(this, start, enemies);
        DecisionTree = method == Method.NegaMax ?
            new NegaMaxAlgo(startPosition, enemies[0].Item1, enemies[0].Item2, 6, this) :
            method == Method.AlphaBeta ?
            new NegaAlphaBetaAlgo(startPosition, enemies[0].Item1, enemies[0].Item2, 6, this):
            new NegaScoutAlgo(startPosition, enemies[0].Item1, enemies[0].Item2, 6, this);
    }

    public Position GetNewPosition(short newI, short newJ, Position position, bool myMove = false, short enemyIndex = 0)
    {
        if (!myMove) return new Position(this, (newI, newJ), position.EnemiesCoordinates);
        (short, short)[] enemies = new (short, short)[position.EnemiesCoordinates.Length];
        position.EnemiesCoordinates.CopyTo(enemies, 0);
        enemies[enemyIndex] = (newI, newJ);
        return new Position(this, position.PlayerCoordinates, enemies);
    }

    private static (short, short) TryPlaceEnemy(FieldModel field, (short, short) playerPosition)
    {
        short i, j, ctr = 0;
        Random random = new Random();
        do
        {
            i = (short)random.Next(field.Height);
            j = (short)random.Next(field.Width);
            ctr++;
        } while ((field[i, j].IsWall || Math.Abs(i-playerPosition.Item1)+Math.Abs(j-playerPosition.Item2) < 3) && ctr < field.Height*field.Width*2);

        if (ctr > field.Height * field.Width * 2 - 1) throw new ArgumentException("Impossible to place enemies");
        return (i, j);
    }

    public bool GetNextMoveCoordinates(out short i, out short j, int moveCtr)
    {
        if (moveCtr % 2 == 0)
            return DecisionTree.ChooseNextMove(out i, out j, 5);
        
        FieldModel PathToPlayer = new FieldModel(Field);
        PathToPlayer.SetEntryPoints(DecisionTree.Root.CurrentState.EnemiesCoordinates[0], DecisionTree.Root.CurrentState.PlayerCoordinates);
        RouteModel? route = PathSearchAlgo.FindPath(PathToPlayer);
        if (route is null)
        {
            i = j = -1;
            Console.WriteLine("route is null");
            return false;
        }
        (i, j) = route.Route[1];
        return true;
    }
}

