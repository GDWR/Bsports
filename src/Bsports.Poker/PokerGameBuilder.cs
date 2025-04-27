using Bsports.Cards;
using Bsports.Poker.BlindStructure;

namespace Bsports.Poker;

public class PokerGameBuilder
{
    private IDeckFactory? _deckFactory;
    private IBlindStructure? _blindStructure;
    private int? _startingBalance;
    private int? _smallBlind;
    private int? _bigBlind;
    private readonly List<IPokerBot> _bots = [];

    private static void ThrowIfAlreadySet(object? value, string name)
    {
        if (value != null)
            throw new InvalidOperationException($"The {name} has already been set.");
    }

    private static void ThrowIfNotSet(object? value, string name)
    {
        if (value == null)
            throw new InvalidOperationException($"The {name} has not been set.");
    }

    public static PokerGameBuilder Empty()
    {
        return new PokerGameBuilder();
    }

    public PokerGameBuilder StartingBalance(int balance)
    {
        ThrowIfAlreadySet(_startingBalance, nameof(_startingBalance));
        _startingBalance = balance;
        return this;
    }

    public PokerGameBuilder WithDeckFactory(IDeckFactory deckFactory)
    {
        ThrowIfAlreadySet(_deckFactory, nameof(_deckFactory));
        _deckFactory = deckFactory;
        return this;
    }

    public PokerGameBuilder WithBlindStructure(IBlindStructure blindStructure)
    {
        ThrowIfAlreadySet(_blindStructure, nameof(_blindStructure));
        _blindStructure = blindStructure;
        return this;
    }

    public PokerGameBuilder StartingBlinds(int smallBlind, int bigBlind)
    {
        ThrowIfAlreadySet(_smallBlind, nameof(_smallBlind));
        ThrowIfAlreadySet(_bigBlind, nameof(_bigBlind));
        _smallBlind = smallBlind;
        _bigBlind = bigBlind;
        return this;
    }

    public PokerGameBuilder AddBot(IPokerBot bot)
    {
        _bots.Add(bot);
        return this;
    }

    public PokerGameBuilder AddBots(IEnumerable<IPokerBot> bots)
    {
        _bots.AddRange(bots);
        return this;
    }

    public PokerGame Build()
    {
        ThrowIfNotSet(_deckFactory, nameof(_deckFactory));
        ThrowIfNotSet(_blindStructure, nameof(_blindStructure));
        ThrowIfNotSet(_startingBalance, nameof(_startingBalance));
        ThrowIfNotSet(_smallBlind, nameof(_smallBlind));
        ThrowIfNotSet(_bigBlind, nameof(_bigBlind));

        if (_bots.Count < 2)
            throw new InvalidOperationException("At least two bots are required to play a game.");

        return new PokerGame(
            players: _bots
                .Select(bot => new PokerPlayer { Balance = _startingBalance!.Value, Logic = bot })
                .ToList(),
            deckFactory: _deckFactory!,
            blindStructure: _blindStructure!,
            smallBlind: _smallBlind!.Value,
            bigBlind: _bigBlind!.Value);
    }
}