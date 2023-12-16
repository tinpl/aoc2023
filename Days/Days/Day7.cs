namespace aoc.Days;

public class Day7
{
  private readonly string _inputFilePath = "Input/Day7.txt";

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
    var hands = ParseInput(lines);
    hands = hands.Select(x => { x.HandType = GetType(x.Cards); return x; });
    var sortedHands = hands.Order(new HandComparer()).ToList();

    int ret = 0;
    for (int handIdx = 0; handIdx < sortedHands.Count; ++handIdx) {
      var rank = handIdx + 1;
      ret += sortedHands[handIdx].Bid * rank;
    }

    return ret;
  }


  public static long Part2(string input)
  {
    var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    var hands = ParseInput(lines).ToList();

    foreach (var hand in hands)
    {
      hand.Cards = hand.Cards.Replace('J', 'X');
      hand.HandType = GetTypeX(hand.Cards);
    }

    var sortedHands = hands.Order(new HandComparer()).ToList();

    int ret = 0;
    for (int handIdx = 0; handIdx < sortedHands.Count; ++handIdx)
    {
      var rank = handIdx + 1;
      ret += sortedHands[handIdx].Bid * rank;
    }

    return ret;
  }

  static IEnumerable<Hand> ParseInput(string[] lines) {
    return lines.Select(line => {
      var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      var hand = GetHand(tokens[0], Convert.ToInt32(tokens[1]));
      return hand;
    });
  }

  static Hand GetHand(string hand, int bid) {
    var ret = new Hand();
    ret.Bid = bid;
    ret.Cards = hand;
    return ret;
  }

  static readonly List<char> AllCards = new List<char> { 
    'X', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'
    };

  class Hand {
    public string Cards { get; set; }
    public int Bid { get; set; }

    public HandType HandType { get; set; }
  }

  static HandType GetType(string hand) {
    var cardsCounts = new List<int>(AllCards.Select(_ => 0));

    foreach (var card in hand)
      cardsCounts[AllCards.IndexOf(card)]++;

    if (cardsCounts.Max() == 5)
      return HandType.FiveOfAKind;
    if (cardsCounts.Max() == 4)
      return HandType.FourOfAKind;
    if (cardsCounts.Max() == 3) {
      if (cardsCounts.Contains(2))
        return HandType.FullHouse;
      else 
        return HandType.ThreeOfAKind;
    }
    if (cardsCounts.Max() == 2) {
      if (cardsCounts.FindAll(x => x == 2).Count == 2)
        return HandType.TwoPair;
      else 
        return HandType.OnePair;
    }
    if (cardsCounts.Max() == 1) 
      return HandType.HighCard;

    throw new Exception("Unknown Hand Type");
  }

  static HandType GetTypeX(string hand)
  {
    var cardsCounts = new List<int>(AllCards.Select(_ => 0));

    foreach (var card in hand)
    {
      var cardIndex = AllCards.IndexOf(card);
      if (cardIndex == 0) continue; // Skip X
      cardsCounts[cardIndex]++;
    }

    cardsCounts[cardsCounts.IndexOf(cardsCounts.Max())] +=
      hand.Count(x => x == 'X');

    if (cardsCounts.Max() == 5)
      return HandType.FiveOfAKind;
    if (cardsCounts.Max() == 4)
      return HandType.FourOfAKind;
    if (cardsCounts.Max() == 3)
    {
      if (cardsCounts.Contains(2))
        return HandType.FullHouse;
      else
        return HandType.ThreeOfAKind;
    }
    if (cardsCounts.Max() == 2)
    {
      if (cardsCounts.FindAll(x => x == 2).Count == 2)
        return HandType.TwoPair;
      else
        return HandType.OnePair;
    }
    if (cardsCounts.Max() == 1)
      return HandType.HighCard;

    throw new Exception("Unknown Hand Type");
  }

  class HandComparer : IComparer<Hand>
  {
    public int Compare(Hand? x, Hand? y)
    {
      if (x?.HandType != y?.HandType)
        return x.HandType.CompareTo(y.HandType);
      
      for (int cardIdx = 0; cardIdx < 5; ++cardIdx) {
        if (AllCards.IndexOf(x.Cards[cardIdx]) != AllCards.IndexOf(y.Cards[cardIdx]))
          return AllCards.IndexOf(x.Cards[cardIdx]).CompareTo(AllCards.IndexOf(y.Cards[cardIdx]));
      }

      return 0;
    }
  }

  enum HandType {
    Unknown,
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
  };
}