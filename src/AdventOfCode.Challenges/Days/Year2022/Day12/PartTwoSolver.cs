namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public record PartTwoSolver(Grid Grid) : PartOneSolver(Grid)
{
    protected override IEnumerable<Cell> FindNeighbors(Cell from, HashSet<(int, int)> visited)
    {
        return Grid.FindNeighborsDown(from).Where(n => !visited.Contains(n.ToTuple())).ToHashSet();
    }
}
