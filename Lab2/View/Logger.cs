namespace Lab2.View;

public class Logger
{
    private const string OutFileName = "res.txt";
    private StreamWriter _file;

    public Logger()
    {
        _file = new StreamWriter(OutFileName, true);
    }

    public void Close()
    {
        _file.Close();
    }

    public void Log(string message)
    {
        _file.Write(message);
        Console.Write(message);
    }

    public void NewLine()
    {
        _file.WriteLineAsync('m');
        Console.WriteLine();
    }

    public static bool ClearOldLogs()
    {
        bool deleted;
        if (File.Exists(OutFileName))
        {
            File.Delete(OutFileName);
            deleted = true;
        }
        else deleted = false;

        using (File.Create(OutFileName)) { }

        return deleted;
    }
}