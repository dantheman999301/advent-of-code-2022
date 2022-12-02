using AdventOfCode.Shared.Days;
using AdventOfCode.Shared.Days.One;
using TextCopy;

namespace ConsoleApp1;

public static class DayRunner
{
    private static readonly Dictionary<int, IDayBase> Days = new()
    {
        { 1, new DayOne() }
    };

    public static async Task Run(int day, Stream input)
    {
        if (!Days.TryGetValue(day, out var parts))
        {
            Console.WriteLine("Could not find day");
            return;
        };

        var result = await parts.RunPartOne(input);
        await ClipboardService.SetTextAsync(result.Value); 
        Console.WriteLine("Part one found and copied to clipboard");
        Console.WriteLine("Press a key to move to part two");
        Console.ReadKey();

        input.Position = 0;
        result = await parts.RunPartTwo(input);
        await ClipboardService.SetTextAsync(result.Value);
        Console.WriteLine("Part two found and copied to clipboard");
    }
}
