namespace AdventOfCode.Challenges.Extensions;

public static class CharExtensions
{
    public static int ToInt(this char @char)
    {
        return (int)char.GetNumericValue(@char);
    }
}
