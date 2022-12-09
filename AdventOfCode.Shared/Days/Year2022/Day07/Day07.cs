using FluentResults;

namespace AdventOfCode.Shared.Days.Year2022.Day07;

public class Day07 : IDay
{
    public int DayNumber => 7;
    
    private const string CommandPrefix = "$";
    private const string DirectoryPrefix = "dir";
    private const string ParentFolder = "..";

    private const int FileSystemTotalSize = 70000000;

    public async Task<Result<string>> RunPartOne(Stream input)
    {
        var root = await ParseInput(input);
        var foundFolders = FindFoldersWithMaxSize(root, 100000);

        return Result.Ok(foundFolders.ToString());
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        var root = await ParseInput(input);
        var minSizeFolderToDelete = FindMinimumSizeToDelete(root, 30000000);
        var smallestViableFolder = FindFolderToDelete(root, minSizeFolderToDelete, int.MaxValue);

        return Result.Ok(smallestViableFolder.ToString());
    }

    private static int FindMinimumSizeToDelete(TreeNode<ISystemEntry> root, int spaceRequired)
    {
        var size = root.GetSizeIncludingChildren();
        var currentFreeSpace = FileSystemTotalSize - size;
        return spaceRequired - currentFreeSpace;
    }

    private static async Task<TreeNode<ISystemEntry>> ParseInput(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        await streamReader.ReadLineAsync();

        var currentNode = new TreeNode<ISystemEntry>(new Folder("/"));

        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;
            
            if (line.StartsWith(CommandPrefix))
            {
                var currentCommand = ParseCommand(line);

                if (currentCommand.Type == CommandType.ChangeDirectory)
                {
                    currentNode = SwitchNode(currentCommand, currentNode);
                }
                
                continue;
            };

            TryPopulateNode(line, currentNode);
        }
        
        while (currentNode.Parent != null)
        {
            currentNode = currentNode.Parent;
        }

        return currentNode;
    }

    private static void TryPopulateNode(string line, TreeNode<ISystemEntry> currentNode)
    {
        var lineParts = line.Split(' ');
        ISystemEntry nodeToAdd = line.StartsWith(DirectoryPrefix)
            ? new Folder(lineParts[1])
            : new File(lineParts[1], int.Parse(lineParts[0]));
        
        currentNode.AddChildNode(nodeToAdd);
    }
    
    private static TreeNode<ISystemEntry> SwitchNode(Command currentCommand, TreeNode<ISystemEntry> currentNode)
    {
        var folderToChangeTo = currentCommand.Arguments;
        if (folderToChangeTo == ParentFolder)
        {
            currentNode = currentNode.Parent ?? throw new NullReferenceException("Parent should not be null");
        }
        else
        {
            currentNode = currentNode.Children.Single(c => c.Value.Name == folderToChangeTo);
        }

        return currentNode;
    }

    private static Command ParseCommand(string line)
    {
        var commandSplit = line.Split(' ');
        var command = commandSplit[1];
        return command switch
        {
            "cd" => new Command
            {
                Type = CommandType.ChangeDirectory,
                Arguments = commandSplit[2]
            },
            "ls" => new Command
            {
                Type = CommandType.ListFile
            },
            _ => throw new ArgumentOutOfRangeException(nameof(command))
        };
    }
    
    private static int FindFoldersWithMaxSize(TreeNode<ISystemEntry> currentNode, int maxSize)
    {
        if (currentNode.Value is File) return 0;
        
        var totalSize = 0;
        var size = currentNode.GetSizeIncludingChildren();
        totalSize += size > maxSize ? 0 : size;
        foreach (var child in currentNode.Children)
        {
            totalSize += FindFoldersWithMaxSize(child, maxSize);
        }

        return totalSize;
    }
    
    private static int FindFolderToDelete(TreeNode<ISystemEntry> currentNode,  int minSize, int currentBestSize)
    {
        if (currentNode.Value is File) return currentBestSize;
        
        var size = currentNode.GetSizeIncludingChildren();

        if (size <= minSize)
        {
            return currentBestSize;
        }

        if (size < currentBestSize)
        {
            currentBestSize = size;
        }

        return currentNode.Children.Aggregate(currentBestSize, (current, child) => FindFolderToDelete(child, minSize, current));
    }
}
