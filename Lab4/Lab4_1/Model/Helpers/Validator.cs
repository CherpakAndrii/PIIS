using System.Text.RegularExpressions;

namespace Lab4_1.Model.Helpers;

public class Validator
{
    public bool ValidateInputSequence(string? input)
    {
        return input is not null && Regex.IsMatch(input, @"^\d{8,}$");
    }
    
    public bool ValidateGoalSequence(string? input)
    {
        return input is not null && Regex.IsMatch(input, @"^\d{6,8}$");
    }
}