using aoc.Days;
using FluentAssertions;

namespace aoc.Tests
{
  public class Day3Tests
  {
    [Fact]
    public async Task Part1()
    {
      var input = @"
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

      Day3.Part1(input).Should().Be(4361);
    }

    [Fact]
    public async Task Part2()
    {
      var input = @"
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

      Day3.Part2(input).Should().Be(467835);
    }
  }
}
