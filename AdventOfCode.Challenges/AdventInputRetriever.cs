using System.Reflection;
using FluentResults;

namespace AdventOfCode.Challenges;

public static class AdventInputRetriever
{
    public static Result<Stream> GetInputForDay(int day)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith($"Input.{day}.txt"));

        var stream = assembly.GetManifestResourceStream(resourceName);
        return stream is not null ? Result.Ok(stream) : Result.Fail("Could not find input to puzzle");
    }
}
