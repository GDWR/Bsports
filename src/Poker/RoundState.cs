namespace Poker;

/// <summary> All the information, known to you, about the current round. </summary>
public record RoundState
{
    /// <summary> The current phase of the round. </summary>
    public required RoundPhase Phase { get; init; }

    /// <summary> Your index in the turn order. </summary>
    /// <remarks> zero-indexed. </remarks>
    public required int PlayerIndex { get; init; }

    /// <summary> If each player is folded or not. </summary>
    /// <remarks> Each index matches the player index. </remarks>
    public required IList<bool> PlayersFolded { get; init; }
    
    /// <summary> The amount of money in the pot. </summary>
    public required int Pot { get; init; }

    /// <summary> The amount of money you have in the pot. </summary>
    public required int PotContribution { get; init; }

    /// <summary> The current bet amount. </summary>
    public required int CurrentBet { get; init; }
    
    /// <summary> The amount of money you have in the current round. </summary>
    /// <remarks> This does not include any money currently in the pot. see <see cref="PotContribution"/>. </remarks>
    public required int Balance { get; init; }

    /// <summary> Cards held by you. </summary>
    public required (Card, Card) Hand { get; init; }

    /// <summary> Community cards on the table, depending on the phase these may be empty. </summary>
    /// <remarks>
    ///   <para> During <see cref="RoundPhase.PreFlop"/> all entries are null. </para>
    ///   <para> During <see cref="RoundPhase.Flop"/>, the last two entries are null. </para>
    ///   <para> During <see cref="RoundPhase.Turn"/>, the last entry is null. </para>
    ///   <para> During <see cref="RoundPhase.River"/>, all cards are present. </para>
    /// </remarks>
    public required (Card?, Card?, Card?, Card?, Card?) CommunityCards { get; init; }
}