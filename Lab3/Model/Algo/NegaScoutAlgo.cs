using Lab3.Model.Entities;

namespace Lab3.Model.Algo;

public class NegaScoutAlgo : DecisionTree
{
    public NegaScoutAlgo(Position startState, short firstI, short firstJ, int currentMaxDepth, Game gm) : base(startState, firstI, firstJ, currentMaxDepth, gm) { }

    protected override int GetNextMoveNodeValue(Node node, int depth)
    {
        Node move = NegaScout(node, depth, Int32.MinValue/2-1, Int32.MaxValue/2+1, 1).Item2;
        return move.Value;
    }

    private (int, Node) NegaScout(Node node, int depth, int alpha, int beta, int color)
    {
        int b, t;
        if (depth == 0 || node.IsTerminal)
        {
            node.Value = node.CurrentState.Value() * color;
            return (node.Value, node);
        }

        Node move = node;
        bool firstChildProcessed = false;
        foreach (Node child in node.PossibleNextMoves!)
        {
            if (!firstChildProcessed) t = -NegaScout(child, depth - 1, -beta, -alpha, -color).Item1;
            else
            {
                t = -NegaScout(child, depth - 1, -alpha - 1, -alpha, -color).Item1;
                if (t > alpha && t < beta && depth > 1)
                {
                    b = -NegaScout(child, depth - 1, -beta , -t, -color).Item1;
                    t = Math.Max(t, b);
                }
            }
            if (alpha < t)
            {
                alpha = t;
                move = child;
            }
            if (alpha >= beta) break;
            firstChildProcessed = true;
        }

        node.Value = move.Value;
        return (alpha, move);
    }
}