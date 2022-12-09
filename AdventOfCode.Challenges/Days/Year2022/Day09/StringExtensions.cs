namespace AdventOfCode.Challenges.Days.Year2022.Day09;

public static class StringExtensions
{
    public static Direction ToDirection(this string @char) => @char switch
    {
        "U" => Direction.Up,
        "D" => Direction.Down,
        "L" => Direction.Left,
        "R" => Direction.Right,
        _ => throw new ArgumentOutOfRangeException(nameof(@char), @char, null)
    };
}
