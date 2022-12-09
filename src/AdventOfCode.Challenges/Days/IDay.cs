using FluentResults;

namespace AdventOfCode.Challenges.Days;

public interface IDay
{
    int DayNumber { get; }
    
    Task<Result<string>> RunPartOne(Stream input);

    Task<Result<string>> RunPartTwo(Stream input);
}
