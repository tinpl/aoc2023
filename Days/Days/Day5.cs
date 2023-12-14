namespace aoc.Days;

public class Day5
{
  private readonly string _inputFilePath = "Input/Day5.txt";

  public async Task<(long, long)> Run()
  {
    var input = await File.ReadAllTextAsync(_inputFilePath);
    long part1 = Part1(input);
    long part2 = Part2(input);

    return (part1, part2);
  }

  public static long Part1(string input)
  {
    var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    Input inp = ParseInputPart1(lines);
    return FindSeedsLocations(inp).Min();
  }


  public static long Part2(string input)
  {
    var lines = input.Split(Environment.NewLine,
      StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    Input inp = ParseInputPart2(lines);
    return FindSeedsLocations(inp).Min();
  }

  static Input ParseInputPart1(string[] lines)
  {
    Input ret = new();

    MapsSegment? currentMaps = null;
    for (int line_idx = 0; line_idx < lines.Length; ++line_idx)
    {
      var line = lines[line_idx];
      if (line.Contains("seeds:"))
      {
        ret.Seeds = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .Select(x => new Seed { Start = Convert.ToInt64(x), Range = 1 })
          .ToList();
        continue;
      }

      if (line.Contains("seed-to-soil map:"))
      {
        currentMaps = ret.SeedToSoil;
        continue;
      }

      if (line.Contains("soil-to-fertilizer map:"))
      {
        currentMaps = ret.SoilToFertilizer;
        continue;
      }

      if (line.Contains("fertilizer-to-water map:"))
      {
        currentMaps = ret.FertilizerToWater;
        continue;
      }

      if (line.Contains("water-to-light map:"))
      {
        currentMaps = ret.WaterToLight;
        continue;
      }

      if (line.Contains("light-to-temperature map:"))
      {
        currentMaps = ret.LightToTemperature;
        continue;
      }

      if (line.Contains("temperature-to-humidity map:"))
      {
        currentMaps = ret.TemperatureToHumidity;
        continue;
      }

      if (line.Contains("humidity-to-location map:"))
      {
        currentMaps = ret.HumidityToLocation;
        continue;
      }

      var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length != 3)
        throw new Exception("Tokens length is not 3");

      currentMaps?.Maps.Add(new MapsSegment.Map
      {
        Destination = Convert.ToInt64(tokens[0]),
        Source = Convert.ToInt64(tokens[1]),
        Range = Convert.ToInt64(tokens[2]),
      });
    }

    return ret;
  }

  static Input ParseInputPart2(string[] lines)
  {
    Input ret = new();

    MapsSegment? currentMaps = null;
    foreach (var line in lines)
    {
      if (line.Contains("seeds:"))
      {
        var values = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .Select(x => Convert.ToInt64(x)).ToList();
        for (int value_idx = 0; value_idx < values.Count; ++value_idx) {
          if (value_idx % 2 == 0)
            ret.Seeds.Add(new Seed { Start = Convert.ToInt64(values[value_idx]), Range = 1});
          else 
            ret.Seeds.Last().Range = Convert.ToInt64(values[value_idx]);
        }
        
        continue;
      }

      if (line.Contains("seed-to-soil map:"))
      {
        currentMaps = ret.SeedToSoil;
        continue;
      }

      if (line.Contains("soil-to-fertilizer map:"))
      {
        currentMaps = ret.SoilToFertilizer;
        continue;
      }

      if (line.Contains("fertilizer-to-water map:"))
      {
        currentMaps = ret.FertilizerToWater;
        continue;
      }

      if (line.Contains("water-to-light map:"))
      {
        currentMaps = ret.WaterToLight;
        continue;
      }

      if (line.Contains("light-to-temperature map:"))
      {
        currentMaps = ret.LightToTemperature;
        continue;
      }

      if (line.Contains("temperature-to-humidity map:"))
      {
        currentMaps = ret.TemperatureToHumidity;
        continue;
      }

      if (line.Contains("humidity-to-location map:"))
      {
        currentMaps = ret.HumidityToLocation;
        continue;
      }

      var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length != 3)
        throw new Exception("Tokens length is not 3");

      currentMaps?.Maps.Add(new MapsSegment.Map
      {
        Destination = Convert.ToInt64(tokens[0]),
        Source = Convert.ToInt64(tokens[1]),
        Range = Convert.ToInt64(tokens[2]),
      });
    }

    return ret;
  }

  static IEnumerable<long> FindSeedsLocations(Input inp)
  {
    var mapsSegments = new List<MapsSegment>
    {
      inp.SeedToSoil,
      inp.SoilToFertilizer,
      inp.FertilizerToWater,
      inp.WaterToLight,
      inp.LightToTemperature,
      inp.TemperatureToHumidity,
      inp.HumidityToLocation
    };

    return inp.Seeds.Select(seed =>
    {
      object lck = new object();

      long minLocation = long.MaxValue;

      Parallel.For(seed.Start, seed.Start + seed.Range, seedNumber =>
      {
        // brutforce
        long value = seedNumber;
        foreach (MapsSegment mapSegment in mapsSegments)
        {
          // if value in map, change
          foreach (var map in mapSegment.Maps)
          {
            if (value >= map.Source && value < map.Source + map.Range)
            {
              value = value - map.Source + map.Destination;
              break;
            }
          }
        }

        lock (lck)
        {
          if (value < minLocation)
            minLocation = value;
        }
      });

      return minLocation;
    });
  }

  class Input
  {
    public List<Seed> Seeds { get; set; } = new List<Seed>();
    public readonly MapsSegment SeedToSoil = new MapsSegment("seed", "soil");
    public readonly MapsSegment SoilToFertilizer = new MapsSegment("soil", "fertilizer");
    public readonly MapsSegment FertilizerToWater = new MapsSegment("fertilizer", "water");
    public readonly MapsSegment WaterToLight = new MapsSegment("water", "light");
    public readonly MapsSegment LightToTemperature = new MapsSegment("light", "temperature");
    public readonly MapsSegment TemperatureToHumidity = new MapsSegment("temperature", "humidity");
    public readonly MapsSegment HumidityToLocation = new MapsSegment("humidity", "location");
  }

  class Seed
  {
    public long Start { get; set; }
    public long Range { get; set; }
  }

  class MapsSegment
  {
    public string Source { get; init; }
    public string Destination { get; init; }
    public List<Map> Maps { get; set; } = new List<Map>();

    public MapsSegment(string source, string destination)
    {
      Source = source;
      Destination = destination;
    }

    public struct Map
    {
      public long Source { get; set; }
      public long Destination { get; set; }
      public long Range { get; set; }
    }
  }
}