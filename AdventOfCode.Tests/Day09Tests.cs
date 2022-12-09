using AdventOfCode.Shared.Days.Year2022.Day09;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day09Tests
{
    private const string InputOne = """
                                R 4
                                U 4
                                L 3
                                D 1
                                R 4
                                D 1
                                L 5
                                R 2
                                """;
    
    private const string InputTwo = """
                                R 5
                                U 8
                                L 8
                                D 3
                                R 17
                                D 10
                                L 25
                                U 20
                                """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDay09PartOneIsRun_ThenResultIs13()
    {
        await using var stream = InputOne.ToStream();
        var result = await new Day09().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("13");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDay09PartTwoIsRun_ThenResultIs36()
    {
        await using var stream = InputTwo.ToStream();
        var result = await new Day09().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("36");
    }
}
