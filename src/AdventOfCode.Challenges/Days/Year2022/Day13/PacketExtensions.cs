namespace AdventOfCode.Challenges.Days.Year2022.Day13;

public static class PacketExtensions
{
    public static ArrayPacket AsArray(this Packet packet)
    {
        return packet switch
        {
            ArrayPacket arrayPacket => arrayPacket,
            NumberPacket numberPacket => new ArrayPacket(new List<Packet> { numberPacket }),
            _ => throw new ArgumentOutOfRangeException(nameof(packet))
        };
    }
}