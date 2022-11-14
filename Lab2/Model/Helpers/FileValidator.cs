using System.Text.RegularExpressions;

namespace Lab2.Model;

public static class FileValidator
{
    public static byte ValidateFile(string? filename)
    {
        if (string.IsNullOrEmpty(filename)) return 1;
        if (!File.Exists(filename)) return 2;
        StreamReader sr = new StreamReader(filename);
        if (sr.EndOfStream)
        {
            sr.Close();
            return 3;
        }
        short lineSize = (short) sr.ReadLine().Length;
        sr.Close();
        sr = new StreamReader(filename);
        while (sr.Peek()!=-1)
        {
            string line = sr.ReadLine();
            if (!Regex.IsMatch(line, $"^[0-1]{{{lineSize}}}$")) return 4;
        }
        sr.Close();
        return 0;
    }
}