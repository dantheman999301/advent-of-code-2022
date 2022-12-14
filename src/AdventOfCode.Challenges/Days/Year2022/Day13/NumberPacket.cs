using System.Text.Json.Nodes;

namespace AdventOfCode.Challenges.Days.Year2022.Day13;

public record NumberPacket(int Value) : Packet, IComparable<NumberPacket>
{
    public static NumberPacket Parse(JsonNode node) => new(node.GetValue<int>());

    public override string ToString() => Value.ToString();

    public int CompareTo(NumberPacket? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Value.CompareTo(other.Value);
    }
};
