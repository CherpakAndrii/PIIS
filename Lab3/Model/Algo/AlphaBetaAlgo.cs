namespace Lab3.Model;

public class AlphaBetaAlgo : DecisionTree
{
    public AlphaBetaAlgo(Position ss, short i, short j, int md, Game g) : base(ss, i, j, md, g) {}
    protected override int GetNextMoveNodeValue(Node node, int depth)
    {
        return AlphaBeta(node, depth);
    }

    private int AlphaBeta(Node node, int depth, int α = Int32.MinValue/2, int β = Int32.MaxValue/2)
    {
        if (depth == 0 || node.IsTerminal)
        {
            node.Value = (int)node.CurrentState.Value();
        }
        else if (node.DepthLevel % 2 == 0)
        {
            node.Value = Int32.MinValue/2;
            foreach (Node child in node.PossibleNextMoves)
            {
                int newValue = AlphaBeta(child, depth - 1, α, β);
                if (newValue > node.Value) node.Value = newValue;
                if (α < node.Value) α = node.Value;
                if (node.Value >= β) break;
            }
        }
        else
        {
            node.Value = Int32.MaxValue/2;
            foreach (Node child in node.PossibleNextMoves)
            {
                int newValue = AlphaBeta(child, depth - 1, α, β);
                if (node.Value > newValue) node.Value = newValue;
                if (β > node.Value) β = node.Value;
                if (node.Value <= α) break;
            }
        }

        return node.Value;
    }
}