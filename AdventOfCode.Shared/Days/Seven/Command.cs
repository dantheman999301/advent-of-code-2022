namespace AdventOfCode.Shared.Days.Seven;

public class Command
{
    public required CommandType Type { get; init; }
        
    public string? Arguments { get; init; }
}