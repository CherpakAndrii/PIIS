using Lab4_3.Model;
using Lab4_3.View;
using Lab4_3.Controller;

UserInput ui = new UserInput();
(List<string> inputs, int graphSize) = ui.GetGraphInputsAndSize();
if (graphSize < 2) Environment.Exit(1);
int[,] adjacenceMatrix = new GraphFactory().GetGraph(inputs, graphSize);
Prima algo = new Prima(adjacenceMatrix);
Node[]? vertices = algo.GetMinCoverTree(out int treeWeight);

new ResultOutput().PrintResult(vertices, treeWeight);

/*
21=4
28=4
23=3
24=1
31=3
35=2
42=3
47=3
56=5
57=2
62=1
63=3
65=2
67=1
68=3
76=2
72=1
83=2
84=2
87=4
done
*/