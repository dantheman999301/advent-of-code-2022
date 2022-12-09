using AdventOfCode.Challenges.Days.Year2022.Day07;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day07Tests
{
    private const string Input = """
                                 $ cd /
                                 $ ls
                                 dir a
                                 14848514 b.txt
                                 8504156 c.dat
                                 dir d
                                 $ cd a
                                 $ ls
                                 dir e
                                 29116 f
                                 2557 g
                                 62596 h.lst
                                 $ cd e
                                 $ ls
                                 584 i
                                 $ cd ..
                                 $ cd ..
                                 $ cd d
                                 $ ls
                                 4060174 j
                                 8033020 d.log
                                 5626152 d.ext
                                 7214296 k
                                 """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDaySevenPartOneIsRun_ThenResultIs95437()
    {
        await using var stream = Input.ToStream();
        var result = await new Day07().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("95437");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDaySevenPartTwoIsRun_ThenResultIs24933642()
    {
        await using var stream = Input.ToStream();
        var result = await new Day07().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("24933642");
    }
}
