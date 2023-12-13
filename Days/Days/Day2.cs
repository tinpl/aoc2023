namespace aoc.Days;

public class Day2
{
  private readonly string _inputFilePath = "Input/Day2.txt";

  public async Task<(int, int)> Run()
  {
    var input = await File.ReadAllTextAsync(_inputFilePath);

    var passConditions = new MaxItemsPassConditions(12, 13, 14);
    var sumGameIds = input.Split(Environment.NewLine,
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
      .Select(ProcessLine)
      .Where(x => passConditions.CheckData(x))
      .Sum(x => x.ID);

    var sumPowers = input.Split(Environment.NewLine,
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
      .Select(ProcessLine)
      .Select(SetsPower)
      .Sum();

    return (sumGameIds, sumPowers);
  }

  public static GameData ProcessLine(string line)
  {
    var ret = new GameData();

    var tokens = line.Split(':', ';');
    ret.ID = Convert.ToInt32(tokens[0].Split(' ')[1]);
    for (int idx = 1; idx < tokens.Length; idx++)
    {
      ret.Sets.Add(ParseGameSet(tokens[idx]));
    }

    return ret;
  }

  static GameData.GameSet ParseGameSet(string gameSetLine)
  {
    GameData.GameSet ret = new GameData.GameSet(0, 0, 0);

    var tokens = gameSetLine.Split(',');
    foreach (var token in tokens)
    {
      var ballTokens = token.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
      if (ballTokens[1] == "red")
        ret.R = Convert.ToInt32(ballTokens[0]);
      if (ballTokens[1] == "green")
        ret.G = Convert.ToInt32(ballTokens[0]);
      if (ballTokens[1] == "blue")
        ret.B = Convert.ToInt32(ballTokens[0]);
    }

    return ret;
  }

  public static int SetsPower(GameData gameData)
  {
    int maxR = gameData.Sets.Max(x => x.R);
    int maxG = gameData.Sets.Max(x => x.G);
    int maxB = gameData.Sets.Max(x => x.B);

    return maxR * maxG * maxB;
  }

  public class GameData
  {
    public class GameSet
    {
      public int R { get; set; }
      public int G { get; set; }
      public int B { get; set; }

      public GameSet(int r, int g, int b)
      {
        R = r;
        G = g;
        B = b;
      }
    }

    public int ID { get; set; }
    public List<GameSet> Sets { get; set; } = new List<GameSet>();
  }

  public interface IPassConditions
  {
    public bool CheckData(GameData gameData);
  }

  public class MaxItemsPassConditions : IPassConditions
  {
    public int MaxR { get; set; }
    public int MaxG { get; set; }
    public int MaxB { get; set; }

    public MaxItemsPassConditions(int r, int g, int b)
    {
      MaxR = r;
      MaxG = g;
      MaxB = b;
    }

    public bool CheckData(GameData gameData)
    {
      foreach (var gameDataSet in gameData.Sets)
      {
        if (gameDataSet.R > MaxR || gameDataSet.G > MaxG || gameDataSet.B > MaxB)
          return false;
      }

      return true;
    }
  }
}

