using System.Reflection;
using AdventOfCode.Shared.Days;
using TextCopy;

namespace AdventOfCode.Runner;

public static class DayRunner
{
    private static readonly Lazy<IDictionary<int, IDay>> Days = new(CreateDayDictionary);

    public static async Task Run(int day, Stream input)
    {
        if (!Days.Value.TryGetValue(day, out var parts))
        {
            Console.WriteLine("Could not find day");
            return;
        };

        var result = await parts.RunPartOne(input);
        await ClipboardService.SetTextAsync(result.Value);
        Console.WriteLine("Part one found and copied to clipboard. Result was:");
        Console.WriteLine(result.Value);
        Console.WriteLine("Press a key to move to part two");
        Console.ReadKey();
        
        input.Position = 0;
        try
        {
            result = await parts.RunPartTwo(input);
            await ClipboardService.SetTextAsync(result.Value);
            Console.WriteLine("Part two found and copied to clipboard. Result was");
            Console.WriteLine(result.Value);
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("Part two not yet implemented");
        }
    }

    private static IDictionary<int, IDay> CreateDayDictionary()
    {
        var dayType = typeof(IDay);
        var assembly = dayType.GetTypeInfo().Assembly;
        return assembly
            .DefinedTypes
            .Where(t => dayType.IsAssignableFrom(t) && t != dayType)
            .Select(Activator.CreateInstance)
            .Cast<IDay>()
            .ToDictionary(d => d.DayNumber, d => d);
    }
}
