namespace Bsports.Poker;

public interface IPokerBot : IBot
{
    PokerAction GetAction();
}