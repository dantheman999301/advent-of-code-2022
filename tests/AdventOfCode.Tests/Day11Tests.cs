using AdventOfCode.Challenges.Days.Year2022.Day09;
using AdventOfCode.Challenges.Days.Year2022.Day11;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day11Tests
{
    private const string Input = """
                                Monkey 0:
                                  Starting items: 79, 98
                                  Operation: new = old * 19
                                  Test: divisible by 23
                                    If true: throw to monkey 2
                                    If false: throw to monkey 3

                                Monkey 1:
                                  Starting items: 54, 65, 75, 74
                                  Operation: new = old + 6
                                  Test: divisible by 19
                                    If true: throw to monkey 2
                                    If false: throw to monkey 0

                                Monkey 2:
                                  Starting items: 79, 60, 97
                                  Operation: new = old * old
                                  Test: divisible by 13
                                    If true: throw to monkey 1
                                    If false: throw to monkey 3

                                Monkey 3:
                                  Starting items: 74
                                  Operation: new = old + 3
                                  Test: divisible by 17
                                    If true: throw to monkey 0
                                    If false: throw to monkey 1
                                """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDay11PartOneIsRun_ThenResultIs10605()
    {
        await using var stream = Input.ToStream();
        var result = await new Day11().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("10605");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDay11PartTwoIsRun_ThenResultIs2713310158()
    {
        await using var stream = Input.ToStream();
        var result = await new Day11().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("2713310158");
    }
}
