using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentResults;

namespace AdventOfCode.Shared.Days.Five;

public class DayFive : IDay
{
    public int DayNumber => 5;
    
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        return await MoveStacks(input, false);
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        return await MoveStacks(input, true);
    }

    private static async Task<Result<string>> MoveStacks(Stream input, bool canMoveMultiple)
    {
        var stackLines = new Stack<string>();
        var amountOfStacks = 0;
        var moves = new List<Move>(); 
        var parsedInitialStacks = false;
        
        using var streamReader = new StreamReader(input, leaveOpen: true);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (!parsedInitialStacks)
            {
                if (IsEndOfStacks(line))
                {
                    amountOfStacks = int.Parse(line[^2].ToString());
                    await streamReader.ReadLineAsync(); // Empty line next
                    parsedInitialStacks = true;
                    continue;
                }
                
                stackLines.Push(line!);
                continue;
            }
            
            if(string.IsNullOrEmpty(line)) continue;

            moves.Add(new Move(line));
        }

        IList<Stack<char>> stacks = ProcessIntoStacks(amountOfStacks, stackLines);

        if (canMoveMultiple)
        {
            ApplyMovesMultiple(stacks, moves);
        }
        else
        {
            ApplyMoves(stacks, moves);    
        }
        

        return Result.Ok(PeekTops(stacks));
    } 
    
    private static List<Stack<char>> ProcessIntoStacks(int amountOfStacks, Stack<string> stackLines)
    {
        var stacks = new List<Stack<char>>(Enumerable.Range(0, amountOfStacks).Select(_ => new Stack<char>()));
        
        foreach (var stackLine in stackLines)
        {
            for (int strIndex = 0, stackIndex = 0; strIndex < stackLine.Length; strIndex += 4, stackIndex++)
            {
                if (stackLine[strIndex] != '[') continue;
                
                stacks[stackIndex].Push(stackLine[strIndex + 1]);
            }
        }

        return stacks;
    }

    private static void ApplyMoves(IList<Stack<char>> stacks, List<Move> moves)
    {
        foreach (var move in moves)
        {
            for (var i = 0; i < move.Count; i++)
            {
                stacks[move.To - 1].Push(stacks[move.From - 1].Pop());
            }
        }
    }
    
    private static void ApplyMovesMultiple(IList<Stack<char>> stacks, List<Move> moves)
    {
        foreach (var move in moves)
        {
            var tempStack = new Stack<char>();
            for (var i = 0; i < move.Count; i++)
            {
                tempStack.Push(stacks[move.From - 1].Pop());
            }

            foreach (var crate in tempStack)
            {
                stacks[move.To - 1].Push(crate);    
            }
        }
    }
    
    private static string PeekTops(IEnumerable<Stack<char>> stacks)
    {
        var builder = new StringBuilder();
        foreach (var stack in stacks)
        {
            builder.Append(stack.Peek());
        }

        return builder.ToString();
    }

    private static bool IsEndOfStacks([NotNullWhen(true)] string? line) =>
        !string.IsNullOrEmpty(line) && line[1] == '1';

    private class Move
    {
        public Move(string input)
        {
            var (count, from, to) = Parse(input);
            Count = count;
            From = from;
            To = to;
        }

        private static (int count, int from, int to) Parse(string input)
        {
            var splitInput = input.Split(' ');
            var count = int.Parse(splitInput[1]);
            var from = int.Parse(splitInput[3]);
            var to = int.Parse(splitInput[5]);

            return (count, from, to);
        }

        public int Count { get; }
        
        public int From { get; }
        
        public int To { get; }
    }
}
