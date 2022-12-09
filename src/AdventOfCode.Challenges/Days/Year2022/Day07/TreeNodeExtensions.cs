using AdventOfCode.Challenges.Common;

namespace AdventOfCode.Challenges.Days.Year2022.Day07;

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
