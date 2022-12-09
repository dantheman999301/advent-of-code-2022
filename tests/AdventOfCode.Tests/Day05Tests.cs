using AdventOfCode.Challenges.Days.Year2022.Day05;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day05Tests
{
    private const string Input = """
                                    [D]    
                                [N] [C]    
                                [Z] [M] [P]
                                 1   2   3 

                                move 1 from 2 to 1
                                move 3 from 1 to 3
                                move 2 from 2 to 1
                                move 1 from 1 to 2
                                """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDayFivePartOneIsRun_ThenResultIsCMZ()
    {
        await using var stream = Input.ToStream();
        var result = await new Day05().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("CMZ");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayFivePartTwoIsRun_ThenResultIsMCD()
    {
        await using var stream = Input.ToStream();
        var result = await new Day05().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("MCD");
    }
}
