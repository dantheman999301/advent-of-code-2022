using AdventOfCode.Shared.Days.Five;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class DayFiveTests
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
        var result = await new DayFive().RunPartOne(Input.ToStream());
        result.Should().BeSuccess().And.HaveValue("CMZ");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayFivePartOneIsRun_ThenResultIsMCD()
    {
        var result = await new DayFive().RunPartTwo(Input.ToStream());
        result.Should().BeSuccess().And.HaveValue("MCD");
    }
}
