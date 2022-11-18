using Lab3.Model.Entities;

namespace Lab3.Model.Algo;

public class NegaAlphaBetaAlgo : DecisionTree
{
    public NegaAlphaBetaAlgo(Position ss, short i, short j, int md, Game g) : base(ss, i, j, md, g) {}
    protected override int GetNextMoveNodeValue(Node node, int depth)
    {
        return -AlphaBeta(node, depth, Int32.MinValue/2, Int32.MaxValue/2, 1);
    }

    private int AlphaBeta(Node node, int depth, int α, int β, int color)
    {
        if (depth == 0 || node.IsTerminal)
        {
            node.Value = node.CurrentState.Value() * color;
        }
        else
        {
            node.Value = Int32.MinValue/2;
            foreach (Node child in node.PossibleNextMoves!)
            {
                int newValue = -AlphaBeta(child, depth - 1, -β, -α, -color);
                if (newValue > node.Value) node.Value = newValue;
                if (node.Value > α) α = node.Value;
                if (α >= β) break;
            }
        }

        return node.Value;
    }
}