using AdventOfCode.Challenges.Days.Year2022.Day13;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day13Tests
{
    
    private const string Input = """
                                [1,1,3,1,1]
                                [1,1,5,1,1]

                                [[1],[2,3,4]]
                                [[1],4]

                                [9]
                                [[8,7,6]]

                                [[4,4],4,4]
                                [[4,4],4,4,4]

                                [7,7,7,7]
                                [7,7,7]

                                []
                                [3]

                                [[[]]]
                                [[]]

                                [1,[2,[3,[4,[5,6,7]]]],8,9]
                                [1,[2,[3,[4,[5,6,0]]]],8,9]
                                """;

    [Fact]
    public async Task GivenAnExampleInput_WhenDay13PartOneIsRun_ThenResultIs13()
    {
        await using var stream = Input.ToStream();
        var result = await new Day13().RunPartOne(stream);
        result.Should().BeSuccess().And.HaveValue("13");
    }
    
    [Fact]
    public async Task GivenAnExampleInput_WhenDay13PartTwoIsRun_ThenResultIs140()
    {
        await using var stream = Input.ToStream();
        var result = await new Day13().RunPartTwo(stream);
        result.Should().BeSuccess().And.HaveValue("140");
    }
}
