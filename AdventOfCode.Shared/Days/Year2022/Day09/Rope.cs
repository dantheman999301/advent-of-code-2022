namespace AdventOfCode.Shared.Days.Year2022.Day09;

public class Rope
{
    public Point[] RopePositions { get; }
    
    public Rope(int length)
    {
        RopePositions = Enumerable.Range(0, length).Select(_ => new Point(0, 0)).ToArray();
    }

    public ISet<Point> Move(Instruction instruction)
    {
        HashSet<Point> tailPointsVisited = new();
        for (var i = 0; i < instruction.Amount; i++)
        {
            // Move head
            var headPoint = RopePositions[0];
            headPoint = instruction switch
            {
                { Direction: Direction.Up } => headPoint with { Y = headPoint.Y + 1 },
                { Direction: Direction.Down } => headPoint with { Y = headPoint.Y - 1},
                { Direction: Direction.Left } => headPoint with { X = headPoint.X - 1 },
                { Direction: Direction.Right } => headPoint with { X = headPoint.X + 1},
                _ => throw new ArgumentOutOfRangeException(nameof(instruction))
            };

            RopePositions[0] = headPoint;
            
            for (var j = 0; j < RopePositions.Length - 1; j++)
            {
                var movingPoint = RopePositions[j];
                var nextPoint = RopePositions[j + 1];
                
                nextPoint = MoveNext(movingPoint, nextPoint);
                
                RopePositions[j + 1] = nextPoint;
            }

            tailPointsVisited.Add(RopePositions[^1]);
        }

        return tailPointsVisited;
    }

    private static Point MoveNext(Point movingPoint, Point nextPoint)
    {
        if (movingPoint.IsTouching(nextPoint))
        {
            return nextPoint;
        }

        var diff = movingPoint.GetDifference(nextPoint);
        var newTailPoint = diff switch
        {
            { X: 0 } => nextPoint with { Y = nextPoint.Y + Math.Sign(diff.Y) },
            { Y: 0 } => nextPoint with { X = nextPoint.X + Math.Sign(diff.X) },
            _ => new Point(nextPoint.X + Math.Sign(diff.X), nextPoint.Y + Math.Sign(diff.Y))
        };

        return newTailPoint;
    }
}
