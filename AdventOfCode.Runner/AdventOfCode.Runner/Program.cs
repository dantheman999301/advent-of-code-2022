// See https://aka.ms/new-console-template for more information

using AdventOfCode.Runner;
using AdventOfCode.Shared;

Console.WriteLine("Advent of Code!");
Console.WriteLine();

var selected = false;
while (!selected)
{
    Console.WriteLine("Select the day you wish to run");
    var input = Console.ReadLine();
    var isNumber = int.TryParse(input, out var dayToRun);
    
    if (!isNumber && dayToRun is > 0 and < 26)
    {
        Console.WriteLine("Unknown day, please try again");
        continue;
    }

    var dailyInput = AdventInputRetriever.GetInputForDay(dayToRun);
    if (dailyInput.IsFailed)
    {
        Console.WriteLine("Could not retrieve input for day, please try again");
        selected = false;
        continue;
    }
    
    await DayRunner.Run(dayToRun, dailyInput.Value);
    
    selected = true;

    Console.WriteLine();
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
}


