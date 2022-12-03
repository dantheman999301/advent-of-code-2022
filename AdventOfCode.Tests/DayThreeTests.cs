using AdventOfCode.Shared.Days.Three;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class DayThreeTests
{
    [Fact]
    public async Task GivenAnExampleInput_WhenDayThreePartOneIsRun_ThenSumOfPrioritiesIsCorrect()
    {
        const string input = """
            vJrwpWtwJgWrhcsFMMfFFhFp
            jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
            PmmdzqPrVvPwwTWBwg
            wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
            ttgJtRGJQctTZtZT
            CrZsJsPPZsGzwwsLwLmpwMDw
            """;

        var result = await new DayThree().RunPartOne(input.ToStream());
        result.Should().BeSuccess().And.HaveValue("157");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayThreePartTwoIsRun_ThenSumOfPrioritiesIsCorrect()
    {
        const string input = """
            vJrwpWtwJgWrhcsFMMfFFhFp
            jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
            PmmdzqPrVvPwwTWBwg
            wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
            ttgJtRGJQctTZtZT
            CrZsJsPPZsGzwwsLwLmpwMDw
            """;

        var result = await new DayThree().RunPartTwo(input.ToStream());
        result.Should().BeSuccess().And.HaveValue("70");
    }

}
