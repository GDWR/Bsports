using Bsports.Poker;

public class PokerBot : IPokerBot
{
    public string Name => nameof(BotName);

    public PokerAction GetAction()
    {
        return new PokerAction();
    }
}