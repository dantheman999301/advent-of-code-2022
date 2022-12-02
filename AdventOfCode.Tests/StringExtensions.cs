using System.Text;

namespace AdventOfCode.Tests;

public static class StringExtensions
{
    public static Stream ToStream(this string str) => new MemoryStream(Encoding.UTF8.GetBytes(str));
}
