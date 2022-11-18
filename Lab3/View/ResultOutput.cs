using Lab3.Model.Entities;
using Lab3.Model.EnumsAndModels;

namespace Lab3.View;

public static class ResultOutput
{ 
    public static void OutputResult(FieldModel field, Position position)
    {
        char[,] visualizedField = new char[field.Height + 2, field.Width + 2];
        
        for (int i = 1; i < field.Height+1; i++)
        {
            visualizedField[i, 0] = visualizedField[i, field.Width + 1] = '|';
        }
        for (int i = 1; i < field.Width+1; i++)
        {
            visualizedField[0, i] = visualizedField[field.Height + 1, i] = '-';
        }

        visualizedField[0, 0] = visualizedField[field.Height + 1, 0] = visualizedField[0, field.Width + 1] =
            visualizedField[field.Height + 1, field.Width + 1] = '+';

        for (short i = 0; i < field.Height; i++)
        {
            for (short j = 0; j < field.Width; j++)
            {
                visualizedField[i + 1, j + 1] = field[i, j].IsWall ? '■' : ' ';
            }
        }

        foreach ((short, short) entry in new [] { field.StartPoint, field.EndPoint })
        {
            visualizedField[entry.Item1+1, entry.Item2+1] = 'X';
        }
        
        foreach ((short, short) point in position.EnemiesCoordinates)
        {
            visualizedField[point.Item1+1, point.Item2+1] = '*';
        }

        visualizedField[position.PlayerCoordinates.Item1+1, position.PlayerCoordinates.Item2+1] = '+';

        Logger logger = new();
        logger.Log("Distance to finish: "+position.DistanceToFinish+"\n");
        logger.Log("Position value: "+position.Value()+"\n");
        for (int i = 0; i < visualizedField.GetLength(0); i++)
        {
            for (int j = 0; j < visualizedField.GetLength(1); j++)
            {
                logger.Log(visualizedField[i,j].ToString());
            }

            logger.NewLine();
        }
        logger.Close();
    }
}