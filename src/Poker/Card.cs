namespace Poker;

// TODO: Deduplicate this enum with PlayingCardViewModel
public record Card(CardType cardType)
{
    /// <summary>
    /// Gets the card suit.
    /// </summary> 
    /// <value>The card suit.</value>
    public CardSuit Suit
    {
        get
        {
            //  The suit can be worked out from the numeric value of the CardType enum.
            var enumVal = (int)cardType;
            return enumVal switch
            {
                < 13 => CardSuit.Hearts,
                < 26 => CardSuit.Diamonds,
                < 39 => CardSuit.Clubs,
                _ => CardSuit.Spades
            };
        }
    }

    /// <summary>
    /// Gets the card value.
    /// </summary>
    /// <value>The card value.</value>
    public int Value =>
        //  The CardType enum has 13 cards in each suit.
        (int)cardType % 13;

    /// <summary>
    /// Gets the card colour.
    /// </summary>
    /// <value>The card colour.</value>
    public CardColour Colour =>
        //  The first two suits in the CardType enum are red, the last two are black.
        (int)cardType < 26 ? CardColour.Red : CardColour.Black;
}