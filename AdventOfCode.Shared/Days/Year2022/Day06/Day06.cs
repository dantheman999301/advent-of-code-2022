using FluentResults;

namespace AdventOfCode.Shared.Days.Year2022.Day06;

public class Day06 : IDay
{
    public int DayNumber => 6;
    
    public Task<Result<string>> RunPartOne(Stream input)
    {
        return Task.FromResult(FindMarker(input, 4));
    }
    
    public Task<Result<string>> RunPartTwo(Stream input)
    {
        return Task.FromResult(FindMarker(input, 14));
    }

    private static Result<string> FindMarker(Stream input, int markerSize)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        var window = InitialiseWindow(streamReader, markerSize);

        if (AllUnique(window))
        {
            return Result.Ok(markerSize.ToString());
        }

        var currentMarker = markerSize;
        while (!streamReader.EndOfStream)
        {
            var character = (char)streamReader.Read();
            currentMarker++;

            window.Enqueue(character);
            if (AllUnique(window))
            {
                break;
            }
        }

        return Result.Ok(currentMarker.ToString());
    }

    private static FixedQueue<char> InitialiseWindow(TextReader streamReader, int size)
    {
        var window = new FixedQueue<char>(size);
        for (var i = 0; i < size; i++)
        {
            window.Enqueue((char)streamReader.Read());    
        }

        return window;
    }

    private static bool AllUnique(IEnumerable<char> chars)
    {
        var set = new HashSet<char>();
        return chars.All(set.Add);
    }
}
