// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using AdventOfCode.Challenges;
using AdventOfCode.Runner;
using Spectre.Console;

AnsiConsole.Write(new Rule("🎄 [red]Advent[/] [white]of[/] [yellow]Code[/] [green]Runner[/] 🎄"));
AnsiConsole.WriteLine();

var selected = false;


while (!selected)
{
    AnsiConsole.WriteLine();
    var dayToRun = AnsiConsole.Prompt(
        new TextPrompt<int>("What [deepskyblue1]day[/] do you wish to run?")
            .PromptStyle("deepskyblue1")
            .ValidationErrorMessage("[red]Not a valid Christmas day![/]")
            .Validate(age =>
            {
                return age switch
                {
                    <= 0 => ValidationResult.Error("[red]Day should be greater than 1[/]"),
                    >= 26 => ValidationResult.Error("[red]Day should be less than 26[/]"),
                    _ => ValidationResult.Success(),
                };
            }));

    var dailyInput = AdventInputRetriever.GetInputForDay(dayToRun);
    if (dailyInput.IsFailed)
    {
        AnsiConsole.WriteException(new Exception("Input file missing"));
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        break;
    }

    var result = await DayRunner.Run(dayToRun, dailyInput.Value);
    if (result.IsFailed)
    {
        AnsiConsole.Write("[red]Could not find day, please specify again[/]");
    }

    selected = result.IsSuccess;

    Console.WriteLine();
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
}
