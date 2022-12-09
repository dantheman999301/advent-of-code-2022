using AdventOfCode.Shared.Days.Year2022.Day06;
using FluentResults.Extensions.FluentAssertions;

namespace AdventOfCode.Tests;

public class Day06Tests
{
    [Theory]
    [MemberData(nameof(TestDataPartOne))]
    public async Task GivenASignal_WhenSignalIsReadLookingFor4CharMarker_CorrectMarkerNumberIsReturned(string signal, int expectedMarker)
    {
        await using var input = signal.ToStream();
        var markerResult = await new Day06().RunPartOne(input);
        markerResult.Should().BeSuccess().And.HaveValue(expectedMarker.ToString());
    }
    
    [Theory]
    [MemberData(nameof(TestDataPartTwo))]
    public async Task GivenASignal_WhenSignalIsReadLookingFor14CharMarker_CorrectMarkerNumberIsReturned(string signal, int expectedMarker)
    {
        await using var input = signal.ToStream();
        var markerResult = await new Day06().RunPartTwo(input);
        markerResult.Should().BeSuccess().And.HaveValue(expectedMarker.ToString());
    }
    
    public static IEnumerable<object[]> TestDataPartOne =>
        new List<object[]>
        {
            new object[] { "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7 },
            new object[] { "bvwbjplbgvbhsrlpgdmjqwftvncz", 5 }, 
            new object[] { "nppdvjthqldpwncqszvftbrmjlhg", 6 },
            new object[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10 }, 
            new object[] { "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11 },
        };
    
    public static IEnumerable<object[]> TestDataPartTwo =>
        new List<object[]>
        {
            new object[] { "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19 },
            new object[] { "bvwbjplbgvbhsrlpgdmjqwftvncz", 23 }, 
            new object[] { "nppdvjthqldpwncqszvftbrmjlhg", 23 },
            new object[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29 }, 
            new object[] { "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26 },
        };
}
