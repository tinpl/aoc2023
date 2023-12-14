using aoc.Days;

namespace aoc.Tests
{
  public class Day3Tests
  {
    [Fact]
    public Task Part1()
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

      return Task.CompletedTask;
    }

    [Fact]
    public Task Part2()
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

      return Task.CompletedTask;
    }
  }
}
