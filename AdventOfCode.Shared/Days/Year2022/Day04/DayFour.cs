using FluentResults;

namespace AdventOfCode.Shared.Days.Year2022.Day04;

public class DayFour : IDay
{
    public int DayNumber => 4;
    
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        return await GetResult(input, false);
    }
    
    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        return await GetResult(input, true);
    }

    private static async Task<Result<string>> GetResult(Stream input, bool countAnyOverlap)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        var fullyContainedRangeCount = 0;
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;
            
            var (rangeOne, rangeTwo) = ParseIntoRanges(line);
            if (countAnyOverlap)
            {
                if (rangeOne.Overlaps(rangeTwo))
                {
                    fullyContainedRangeCount++;    
                }
            }
            else
            {
                if (IsEitherRangeSubsetOfOther(rangeOne, rangeTwo))
                {
                    fullyContainedRangeCount++;    
                }
            }
        }

        return fullyContainedRangeCount.ToString();
    }

    private static (Range rangeOne, Range rangeTwo) ParseIntoRanges(string line)
    {
        var ranges = line.Split(',');
        return (SplitIntoRange(ranges[0]), SplitIntoRange(ranges[1]));
    }

    private static Range SplitIntoRange(string rangeStr)
    {
        var values = rangeStr.Split('-').Select(int.Parse).ToArray();
        return new Range(values[0], values[1]);
    }
    
    private static bool IsEitherRangeSubsetOfOther(Range a, Range b) =>
        a.IsSubsetOf(b) || b.IsSubsetOf(a);
}
