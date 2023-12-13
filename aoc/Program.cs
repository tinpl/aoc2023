// See https://aka.ms/new-console-template for more information

using aoc.Days;

namespace aoc
{
  class Program
  {
    public static async Task<int> Main(string[] args)
    {
      var day1 = await new Day1().Run();
      var day2 = await new Day2().Run();
      var day3 = await new Day3().Run();

      return 0;
    }
  }
}

