namespace AdventOfCode.Shared.Days.Four;

public static class RangeExtensions
{
    public static bool IsSubsetOf(this Range a, Range b) =>
        a.Start.Value <= b.Start.Value && a.End.Value >= b.End.Value;

    public static bool Overlaps(this Range a, Range b) =>
        a.Start.Value <= b.End.Value && b.Start.Value <= a.End.Value;
}
