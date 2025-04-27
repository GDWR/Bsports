namespace Bsports.Cards;

public class TraditionalDeck : IDeck
{
    private readonly List<Card> _cards = new(52);
    private readonly Random _random = new();

    public TraditionalDeck()
    {
        for (var suit = 0; suit < 4; suit++)
        {
            for (var rank = 1; rank <= 13; rank++)
            {
                _cards.Add(new Card((CardRank)rank, (CardSuit)suit));
            }
        }
    }

    public void Shuffle()
    {
        _random.Shuffle(_cards);
    }
    
    public Card Draw()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("No cards left in the deck.");
        }

        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }
}