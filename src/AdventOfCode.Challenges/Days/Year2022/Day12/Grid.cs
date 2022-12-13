namespace AdventOfCode.Challenges.Days.Year2022.Day12;

public record Grid(int[,] HeightMap, Cell Start, HashSet<(int, int)> End)
{
    public Grid(int[,] heightMap, Cell start, Cell end) 
        : this(heightMap, start, new HashSet<(int, int)> { (end.Row, end.Column) })
    {
    }

    public void DisplayMap()
    {
        for (var row = 0; row < HeightMap.GetLength(0); row++)
        {
            for (var col = 0; col < HeightMap.GetLength(1); col++)
            {
                Console.Write($"{HeightMap[row, col], 3}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool IsInBounds(Cell p)
    {
        return p.Row >= 0 && 
               p.Row < HeightMap.GetLength(0) && 
               p.Column >= 0 && 
               p.Column < HeightMap.GetLength(1);
    }

    public static bool CheckHeights(int fromHeight, int toHeight)
    {
        return toHeight <= fromHeight + 1;
    }

    public IEnumerable<Cell> FindNeighborsUp(Cell current)
    {
        return current.GetNeighbours()
            .Where(IsInBounds)
            .Where(neighbour =>
                CheckHeights(HeightMap[current.Row, current.Column], HeightMap[neighbour.Row, neighbour.Column]))
            .ToHashSet();
    }

    public IEnumerable<Cell> FindNeighborsDown(Cell current)
    {
        var set = current.GetNeighbours()
            .Where(IsInBounds)
            .Where(neighbour =>
                CheckHeights(HeightMap[neighbour.Row, neighbour.Column], HeightMap[current.Row, current.Column]))
            .ToHashSet();

        return set;
    }

    public static Grid Parse(IList<string> lines, char startChar = 'S', char endChar = 'E')
    {
        var heightMap = new int[lines.Count, lines[0].Length];
        Cell start = null!;
        HashSet<(int, int)> end = new ();
        for (var row = 0; row < lines.Count; row++)
        {
            for (var col = 0; col < lines[0].Length; col++)
            {
                var ch = lines[row][col];
                start = ch == startChar ? new Cell(row, col, null) : start;
                if (ch == endChar)
                {
                    end.Add((row, col));
                }
                heightMap[row, col] = CharHeight(ch);
            }
        }
        return new Grid(heightMap, start, end);
    }

    private static int CharHeight(char ch)
    {
        return ch switch
        {
            'S' => 'a' - 'a',
            'E' => 'z' - 'a',
            _ => ch - 'a',
        };
    }
}
