using AdventOfCode.Shared.Days.Eight;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class DayEightTests
{
    private const string Input = """
                                30373
                                25512
                                65332
                                33549
                                35390
                                """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDayEightPartOneIsRun_ThenResultIs21()
    {
        await using var stream = Input.ToStream();
        var result = await new DayEight().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("21");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayEightPartTwoIsRun_ThenResultIs8()
    {
        await using var stream = Input.ToStream();
        var result = await new DayEight().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("8");
    }
}
