using AdventOfCode.Challenges.Extensions;
using FluentResults;

namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public class Day12 : IDay
{
    public int DayNumber => 12;

    public async Task<Result<string>> RunPartOne(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        var lines = new List<string>();
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (line.IsNullOrEmpty()) continue;

            lines.Add(line);
        }
        

        var grid = Grid.Parse(lines);

        var solver = new PartOneSolver(grid);
        return solver.TrySolve(out var end) ? Result.Ok(end.GetPath().Count.ToString()) : Result.Fail("Failed to find path");
    }

    public Task<Result<string>> RunPartTwo(Stream input)
    {
        throw new NotImplementedException();
    }
}
