namespace Lab3.Model;

public class NegaScoutAlgo : DecisionTree
{
    public NegaScoutAlgo(Position startState, short firstI, short firstJ, int currentMaxDepth, Game gm) : base(startState, firstI, firstJ, currentMaxDepth, gm) { }

    protected override int GetNextMoveNodeValue(Node node, int depth)
    {
        return NegaScout(node, depth, Int32.MinValue/2, Int32.MaxValue/2, 1);
    }

    private int NegaScout(Node node, int depth, int alpha, int beta, int color)
    {
        int b, t;
        if (depth == 0 || node.IsTerminal)
        {
            node.Value = node.CurrentState.Value() * color;
            return node.Value;
        }
        b = beta;
        bool firstChildProcessed = false;
        foreach (Node child in node.PossibleNextMoves)
        {
            t = -NegaScout(child, depth-1, -b, -alpha, -color);
            if (t > alpha && t < beta && firstChildProcessed)
                t = -NegaScout(child, depth-1, -beta, -alpha, -color);
            alpha = Math.Max(alpha, t);
            if (alpha >= beta)
                return alpha;
            b = alpha + 1;
            firstChildProcessed = true;
        }

        return alpha;
    }
}