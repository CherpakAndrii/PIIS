namespace Lab3.Model.Entities;

public abstract partial class DecisionTree
{
    public Node Root;
    private int _depthLimitCtr;

    protected DecisionTree(Position startState, short firstI, short firstJ, int currentMaxDepth, Game gm)
    {
        if (startState.DistancesToEnemies.Length > 1)
            throw new NotImplementedException("This application can't work with more than 1 enemy for now :(");
        _depthLimitCtr = currentMaxDepth;
        Root = new Node(startState, firstI, firstJ, 0, gm, _depthLimitCtr);
    }

    protected abstract int GetNextMoveNodeValue(Node node, int depth);

    public bool ChooseNextMove(out short i, out short j, int depth)
    {
        int bestVal = GetNextMoveNodeValue(Root, depth);
        bool found = false;
        i = j = -1;
        if (Root.PossibleNextMoves is null) return false;
        foreach (var node in Root.PossibleNextMoves)
        {
            if (node.Value == bestVal && (!found || node.IsTerminal))
            {
                i = node.I;
                j = node.J;
                found = true;
                if (node.IsTerminal) return true;
            }
        }

        return found;
    }

    public void MakeMove(int i, int j, bool initializingNeeded)
    {
        foreach (Node n in Root.PossibleNextMoves!)
        {
            if (n.I == i && n.J == j)
            {
                Root = n;
                _depthLimitCtr++;
                if (initializingNeeded) Root.InitializeNewNodes(_depthLimitCtr);
                return;
            }
        }

        throw new IndexOutOfRangeException();
    }

    public bool RootIsTerminal() => Root.IsTerminal;
}