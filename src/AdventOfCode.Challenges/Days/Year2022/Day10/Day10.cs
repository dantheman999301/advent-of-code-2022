using System.Runtime.CompilerServices;
using AdventOfCode.Challenges.Extensions;
using FluentResults;

namespace AdventOfCode.Challenges.Days.Year2022.Day10;

public class Day10 : IDay
{
    public int DayNumber => 10;

    public async Task<Result<string>> RunPartOne(Stream input)
    {
        var x = 1;
        var cycles = 0;
        var signalDuringCycles = new List<int>();

        void IncrementCycle(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                cycles++;
                if (cycles == 20 || (cycles + 20) % 40 == 0)
                {
                    signalDuringCycles?.Add(x * cycles);
                }
            }
        }

        using var streamReader = new StreamReader(input, leaveOpen: true);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (line.IsNullOrEmpty()) continue;

            var splitLine = line.SplitBySpace();
            var instructionType = Enum.Parse<InstructionType>(splitLine[0]);
            switch (instructionType)
            {
                case InstructionType.noop:
                    IncrementCycle();
                    break;
                case InstructionType.addx:
                    IncrementCycle(2);
                    x += int.Parse(splitLine[1]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return Result.Ok(signalDuringCycles.Take(6).Sum().ToString());
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        var x = 1;
        var cycles = 0;
        var row = 0;
        var column = 0;

        var crt = Enumerable.Repeat(0, 6).Select(_ => Enumerable.Repeat(0, 40).Select(_ => '.').ToArray()).ToArray();

        void IncrementCycle(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                cycles++;
                (row, column) = Math.DivRem(cycles - 1, 40);
                if (Math.Abs(column - x) == 1 || Math.Abs(column - x) == 0)
                {
                    crt[row][column] = '#';
                }
            }
        }

        using var streamReader = new StreamReader(input, leaveOpen: true);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (line.IsNullOrEmpty()) continue;

            var splitLine = line.SplitBySpace();
            var instructionType = Enum.Parse<InstructionType>(splitLine[0]);
            switch (instructionType)
            {
                case InstructionType.noop:
                    IncrementCycle();
                    break;
                case InstructionType.addx:
                    IncrementCycle(2);
                    x += int.Parse(splitLine[1]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var printOut = string.Join(Environment.NewLine, crt.Select(line => new string(line)));
        return Result.Ok(printOut);
    }
}

public enum InstructionType
{
    noop,
    addx
}
