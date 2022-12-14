using System.Text.Json.Nodes;

namespace AdventOfCode.Challenges.Days.Year2022.Day13;

public record ArrayPacket(IList<Packet> Packets) : Packet
{
    public static ArrayPacket Parse(JsonArray jsonArray)
    {
        var outer = new List<Packet>();
        foreach (var node in jsonArray)
        {
            if (node is JsonArray nodeArr)
            {
                var inner = ArrayPacket.Parse(nodeArr);
                outer.Add(inner);
            }
            else
            {
                outer.Add(NumberPacket.Parse(node!));
            }
        }

        return new ArrayPacket(outer);
    }

    public Packet this[int index] => Packets[index];

    public int Length => Packets.Count;

    public override string ToString() => $"[ {string.Join(", ", Packets)} ]";
};
