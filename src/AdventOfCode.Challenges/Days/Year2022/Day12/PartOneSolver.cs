namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public record PartOneSolver(Grid Grid) : Solver(Grid)
{
    protected override IEnumerable<Cell> FindNeighbors(Cell from, HashSet<(int, int)> visited)
    {
        return Grid.FindNeighborsDown(from).Where(n => !visited.Contains(n.ToTuple())).ToHashSet();
    }
}
