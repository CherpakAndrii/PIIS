using Lab3.Model.Entities;
using Lab3.Model.EnumsAndModels;

namespace Lab3.Model.Helpers;

public static class FieldFactory
{
    public static FieldModel GetField(ValidatedFileModel file)
    {
        string[] lines;
        using (StreamReader sr = new StreamReader(file.FileName))
        {
            lines = sr.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        Cell[][] field = new Cell[lines.Length][];
        for (short i = 0; i < field.Length; i++)
        {
            field[i] = new Cell[lines[0].Length];
            for (short j = 0; j < lines[i].Length; j++)
            {
                field[i][j] = new Cell(i, j, lines[i][j] == '0');
            }
        }

        return new FieldModel(field);
    }
}