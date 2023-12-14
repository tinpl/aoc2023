﻿using aoc.Days;

namespace aoc.Tests
{
  public class Day4Tests
  {
    [Theory]
    [InlineData("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8)]
    [InlineData("Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2)]
    [InlineData("Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1", 2)]
    [InlineData("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83", 1)]
    [InlineData("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36", 0)]
    [InlineData("Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", 0)]
    public Task Part1(string line, int expected)
    {
      Day4.GetLinePoints(line).Should().Be(expected);

      return Task.CompletedTask;
    }

    [Fact]
    public Task Part2()
    {
      var input = @"
Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
";
      Day4.Part2(input).Should().Be(30);

      return Task.CompletedTask;
    }
  }
}
