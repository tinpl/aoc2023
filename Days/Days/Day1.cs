namespace aoc.Days;

public class Day1
{
  private readonly string _inputFilePath = "Input/Day1.txt";

  public async Task<int> Run()
  {
    var input = await File.ReadAllTextAsync(_inputFilePath);
    var ret = input.Split(Environment.NewLine,
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
      .Select(ProcessLine)
      .Sum();

    return ret;
  }

  
  public static int ProcessLine(string line)
  {
    var chars = line.ToCharArray();

    int firstDigit = -1;
    for (int char_idx = 0; char_idx < chars.Length; ++char_idx)
    {
      int digit = GetDigit(chars, char_idx);
      if (digit >= 0)
      {
        firstDigit = digit;
        break;
      }
    }

    var secondDigit = -1;
    for (int char_idx = chars.Length - 1; char_idx >= 0; --char_idx)
    {
      int digit = GetDigit(chars, char_idx);
      if (digit >= 0)
      {
        secondDigit = digit; 
        break; 
      }
    }

    if (firstDigit < 0 || secondDigit < 0)
      return 0;

    return firstDigit*10 + secondDigit;
  }

  static readonly Dictionary<string, int> entries = new Dictionary<string, int>()
  {
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 },
    { "1", 1 },
    { "2", 2 },
    { "3", 3 },
    { "4", 4 },
    { "5", 5 },
    { "6", 6 },
    { "7", 7 },
    { "8", 8 },
    { "9", 9 },
    { "0", 0 }
  };

  static int GetDigit(char[] chars, int char_idx)
  {
    foreach (var entry in entries)
    {
      if (CheckEntry(chars, char_idx, entry.Key))
        return entry.Value;
    }

    return -1;
  }

  static bool CheckEntry(char[] chars, int char_idx, string entry)
  {
    var entryChars = entry.ToCharArray();
    for (int entryCharIdx = 0; entryCharIdx < entryChars.Length; entryCharIdx++)
    {
      var chidx = char_idx + entryCharIdx;
      if (chidx >= chars.Length) 
        return false;

      if (chars[chidx] != entryChars[entryCharIdx])
        return false;
    }
    
    return true;
  }
}

