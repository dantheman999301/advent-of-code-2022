using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public record Solver(Grid Grid)
{
    public bool TrySolve([NotNullWhen(true)] out Cell? found)
    {
        HashSet<(int Row, int Col)> visited = new();
        Queue<Cell> toVisit = new();
        toVisit.Enqueue(Grid.Start);
        while (toVisit.Count > 0)
        {
            var end = Step(toVisit, visited);
            if (end == null) continue;

            found = end;
            return true;
        }

        found = null;
        return false;
    }

    private Cell? Step(Queue<Cell> toVisit, HashSet<(int, int)> visited)
    {
        var visiting = toVisit.Dequeue();
        if (Grid.End.Contains(visiting.ToTuple()))
        {
            return visiting;
        }

        visited.Add(visiting.ToTuple());

        var neighbors = FindNeighbors(visiting, visited);
        foreach (var n in neighbors.Where(n => !visited.Contains(n.ToTuple())))
        {
            visited.Add(n.ToTuple());
            toVisit.Enqueue(n);
        }

        return null;
    }

    protected virtual IEnumerable<Cell> FindNeighbors(Cell from, HashSet<(int, int)> visited)
    {
        return Grid.FindNeighborsUp(from).Where(n => !visited.Contains(n.ToTuple())).ToHashSet();
    }

}
