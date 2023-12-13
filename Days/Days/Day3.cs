using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace aoc.Days;

public class Day3
{
  private readonly string _inputFilePath = "Input/Day3.txt";

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

    var symbols = FindSymbols(lines);
    var numbers = GetNumbers(FindAdjacentDigits(symbols, lines), lines);

    return numbers.Sum(number => number.Value.Number);
  }

  public static int Part2(string input)
  {
    var lines = input.Split(Environment.NewLine,
      StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var symbols = FindSymbols(lines);
    var numbers = GetNumbers(FindAdjacentDigits(symbols, lines), lines);

    var symbolsToNumbers = InvertNumbersBySymbols(numbers);
    var gears = FindGears(symbolsToNumbers);

    return gears.Sum(x => x.Value.First().Number * x.Value.Last().Number);
  }

  static List<SchematicSymbol> FindSymbols(string[] lines)
  {
    var ret = new List<SchematicSymbol>();

    for (int row_idx = 0; row_idx < lines.Length; ++row_idx)
    {
      var line = lines[row_idx].ToCharArray();
      for (int col_idx = 0; col_idx < line.Length; ++col_idx)
      {
        if (line[col_idx] != '.' && !IsNum(line[col_idx]))
        {
          ret.Add(new SchematicSymbol
            { Value = line[col_idx], Point = new Point(col_idx, row_idx) });
        }
      }
    }

    return ret;
  }

  static bool IsNum(char chr)
  {
    return chr >= '0' && chr <= '9';
  }

  static Dictionary<Point, SchematicSymbol> FindAdjacentDigits(
    List<SchematicSymbol> symbols,
    string[] lines)
  {
    var ret = new Dictionary<Point, SchematicSymbol>();

    bool AlignedToSymbol(int X, int Y, SchematicSymbol symbol, string[] lines,
      Dictionary<Point, SchematicSymbol> ret)
    {
      if (IsNum(lines[Y][X]))
      {
        ret[new Point { X = X, Y = Y }] = symbol;
        return true;
      }

      return false;
    }

    foreach (var symbol in symbols)
    {
      AlignedToSymbol(symbol.Point.X - 1, symbol.Point.Y - 1, symbol, lines,
        ret);
      AlignedToSymbol(symbol.Point.X - 1, symbol.Point.Y, symbol, lines, ret);
      AlignedToSymbol(symbol.Point.X - 1, symbol.Point.Y + 1, symbol, lines,
        ret);
      AlignedToSymbol(symbol.Point.X, symbol.Point.Y - 1, symbol, lines, ret);
      AlignedToSymbol(symbol.Point.X, symbol.Point.Y + 1, symbol, lines, ret);
      AlignedToSymbol(symbol.Point.X + 1, symbol.Point.Y - 1, symbol, lines,
        ret);
      AlignedToSymbol(symbol.Point.X + 1, symbol.Point.Y, symbol, lines, ret);
      AlignedToSymbol(symbol.Point.X + 1, symbol.Point.Y + 1, symbol, lines,
        ret);
    }

    return ret;
  }

  static Dictionary<Point, SchematicNumber> GetNumbers(
    Dictionary<Point, SchematicSymbol> adjacentDigits, string[] lines)
  {
    Dictionary<Point, SchematicNumber> ret =
      new Dictionary<Point, SchematicNumber>();

    foreach (var digit in adjacentDigits)
    {
      var number = GetNumber(digit.Key, digit.Value, lines);

      if (ret.TryGetValue(number.StartPoint, out var value))
        value.EmitedBySymbols.Add(digit.Value);
      else
        ret[number.StartPoint] = number;
    }

    return ret;
  }

  static SchematicNumber GetNumber(Point digit, SchematicSymbol bySymbol,
    string[] lines)
  {
    SchematicNumber ret = new SchematicNumber();

    int leftX = digit.X;
    int rightX = digit.X;

    while (leftX >= 0 && IsNum(lines[digit.Y][leftX]))
      leftX--;
    while (rightX <= lines[digit.Y].Length - 1 && IsNum(lines[digit.Y][rightX]))
      rightX++;

    leftX++;
    rightX--;

    ret.StartPoint = new Point { X = leftX, Y = digit.Y };
    ret.Width = rightX - leftX + 1;

    int number = 0;
    int x = leftX;
    while (x <= rightX)
    {
      number *= 10;
      number += Convert.ToInt32(lines[digit.Y][x] - '0');
      ++x;
    }

    ret.Number = number;
    ret.EmitedBySymbols.Add(bySymbol);
    return ret;
  }

  static Dictionary<SchematicSymbol, List<SchematicNumber>>
    InvertNumbersBySymbols(Dictionary<Point, SchematicNumber> numbers)
  {
    var ret = new Dictionary<SchematicSymbol, List<SchematicNumber>>();

    foreach (var number in numbers.Values)
    {
      foreach (var symbol in number.EmitedBySymbols)
      {
        if (ret.TryGetValue(symbol, out var value))
          value.Add(number);
        else
        {
          ret[symbol] = new List<SchematicNumber>
          {
            number
          };
        }
      }
    }

    return ret;
  }

  static IEnumerable<KeyValuePair<SchematicSymbol, List<SchematicNumber>>>
    FindGears(
      Dictionary<SchematicSymbol, List<SchematicNumber>> symbolsToNumbers)
  {
    return symbolsToNumbers.Where(x =>
      x.Key.Value == '*' && x.Value.Count == 2);
  }


  class SchematicNumber
  {
    public int Number { get; set; }
    public Point StartPoint { get; set; }
    public int Width { get; set; }

    public HashSet<SchematicSymbol> EmitedBySymbols { get; set; } = new();
  }


  class SchematicSymbol
  {
    public char Value { get; set; }
    public Point Point { get; set; }
  }
}