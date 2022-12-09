namespace AdventOfCode.Challenges.Days.Year2022.Day07;

public class File : ISystemEntry
{
    public string Name { get; init; }
    
    public int Size { get; init; }
    
    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public override string ToString() => $"{Name} (size={Size})";
}
