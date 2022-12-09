using FluentResults;

namespace AdventOfCode.Shared.Days.Year2022.Day08;

public class Day08 : IDay
{
    public int DayNumber => 8;
    
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        
        var grid = await GenerateGrid(streamReader);

        var count = grid.SelectMany(row => row).Sum(tree => tree.IsVisible ? 1 : 0);

        return Result.Ok(count.ToString());
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        
        var grid = await GenerateGrid(streamReader);

        var bestScenicScore = grid.SelectMany(row => row).Max(tree => tree.ScenicScore);

        return Result.Ok(bestScenicScore.ToString());
    }

    private static async Task<ForestTree[][]> GenerateGrid(StreamReader streamReader)
    {
        var lines = new List<string>();
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;
            
            lines.Add(line);
        }

        var trees = new ForestTree[lines[0].Length][];

        for (var row = 0; row < lines.Count; row++)
        {
            var line = lines[row];
            trees[row] = new ForestTree[line.Length];
            
            for (var column = 0; column < line.Length; column++)
            {
                var @char = line[column];
                trees[row][column] = new ForestTree((int)char.GetNumericValue(@char));
            }
        }
        
        for (var row = 0; row < trees.Length; row++)
        {
            var treeRow = trees[row];
            for (var column = 0; column < treeRow.Length; column++)
            {
                var tree = treeRow[column];
                if (row > 0)
                {
                    tree.Top = trees[row - 1][column];
                }
                
                if (column > 0)
                {
                    tree.Left = trees[row][column - 1];
                }
                
                if (row < trees.Length - 1)
                {
                    tree.Bottom = trees[row + 1][column];
                }
                
                if (column < treeRow.Length - 1)
                {
                    tree.Right = trees[row][column + 1];
                }
            }
        }

        return trees;
    }
}
