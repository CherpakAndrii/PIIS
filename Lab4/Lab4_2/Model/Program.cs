using System.Runtime.Serialization.Json;
using Lab4_2.Controller;
using Lab4_2.Model;
using Lab4_2.View;

UserInput ui = new UserInput();
(List<string> inputs, int graphSize) = ui.GetGraphInputsAndSize();
if (graphSize < 2) Environment.Exit(1);
int[,] adjacenceMatrix = new GraphFactory().GetGraph(inputs, graphSize);
Dijkstra algo = new Dijkstra(adjacenceMatrix);
int start = ui.GetStart(graphSize);
Node[] vertices = algo.GetAllDistances(start);

new ResultOutput().PrintResult(vertices, start);

/*
21=4
27=4
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
61=3
76=2
72=1
83=2
84=2
87=4
done

 */