namespace aoc.Days;

public class Day6
{
  private readonly string _inputFilePath = "Input/Day6.txt";

  public async Task<(long, long)> Run()
  {
    var input = await File.ReadAllTextAsync(_inputFilePath);
    long part1 = Part1(input);
    long part2 = Part2(input);

    return (part1, part2);
  }

  public static long Part1(string input)
  {
    var lines = input.Split(Environment.NewLine,
      StringSplitOptions.RemoveEmptyEntries);
    var times = lines.First(x => x.Contains("Time:"))
      .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(x => Convert.ToInt64(x))
      .ToList(); 
    var distances = lines.First(x => x.Contains("Distance:"))
      .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(x => Convert.ToInt64(x))
      .ToList();

    var races = Enumerable.Range(0, times.Count)
      .Select(idx =>
        new Race { Time = times[idx], Distance = distances[idx] }
      );

    return races.Select(GetNumberOfWays).Aggregate((lhs, rhs) => lhs * rhs);
  }

  static long GetNumberOfWays(Race race)
  {
    var ways = new List<(long, long)>();
    for (long holdTime = 0; holdTime < race.Time; ++holdTime)
    {
      long speed = holdTime;
      var distance = speed * (race.Time - holdTime);

      if (distance > race.Distance)
        ways.Add((holdTime, distance));
    }

    return ways.Count;
  }


  public static long Part2(string input)
  {
    var lines = input.Split(Environment.NewLine,
      StringSplitOptions.RemoveEmptyEntries);
    var times = lines.First(x => x.Contains("Time:"))
      .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(x => Convert.ToInt64(x))
      .ToList();
    var distances = lines.First(x => x.Contains("Distance:"))
      .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(x => Convert.ToInt64(x))
      .ToList();

    var races = Enumerable.Range(0, times.Count)
      .Select(idx =>
        new Race { Time = times[idx], Distance = distances[idx] }
      );

    return races.Select(GetNumberOfWays).Aggregate((lhs, rhs) => lhs * rhs);
  }

  class Race
  {
    public long Time { get; set; }
    public long Distance { get; set; }
  }
}