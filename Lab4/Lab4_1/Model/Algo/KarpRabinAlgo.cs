namespace Lab4_1.Model.Algo;

public class KarpRabinAlgo
{
    private string _source;
    public KarpRabinAlgo(string source)
    {
        _source = source;
    }
    
    public List<string> GetSimilarSubstrings(string target)
    {
        int targetHash = GetHash(target);
        string substring;
        List<string> similarSubstrings = new List<string>();
        for (int i = 0; i < _source.Length - target.Length + 1; i++)
        {
            substring = _source.Substring(i, target.Length);
            if (GetHash(substring) == targetHash) similarSubstrings.Add(substring);
        }

        return similarSubstrings;
    }

    public byte GetHash(string sequence) => (byte)(int.Parse(sequence) % 28);
}