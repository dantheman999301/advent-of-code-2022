using AdventOfCode.Shared.Days.Three;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class DayThreeTests
{
    private const string Input = """
            vJrwpWtwJgWrhcsFMMfFFhFp
            jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
            PmmdzqPrVvPwwTWBwg
            wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
            ttgJtRGJQctTZtZT
            CrZsJsPPZsGzwwsLwLmpwMDw
            """;
    [Fact]
    public async Task GivenAnExampleInput_WhenDayThreePartOneIsRun_ThenSumOfPrioritiesIsCorrect()
    {
        await using var stream = Input.ToStream();
        var result = await new DayThree().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("157");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayThreePartTwoIsRun_ThenSumOfPrioritiesIsCorrect()
    {
        await using var stream = Input.ToStream();
        var result = await new DayThree().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("70");
    }

}
