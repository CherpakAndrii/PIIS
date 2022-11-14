namespace Lab3.Model;

public class NegaMaxAlgo : DecisionTree
{
    public NegaMaxAlgo(Position ss, short i, short j, int md, Game g) : base(ss, i, j, md, g) {}
    protected override int GetNextMoveNodeValue(Node node, int depth)
    {
        return NegaMax(node, depth, 1);
    }

    private int NegaMax(Node node, int depth, int color)
    {
        if (depth == 0 || node.IsTerminal)
        {
            node.Value = (int)node.CurrentState.Value() * color;
        }
        else
        {
            node.Value = Int32.MinValue / 2;
            foreach (Node child in node.PossibleNextMoves)
            {
                int newValue = -NegaMax(child, depth - 1, -color);
                if (newValue > node.Value) node.Value = newValue;
            }
        }

        return node.Value;
    }
}