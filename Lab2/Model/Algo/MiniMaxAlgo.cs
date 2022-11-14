namespace Lab2.Model;

public class MiniMaxAlgo : DecisionTree
{
    public MiniMaxAlgo(Position ss, short i, short j, int md, Game g) : base(ss, i, j, md, g) {}
    protected override int GetNextMoveNodeValue(Node node, int depth)
    {
        return MiniMax(node, depth);
    }

    private int MiniMax(Node node, int depth)
    {
        if (depth == 0 || node.IsTerminal)
        {
            node.Value = (int)node.CurrentState.Value();
        }
        else
        {
            bool maximize = node.DepthLevel % 2 == 0;
            node.Value = maximize ? Int32.MinValue / 2 : Int32.MaxValue / 2;
            foreach (Node child in node.PossibleNextMoves)
            {
                int newValue = MiniMax(child, depth - 1);
                if (maximize && newValue > node.Value || !maximize && newValue < node.Value) node.Value = newValue;
            }
        }

        return node.Value;
    }
}