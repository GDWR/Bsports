namespace Poker.Lib;

public record RoundState
{
    public required int Pot { get; init; }
    public required RoundPhase Phase { get; init; }
    public required (Card, Card) Hand { get; init; }
    public required (Card?, Card?, Card?, Card?, Card?) CommunityCards { get; init; }
}