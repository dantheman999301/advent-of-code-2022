using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Challenges.Extensions;

public static class StringExtensions
{
    // ReSharper disable once ReturnTypeCanBeEnumerable.Global
    public static IList<string> SplitBySpace(this string str)
    {
        return str.Split(' ');
    }

    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);
}
