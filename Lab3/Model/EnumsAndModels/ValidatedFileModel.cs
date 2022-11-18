namespace Lab3.Model.EnumsAndModels;

public class ValidatedFileModel
{
    public string FileName { get; }

    public ValidatedFileModel(string fileName)
    {
        FileName = fileName;
    }
}