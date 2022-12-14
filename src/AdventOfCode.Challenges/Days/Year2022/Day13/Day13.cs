using System.Text.Json.Nodes;
using AdventOfCode.Challenges.Extensions;
using FluentResults;

namespace AdventOfCode.Challenges.Days.Year2022.Day13;

public class Day13 : IDay
{
    public int DayNumber => 13;
    
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        var packets = await ParseInput(input);

        var comparer = new PacketComparer();
        var i = 1;
        var result = 0;
        foreach (var compareNodes in packets.Chunk(2))
        {
            var compared = comparer.Compare(compareNodes[0], compareNodes[1]);
            if (compared <= 0)
            {
                result += i;
            }
            i++;
        }

        return Result.Ok(result.ToString());
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        var packets = await ParseInput(input);
        var comparer = new PacketComparer();

        var dividerPackets = DividerPackets;
        packets.AddRange(dividerPackets);

        packets.Sort(comparer);

        var decoderKey = dividerPackets
            .Select(dividerPacket => packets.IndexOf(dividerPacket) + 1)
            .Aggregate(1, (current, index) => current * index);

        return Result.Ok(decoderKey.ToString());
    }

    private static async Task<List<ArrayPacket>> ParseInput(Stream input)
    {
        var packets = new List<ArrayPacket>();
        using var streamReader = new StreamReader(input, leaveOpen: true);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (line.IsNullOrEmpty()) continue;

            var jsonNode = JsonNode.Parse(line)! as JsonArray;
            packets.Add(ArrayPacket.Parse(jsonNode!));
        }

        return packets;
    }

    private static IList<ArrayPacket> DividerPackets => new[]
    {
        new ArrayPacket(new List<Packet> { new ArrayPacket(new List<Packet> { new NumberPacket(2) }) }),
        new ArrayPacket(new List<Packet> { new ArrayPacket(new List<Packet> { new NumberPacket(6) }) }),
    };
}
