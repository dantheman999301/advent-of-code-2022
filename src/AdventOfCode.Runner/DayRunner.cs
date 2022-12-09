using System.Reflection;
using AdventOfCode.Challenges.Days;
using FluentResults;
using Spectre.Console;
using TextCopy;

namespace AdventOfCode.Runner;

public static class DayRunner
{
    private static readonly Lazy<IDictionary<int, IDay>> Days = new(CreateDayDictionary);

    public static async Task<Result> Run(int day, Stream input)
    {
        if (!Days.Value.TryGetValue(day, out var parts))
        {
            return Result.Fail("Could not find day");
        };

        AnsiConsole.WriteLine();
        
        var result = await parts.RunPartOne(input);
        
        AnsiConsole.Write(new Rule("Part One") { Alignment = Justify.Left });
        AnsiConsole.MarkupInterpolated($"Result: [palegreen1]{result.Value}[/]");
        AnsiConsole.WriteLine();
        
        await ClipboardService.SetTextAsync(result.Value);
        
        AnsiConsole.WriteLine("Result in clipboard. Press a key to move to part two");
        AnsiConsole.WriteLine();
        Console.ReadKey();
        
        input.Position = 0;
        try
        {
            AnsiConsole.Write(new Rule("Part Two") { Alignment = Justify.Left });
            
            result = await parts.RunPartTwo(input);
            await ClipboardService.SetTextAsync(result.Value);
            
            AnsiConsole.MarkupInterpolated($"Result: [palegreen1]{result.Value}[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Result in clipboard");
        }
        catch (NotImplementedException)
        {
            AnsiConsole.WriteLine("[red]Part two not yet implemented[/]");
        }

        return Result.Ok();
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
