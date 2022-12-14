using AdventOfCode.Challenges.Days.Year2022.Day12;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day12Tests
{
    private const string Input = """
                                Sabqponm
                                abcryxxl
                                accszExk
                                acctuvwj
                                abdefghi
                                """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDay12PartOneIsRun_ThenResultIs31()
    {
        await using var stream = Input.ToStream();
        var result = await new Day12().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("31");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDay12PartTwoIsRun_ThenResultIs29()
    {
        await using var stream = Input.ToStream();
        var result = await new Day12().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("29");
    }
}
