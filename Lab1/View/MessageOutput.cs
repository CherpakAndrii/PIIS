namespace Lab1.View;

public static class MessageOutput
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
        "A wall can not be set as an entry point!"
    };
    
    public static void PrintChosenMethod(string firstVariant, string secondVariant, bool firstIsChosen = true)
    {
        if (firstIsChosen) Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("\r"+firstVariant);
        Console.ResetColor();
        Console.Write(" | ");
        if (!firstIsChosen) Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write(secondVariant);
        Console.ResetColor();
    }

    public static void InputErrorMessageOutput(byte error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error is > 0 and < 9 ? InputErrorMessages[error] : InputErrorMessages[0]);
        Console.ResetColor();
    }
}