using AdventOfCode.Challenges.Extensions;
using FluentResults;

namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public class Day12 : IDay
{
    public int DayNumber => 12;

    public async Task<Result<string>> RunPartOne(Stream input)
    {
        var grid = await ParseGrid(input);

        var solver = new PartOneSolver(grid);
        if (solver.TrySolve(out var end))
            return Result.Ok(end.GetPathLength().ToString());
        else
            return Result.Fail("Failed to find path");
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        var grid = await ParseGrid(input, 'E', 'a');

        var solver = new PartTwoSolver(grid);
        if (solver.TrySolve(out var end))
            return Result.Ok(end.GetPathLength().ToString());
        else
            return Result.Fail("Failed to find path");
    }

    private static async Task<Grid> ParseGrid(Stream input, char startChar = 'S', char endChar = 'E')
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
        return grid;
    }
}
