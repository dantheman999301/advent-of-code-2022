namespace AdventOfCode.Challenges.Days.Year2022.Day07;

public class Command
{
    public required CommandType Type { get; init; }
        
    public string? Arguments { get; init; }
}
