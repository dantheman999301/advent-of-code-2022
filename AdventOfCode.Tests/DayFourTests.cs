﻿using AdventOfCode.Shared.Days.Four;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class DayFourTests
{
    private const string Input = """
                                 2-4,6-8
                                 2-3,4-5
                                 5-7,7-9
                                 2-8,3-7
                                 6-6,4-6
                                 2-6,4-8
                                 """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDayFourPartOneIsRun_ThenResultIsTwo()
    {
        var result = await new DayFour().RunPartOne(Input.ToStream());
        result.Should().BeSuccess().And.HaveValue("2");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDayFourPartTwoIsRun_ThenResultIsFour()
    {
        var result = await new DayFour().RunPartTwo(Input.ToStream());
        result.Should().BeSuccess().And.HaveValue("4");
    }
}