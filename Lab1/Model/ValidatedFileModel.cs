namespace Lab1.Model;

public class ValidatedFileModel
{
    public string FileName { get; }

    public ValidatedFileModel(string fileName)
    {
        FileName = fileName;
    }
}