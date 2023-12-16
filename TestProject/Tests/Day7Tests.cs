using aoc.Days;

namespace aoc.Tests
{
  public class Day7Tests
  {
    [Fact]
    public Task Part1()
    {
      var input = @"
32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

      Day7.Part1(input).Should().Be(6440);

      return Task.CompletedTask;
    }

    [Fact]
    public Task Part2()
    {
      var input = @"
32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

      Day7.Part2(input).Should().Be(5905);

      return Task.CompletedTask;
    }
  }
}
