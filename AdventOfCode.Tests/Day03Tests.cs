using AdventOfCode.Shared.Days.Year2022.Day03;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day03Tests
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
        var result = await new Day03().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("157");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayThreePartTwoIsRun_ThenSumOfPrioritiesIsCorrect()
    {
        await using var stream = Input.ToStream();
        var result = await new Day03().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("70");
    }

}
