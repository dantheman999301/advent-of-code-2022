using AdventOfCode.Shared.Days.Two;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class DayTwoTests
{
    [Theory]
    [MemberData(nameof(ScoreDataPartOne))]
    public async Task GivenAPlay_WhenPlayedPartOne_ScoreIsCorrect(string play, int expectedScore)
    {
        var dayTwo = new DayTwo();
        await using var playStream = play.ToStream();
        var score = await dayTwo.RunPartOne(playStream);
        score.Should().BeSuccess().And.HaveValue(expectedScore.ToString());
    }
    
    [Theory]
    [MemberData(nameof(ScoreDataPartOne))]
    public async Task GivenAPlay_WhenPlayedPartTwo_ScoreIsCorrect(string play, int expectedScore)
    {
        var dayTwo = new DayTwo();
        await using var playStream = play.ToStream();
        var score = await dayTwo.RunPartOne(playStream);
        score.Should().BeSuccess().And.HaveValue(expectedScore.ToString());
    }
    
    public static IEnumerable<object[]> ScoreDataPartOne =>
        new List<object[]>
        {
            // Rock
            new object[] { "A X", 1 + 3 }, // Rock
            new object[] { "A Y", 2 + 6 }, // Paper 
            new object[] { "A Z", 3 + 0 }, // Scissors
            
            // Paper
            new object[] { "B X", 1 + 0 }, // Rock
            new object[] { "B Y", 2 + 3 }, // Paper
            new object[] { "B Z", 3 + 6 }, // Scissors
            
            // Scissors
            new object[] { "C X", 1 + 6 }, // Rock
            new object[] { "C Y", 2 + 0 }, // Paper
            new object[] { "C Z", 3 + 3 }, // Scissors
        };
    
    public static IEnumerable<object[]> ScoreDataPartTwo =>
        new List<object[]>
        {
            // Rock
            new object[] { "A X", 0 + 2 }, // Loss (Paper)
            new object[] { "A Y", 3 + 1 }, // Draw (Rock)
            new object[] { "A Z", 6 + 6 }, // Win (Scissors)
            
            // Paper
            new object[] { "B X", 0 + 3 }, // Loss (Scissors)
            new object[] { "B Y", 3 + 2 }, // Draw (Paper)
            new object[] { "B Z", 6 + 1 }, // Win (Rock)
            
            // Scissors
            new object[] { "C X", 0 + 2 }, // Loss (Paper)
            new object[] { "C Y", 3 + 3 }, // Draw (Scissors)
            new object[] { "C Z", 6 + 1 }, // Win (Rock)
        };
}
