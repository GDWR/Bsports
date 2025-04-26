using Poker;
using Poker.Bot;

namespace RandomBot;

public class RandomBot : IPokerBot
{
    public string Name => "RandomBot";

    public Task<BotAction> GetAction(RoundState state, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new BotAction());
    }
}