using System.Drawing;

namespace aoc.Days;

public class Day4
{
  private readonly string _inputFilePath = "Input/Day4.txt";

  public async Task<(int, int)> Run()
  {
    var input = await File.ReadAllTextAsync(_inputFilePath);
    int part1 = Part1(input);
    int part2 = Part2(input);

    return (part1, part2);
  }

  public static int Part1(string input)
  {
    var lines = input.Split(Environment.NewLine,
      StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    return lines.Select(GetLinePoints).Sum();
  }

  public static int GetLinePoints(string line) {
    var card = Card.FromLine(line);

    var intersection = card.Numbers.Intersect(card.WinningNumbers);
    
    int points = 0;
    foreach (var num in intersection) {
      if (points == 0)
        points = 1;
      else 
        points *= 2;
    }

    return points;
  }
 

  public static int Part2(string input)
  {
    var lines = input.Split(Environment.NewLine,
      StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var cards = lines.Select(Card.FromLine);

    Dictionary<int,int> cardsCounts = cards.ToDictionary(x => x.Id, _ => 1);
    foreach (var card in cards) {
      var intersectionCount = card.Numbers.Intersect(card.WinningNumbers).Count();
      for (int i = 1; i <= intersectionCount; ++i) {
        cardsCounts[card.Id + i] += cardsCounts[card.Id];
      }
    }

    return cardsCounts.Values.Sum();;
  }

  class Card {
    public int Id { get; set; }
    public HashSet<int> WinningNumbers { get; set; } = new();
    public HashSet<int> Numbers { get; set; } = new();

    public static Card FromLine(string line) {
      var ret = new Card();
      var tokens = line.Split(new char[] {':', '|'}, 
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

      ret.Id = Convert.ToInt32(tokens[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

      foreach (string num in tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
        ret.WinningNumbers.Add(Convert.ToInt32(num));
      foreach (string num in tokens[2].Split(' ', StringSplitOptions.RemoveEmptyEntries))
        ret.Numbers.Add(Convert.ToInt32(num));

      return ret;
    }
  }

  class ProcessedCard {
    public int Id { get; set; }

  }

}