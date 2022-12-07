using System.Text;

namespace AdventOfCode.Shared.Days.Seven;

public class TreeNode<T>
{
    public IReadOnlyList<TreeNode<T>> Children => _children;

    public Guid Id { get; }

    private int Depth { get; init; }
    
    private int Child { get; init; }
    
    private readonly List<TreeNode<T>> _children = new();
    
    public TreeNode<T>? Parent { get; private init; }

    public T Value { get; }

    public TreeNode(T value)
    {
        Id = Guid.NewGuid();
        Value = value;
    }
    
    public TreeNode<T> AddChildNode(T value)
    {
        var node = new TreeNode<T>(value)
        {
            Parent = this,
            Depth = Depth + 1,
            Child = _children.Count + 1
        };
        
        _children.Add(node);
        return node;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? "Empty";
    }

    public string PrettyPrint()
    {
        var builder = new StringBuilder();
        builder.Append(new string(' ', Depth));
        builder.Append($"- {Value}");
        foreach (var child in Children)
        {
            builder.AppendLine();
            builder.Append(child);
        }

        return builder.ToString();
    }
}

public interface ISystemEntry
{
    string Name { get; }
    
    int Size { get; }
}

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

public static class TreeNodeExtensions
{
    public static int GetSizeIncludingChildren(this TreeNode<ISystemEntry> node)
    {
        return node.Value switch
        {
            Folder => node.Children.Sum(child => child.GetSizeIncludingChildren()),
            File => node.Value.Size,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
