namespace AdventOfCode.Challenges.Days.Year2022.Day13;

public class PacketComparer : IComparer<Packet>
{
    public int Compare(Packet? left, Packet? right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return ComparePacket(left, right);
    }

    private static int ComparePacket(Packet left, Packet right)
    {
        if (left is NumberPacket leftNum && right is NumberPacket rightNum)
        {
            return leftNum.CompareTo(rightNum);
        }

        var leftArr = left.AsArray();
        var rightArr = right.AsArray();
            
        for (var index = 0; index < leftArr.Length; index++)
        {
            if (index >= rightArr.Length)
            {
                return 1;
            }

            var leftVal = leftArr[index];
            var rightVal = rightArr[index];

            var compared = ComparePacket(leftVal, rightVal);
            if (compared != 0)
            {
                return compared;
            }
        }
        
        return leftArr.Packets.Count.CompareTo(rightArr.Packets.Count);
    }
}
