using Lab4_1.Controller;
using Lab4_1.Model.Algo;
using Lab4_1.View;

UserInput ui = new UserInput();
string source = ui.GetSourceSequence();
KarpRabinAlgo algo = new KarpRabinAlgo(source);
ResultOutputter output = new ResultOutputter();
string target = ui.GetTargetSequence();
output.PrintTargetHash(algo.GetHash(target));

List<string> similarStrings = algo.GetSimilarSubstrings(target);

output.PrintResults(similarStrings);
