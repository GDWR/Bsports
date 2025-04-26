namespace Poker.Bot;

public enum ActionType
{
    Call,
    Raise,
    Check,
    Fold,
}

public record BotAction
{
    /// <summary>Action to take.</summary>
    public required ActionType Action { get; init; }

    /// <summary>Amount to raise the bet to. Null if not applicable.</summary>
    /// <remarks>Only applicable when <see cref="BotAction.Action"/> is <see cref="ActionType.Raise"/>.</remarks>
    /// <remarks>If this value is less than current bet, it is considered invalid.</remarks>
    public int? Amount { get; init; } = null;
}

public interface IPokerBot
{
    /// <summary>The display name of the bot in the UI and scoreboard.</summary>
    string Name { get; }

    /// <summary>From the provided game state, return the action to take.</summary>
    /// <remarks>Invalid Action responses will result in a Fold.</remarks>
    /// <remarks>Execution is limited to 1 second.</remarks>
    Task<BotAction> GetAction(RoundState state, CancellationToken cancellationToken = default);
}