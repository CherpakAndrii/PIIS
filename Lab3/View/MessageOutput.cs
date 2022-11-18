namespace Lab3.View;

public class MessageOutput
{
    private static readonly string[] InputErrorMessages =
    {
        "Something went wrong :(",
        "The filename can not be empty!", 
        "Such file doesn't exist!",
        "The file can not be empty!",
        "Invalid file structure!",
        "The coordinates can not be empty!",
        "Invalid arguments format!",
        "The coordinates are outside the field!",
        "A wall can not be set as an entry point!",
        "You can't place more than 4 or less than 0 enemies!",
        "Seems like there is no possible moves!"
    };

    public void PrintChosenOption(string[] variants, int chosen = 0)
    {
        if (variants.Length < 1) throw new ArgumentException("Can`t choose between no options");
        Console.Write('\r');
        for (int i = 0; i < variants.Length; i++)
        {
            Console.Write(" | ");
            if (chosen == i) Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(variants[i]);
            Console.ResetColor();
        }
    }

    public void InputErrorMessageOutput(byte error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error is > 0 and < 11 ? InputErrorMessages[error] : InputErrorMessages[0]);
        Console.ResetColor();
    }
}