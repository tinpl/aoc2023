using aoc.Days;

namespace aoc.Tests
{
  public class Day6Tests
  {
    [Fact]
    public Task Part1()
    {
      var input = @"
Time:      7  15   30
Distance:  9  40  200";

      Day6.Part1(input).Should().Be(288);

      return Task.CompletedTask;
    }

    [Fact]
    public Task Part2()
    {
      var input = @"
Time:      71530
Distance:  940200";

      Day6.Part2(input).Should().Be(71503);

      return Task.CompletedTask;
    }
  }
}
