using Poker.Lib;

namespace Poker.Bot;

public record BotAction;

public interface IPokerBot
{
    /// <summary>The display name of the bot in the UI and scoreboard.</summary>
    string Name { get; }

    /// <summary>From the provided game state, return the action to take.</summary>
    /// <remarks>Execution is limited to 1 second.</remarks>
    Task<BotAction> GetAction(RoundState state, CancellationToken cancellationToken = default);
}