namespace Bsports.Cards;

public record Card(CardRank Rank, CardSuit Suit)
{
    public override string ToString()
        => $"{Rank} of {Suit}";
}