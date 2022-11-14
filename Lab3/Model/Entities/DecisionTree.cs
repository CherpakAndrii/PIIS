using System; 

namespace Lab3.Model;

public abstract partial class DecisionTree
{
    public Node Root;
    private static int _depthLvlCtr;
    internal Game game;

    public DecisionTree(Position startState, short firstI, short firstJ, int currentMaxDepth, Game gm)
    {
        game = gm;
        if (startState.DistancesToEnemies.Length > 1)
            throw new NotImplementedException("This application can't work with more than 1 enemy for now :(");
        _depthLvlCtr = currentMaxDepth;
        Root = new Node(startState, firstI, firstJ, 0, gm);
    }

    protected abstract int GetNextMoveNodeValue(Node node, int depth);

    public bool ChooseNextMove(out short i, out short j, int depth)
    {
        int bestVal = GetNextMoveNodeValue(Root, depth);
        bool found = false;
        i = j = -1;
        foreach (var node in Root.PossibleNextMoves)
        {
            if (node.Value == -bestVal && (!found || node.IsTerminal))
            {
                i = node.I;
                j = node.J;
                found = true;
                if (node.IsTerminal) return true;
            }
        }

        return found;
    }

    public void MoveIsDone(int i, int j)
    {
        foreach (Node n in Root.PossibleNextMoves)
        {
            if (n.I == i && n.J == j)
            {
                Root = n;
                //game.CurrentPosition = Root.CurrentState;
                _depthLvlCtr++;
                Root.InitializeNewNodes();
                GC.Collect();
                return;
            }
        }

        throw new IndexOutOfRangeException();
    }

    public bool RootIsTerminal() => Root.IsTerminal;
}