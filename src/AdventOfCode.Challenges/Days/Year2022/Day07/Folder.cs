namespace AdventOfCode.Challenges.Days.Year2022.Day07;

public class Folder : ISystemEntry
{
    public string Name { get;  }

    public int Size => 0;
    
    public Folder(string name)
    {
        Name = name;
    }

    public override string ToString() => $"{Name} (dir)";
}