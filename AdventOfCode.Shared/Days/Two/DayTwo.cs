using FluentResults;

namespace AdventOfCode.Shared.Days.Two;

public class DayTwo : IDay
{
    public int DayNumber => 2;

    private const int LossScore = 0;
    private const int DrawScore = 3;
    private const int WinScore = 6;
    
    private static class Play
    {
        public const int Rock = 1;
        public const int Paper = 2;
        public const int Scissors = 3;
    }
    
    private static readonly Dictionary<char, int> CharToPlayMap = new()
    {
        { 'A', Play.Rock },
        { 'X', Play.Rock },
        { 'B', Play.Paper },
        { 'Y', Play.Paper },
        { 'C', Play.Scissors },
        { 'Z', Play.Scissors }
    };

    private static readonly Dictionary<int, int> WinningPlayMap = new()
    {
        { Play.Rock, Play.Paper },
        { Play.Paper, Play.Scissors },
        { Play.Scissors, Play.Rock },
    };

    private static readonly Dictionary<int, int> LossPlayMap = WinningPlayMap.ToDictionary(k => k.Value, k => k.Key);
    
    private static readonly Dictionary<int, int> ResultMap = new()
    {
        { 'X', LossScore },
        { 'Y', DrawScore },
        { 'Z', WinScore },
    };

    public async Task<Result<string>> RunPartOne(Stream input)
    {
        return await CalculateTotals(input, false);
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        return await CalculateTotals(input, true);
    }

    private async Task<string> CalculateTotals(Stream input, bool calculatePlay)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);
        var runningScore = 0;
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;
            var chars = line.ToCharArray();

            var opponentPlay = CharToPlayMap[chars[0]];
            var ourPlay = calculatePlay 
                ? CalculatePlay(opponentPlay, chars[2]) 
                : CharToPlayMap[chars[2]];

            runningScore += CalculateScore(opponentPlay, ourPlay);
        }

        return runningScore.ToString();
    }

    private static int CalculatePlay(int opponentPlay, char c)
    {
        var expectedResult = ResultMap[c];
        return expectedResult switch
        {
            WinScore => WinningPlayMap[opponentPlay],
            DrawScore => opponentPlay,
            LossScore => LossPlayMap[opponentPlay],
            _ => throw new ArgumentOutOfRangeException(nameof(c))
        };
    }

    private static int CalculateScore(int opponentPlay, int ourPlay)
    {
        var score = ourPlay;

        if (opponentPlay == ourPlay)
        {
            score += DrawScore;
            return score;
        }
        
        score += (opponentPlay, ourPlay) switch
        {
            (Play.Rock, Play.Paper) => WinScore,
            (Play.Paper, Play.Scissors) => WinScore,
            (Play.Scissors, Play.Rock) => WinScore,
            _ => 0 // Should never get here
        };

        return score;
    }
}
