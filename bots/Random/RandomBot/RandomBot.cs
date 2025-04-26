using Poker;
using Poker.Bot;

namespace RandomBot;

public class RandomBot : IPokerBot
{
    public string Name => "RandomBot";

    private readonly Random _random = new();

    public Task<BotAction> GetAction(RoundState state, CancellationToken cancellationToken = default)
    {
        List<BotAction> validActions =
        [
            new() { Action = ActionType.Fold },
        ];

        if (state.Balance > 0)
            validActions.Add(new BotAction { Action = ActionType.Raise, Amount = _random.Next(state.Balance) });


        validActions.Add(state.CurrentBet == state.PotContribution
            ? new BotAction { Action = ActionType.Check }
            : new BotAction { Action = ActionType.Call });


        var selectedAction = validActions[_random.Next(validActions.Count)];
        return Task.FromResult(selectedAction);
    }
}