namespace AdventOfCode.Challenges.Days.Year2022.Day09;

public record Point(int X, int Y)
{
    public bool IsTouching(Point b) => Math.Abs(X - b.X) <= 1 && Math.Abs(Y - b.Y) <= 1;
    public Point GetDifference(Point b) => new (X - b.X, Y - b.Y);
}
