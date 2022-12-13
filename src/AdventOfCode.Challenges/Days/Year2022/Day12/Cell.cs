namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public record Cell(int Row, int Column, Cell? From)
{
    public Cell North => this with { Row = Row - 1, From = this };
    public Cell South => this with { Row = Row + 1, From = this  };
    public Cell East => this with { Column = Column + 1, From = this  };
    public Cell West => this with { Column = Column - 1, From = this };
    
    public IEnumerable<Cell> GetNeighbours()
    {
        yield return North;
        yield return South;
        yield return East;
        yield return West;
    }
    
    public List<Cell> GetPath()
    {
        List<Cell> backTrack = new () { this };
        var current = this;
        while (current.From != null)
        {
            current = current.From;
            backTrack.Add(current);
        }
        backTrack.Reverse();
        return backTrack;
    }

    public int GetPathLength() => GetPath().Count - 1;

    public (int Row, int Column) ToTuple() => (Row, Column);

    public override string ToString()
    {
        return $"({Row}, {Column})";
    }
}
