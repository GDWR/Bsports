namespace Bsports.Cards;

public class TraditionalDeckFactory : IDeckFactory
{
    public IDeck CreateDeck()
    {
        return new TraditionalDeck();
    }
}