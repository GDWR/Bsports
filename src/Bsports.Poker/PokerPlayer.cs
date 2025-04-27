namespace Bsports.Poker;

public class PokerPlayer
{
    public int Balance { get; internal set; }
    public int RoundContribution { get; internal set; }
    public IPokerBot Logic { get; internal set; }

    public string Name => Logic.Name;
}