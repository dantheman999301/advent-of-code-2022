using FluentResults;

namespace AdventOfCode.Shared.Days.Three;

public class DayThree : IDay
{
    public int DayNumber => 3;

    public async Task<Result<string>> RunPartOne(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);

        var priority = 0;
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;

            var bag = new Bag(line);
            priority += bag
                .UniqueCharsCompartmentOne
                .Intersect(bag.UniqueCharsCompartmentTwo)
                .Sum(GetPriority);
        }

        return priority.ToString();
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);

        var priority = 0;
        var groupBags = new List<Bag>();
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;

            var bag = new Bag(line);
            groupBags.Add(bag);

            if (groupBags.Count != 3) continue;
            
            var badgeSet = new HashSet<char>(groupBags[0].AllChars);
            badgeSet.IntersectWith(groupBags[1].AllChars);
            badgeSet.IntersectWith(groupBags[2].AllChars);
            var badge = badgeSet.First();
            priority += GetPriority(badge);
            
            groupBags.Clear();
        }

        return priority.ToString();
    }

    private static int GetPriority(char @char)
    {
        var index = @char % 32;
        return char.IsUpper(@char) ? index + 26 : index;
    }

    private class Bag
    {
        public Bag(string contents)
        {
            var length = contents.Length;
            var midPoint = length / 2;
            CompartmentOne = contents[..midPoint];
            CompartmentTwo = contents[midPoint..length];
            if (CompartmentOne.Length != CompartmentTwo.Length)
            {
                throw new Exception($"Compartment lengths are not equal - {CompartmentOne} | {CompartmentTwo}");
            }
            
            UniqueCharsCompartmentOne = new HashSet<char>(CompartmentOne);
            UniqueCharsCompartmentTwo = new HashSet<char>(CompartmentTwo);
            AllChars = new HashSet<char>(contents);
        }
        
        public string CompartmentOne { get; }
        
        public string CompartmentTwo { get; }

        public HashSet<char> UniqueCharsCompartmentOne { get; }
        
        public HashSet<char> UniqueCharsCompartmentTwo { get; }
        
        public HashSet<char> AllChars { get; }
    }
}
