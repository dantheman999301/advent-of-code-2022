using AdventOfCode.Challenges.Extensions;
using FluentResults;

namespace AdventOfCode.Challenges.Days.Year2022.Day09;

public class Day09 : IDay
{
    public int DayNumber => 9;
    
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        return await RunWithRopeLength(input, 2);
    }
    
    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        return await RunWithRopeLength(input, 10);
    }

    private static async Task<Result<string>> RunWithRopeLength(Stream input, int ropeLength)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        var pointsVisited = new HashSet<Point>();

        var rope = new Rope(ropeLength);

        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (line.IsNullOrEmpty()) continue;

            var instructions = line.SplitBySpace();
            var instruction = new Instruction(instructions[0].ToDirection(), int.Parse(instructions[1]));

            pointsVisited.UnionWith(rope.Move(instruction));
        }

        return Result.Ok(pointsVisited.Count.ToString());
    }
}

