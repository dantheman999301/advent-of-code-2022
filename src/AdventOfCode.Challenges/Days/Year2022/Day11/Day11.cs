using System.Collections;
using AdventOfCode.Challenges.Extensions;
using FluentResults;

namespace AdventOfCode.Challenges.Days.Year2022.Day11;

public class Day11 : IDay
{
    public int DayNumber => 11;
    
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        var monkeys = await ParseInput(input);
        
        const int maxRounds = 20;
        
        foreach (var monkey in monkeys)
        {
            Console.WriteLine(monkey.ToStarter());
            Console.WriteLine();
        }

        for (var round = 1; round <= maxRounds; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var throwTos in monkey.InspectItems())
                {
                    monkeys[throwTos.Monkey].ThrownItem(throwTos.Item);
                }
            }

            Console.WriteLine($"Round {round}");
            for (var index = 0; index < monkeys.Length; index++)
            {
                var monkey = monkeys[index];
                Console.WriteLine($"Monkey {index} | {monkey}");
            }

            Console.WriteLine();
        }

        var result =
            monkeys.OrderByDescending(m => m.Inspections)
                .Select(m => m.Inspections)
                .Take(2)
                .ToArray()
                .Aggregate((current, next) => current * next);

        return Result.Ok(result.ToString());
    }

    public Task<Result<string>> RunPartTwo(Stream input)
    {
        throw new NotImplementedException();
    }

    private static async Task<Monkey[]> ParseInput(Stream input)
    {
        var monkeys = new List<Monkey>();
        using var streamReader = new StreamReader(input, leaveOpen: true);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (!line?.StartsWith("Monkey") ?? true) continue;
            var monkey = await ParseMonkey(monkeys.Count, streamReader);
            monkeys.Add(monkey);
        }

        return monkeys.ToArray();
    }

    private static async Task<Monkey> ParseMonkey(int currentMoney, TextReader reader)
    {
        var line = await reader.ReadLineAsync();
        var items = line!
            .Split(':')[1]
            .Split(',')
            .Select(s => long.Parse(s.Trim()))
            .ToList();

        line = await reader.ReadLineAsync();
        var operationArgs = line!.Split('=')[1].Trim().SplitBySpace();
        var left = operationArgs[0] == "old" ? (int?) null : int.Parse(operationArgs[0]);
        var right = operationArgs[2] == "old" ? (int?) null : int.Parse(operationArgs[2]);
        var sign = operationArgs[1] == "+" ? Sign.Addition : Sign.Multiply;
        var operation = new Operation(left, sign, right);

        line = await reader.ReadLineAsync();
        var divisibleBy = int.Parse(line!.Trim().SplitBySpace()[^1]);

        line = await reader.ReadLineAsync();
        var trueThrow = int.Parse(line!.Trim().SplitBySpace()[^1]);
        
        line = await reader.ReadLineAsync();
        var falseThrow = int.Parse(line!.Trim().SplitBySpace()[^1]);

        var test = new ThrowToTest(divisibleBy, trueThrow, falseThrow);

        return new Monkey(currentMoney, items, operation, test);
    }
}

public class Monkey
{
    private readonly int _id;
    private readonly Queue<long> _items;
    private readonly Operation _operation;
    private readonly ThrowToTest _throwToTest;

    public int Inspections { get; private set; }

    public Monkey(int id, IEnumerable<long> items, Operation operation, ThrowToTest throwToTest)
    {
        _id = id;
        _items = new Queue<long>(items);
        _operation = operation;
        _throwToTest = throwToTest;

        Inspections = 0;
    }

    public void ThrownItem(long item)
    {
        _items.Enqueue(item);
    }

    public IEnumerable<ThrowTo> InspectItems()
    {
        while (_items.TryDequeue(out var item))
        {
            Inspections++;
            var worryLevel = _operation.Perform(item);
            var monkeyToThrowTo = _throwToTest.RunTest(worryLevel);
            yield return new ThrowTo(monkeyToThrowTo, worryLevel);
        }
    }

    public override string ToString()
    {
        return $"Inspections {Inspections} | Items: {string.Join(", ", _items)}";
    }

    public string ToStarter()
    {
        return $"""
        Monkey {_id}:
          Starting items: {string.Join(", ", _items)}
          Operation: new = {_operation}
          Test: divisible by {_throwToTest.DivisibleBy}
            If true: throw to monkey {_throwToTest.TrueMonkey}
            If false: throw to monkey {_throwToTest.FalseMonkey}
        """;
    }
};

public record ThrowToTest(int DivisibleBy, int TrueMonkey, int FalseMonkey)
{
    public int RunTest(long worryLevel) => worryLevel % DivisibleBy == 0 ? TrueMonkey : FalseMonkey;
}

public record ThrowTo(int Monkey, long Item);

public record Operation(long? LeftHand, Sign Sign, long? RightHand)
{
    public long Perform(long currentLevel)
    {
        var leftHand = LeftHand ?? currentLevel;
        var rightHand = RightHand ?? currentLevel;

        var worryLevel = Sign switch
        {
            Sign.Addition => leftHand + rightHand,
            Sign.Multiply => leftHand * rightHand,
            _ => throw new ArgumentOutOfRangeException()
        };

        worryLevel /= 3;

        return worryLevel;
    }

    public override string ToString()
    {
        return $"{LeftHand?.ToString() ?? "old"} {(Sign == Sign.Multiply ? "*" : "+")} {RightHand?.ToString() ?? "old"}";
    }
}

public enum Sign
{
    Addition,
    Multiply
}
