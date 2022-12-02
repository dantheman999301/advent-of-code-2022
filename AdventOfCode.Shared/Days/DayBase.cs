using FluentResults;

namespace AdventOfCode.Shared.Days;

public interface IDayBase
{
    Task<Result<string>> RunPartOne(Stream input);

    Task<Result<string>> RunPartTwo(Stream input);
}
