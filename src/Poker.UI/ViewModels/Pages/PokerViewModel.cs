using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Poker.Bot;
using Poker.UI.Utils;

namespace Poker.UI.ViewModels.Pages;


class PokerPlayer
{
    public required IPokerBot Logic { get; init; }
    public required int Balance { get; set; }
    public int Contribution { get; set; } = 0;
    
    public required BatchObservableCollection<PlayingCardViewModel> HandCollection { get; init; }
    public (Card, Card) Hand => (new Card(HandCollection[0].CardType), new Card(HandCollection[1].CardType));
}

/// <summary>
/// The Poker View Model.
/// </summary>
public class PokerViewModel : CardGameViewModel
{
    public override string GameName => "Poker";
    
    #region Poker Game Configuration

    private const int SmallBlind = 25;
    private const int BigBlind = 50;
    private const int StartingBalance = 500;

    #endregion
    
    #region Game Speed

    private const float Speed = 1.0f;
    private readonly TimeSpan ShortPause = TimeSpan.FromMilliseconds(75 / Speed);
    private readonly TimeSpan LongPause = TimeSpan.FromMilliseconds(175 / Speed);

    #endregion

    #region UI Elements/Collections

    private readonly List<BatchObservableCollection<PlayingCardViewModel>> _cells = [];
    private readonly List<BatchObservableCollection<PlayingCardViewModel>> _hands = [];

    public BatchObservableCollection<PlayingCardViewModel> Deck { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Cell1 { get; } = new();

    public BatchObservableCollection<PlayingCardViewModel> Cell2 { get; } = new();

    public BatchObservableCollection<PlayingCardViewModel> Cell3 { get; } = new();

    public BatchObservableCollection<PlayingCardViewModel> Cell4 { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Cell5 { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Hand1 { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Hand2 { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Hand3 { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Hand4 { get; } = new();
    public BatchObservableCollection<PlayingCardViewModel> Hand5 { get; } = new();

    #endregion
    
    private int _currentBet = 0;
    private int _currentPot = 0;
    private readonly List<PokerPlayer> _players = [];

    
    public PokerViewModel(CasinoViewModel casinoViewModel) : base(casinoViewModel)
    {
        #region Initialize UI Elements/Collections

        _cells.Add(Cell1);
        _cells.Add(Cell2);
        _cells.Add(Cell3);
        _cells.Add(Cell4);
        _cells.Add(Cell5);
        
        _hands.Add(Hand1);
        _hands.Add(Hand2);
        _hands.Add(Hand3);
        _hands.Add(Hand4);
        _hands.Add(Hand5);

        #endregion
        
        NewGameCommand = new AsyncRelayCommand(DoNewGame);
    }
    
    private async Task DoNewGame()
    {
        ResetGame();
        InitPlayers();

        while (true)
        {
            await PlayGame();
        }
    }
    
    private void InitPlayers()
    {
        for (var i = 0; i < 5; i++)
        {
            var player = new PokerPlayer
            {
                Balance = StartingBalance,
                Logic = BotRegistration.RandomBot,
                HandCollection = _hands[i],
            };
            _players.Add(player);
        }
    }

    private async Task PlayGame()
    {
        var playingCards = GetNewShuffledDeck();

        // Deal to players, one card at a time
        for (var i = 0; i < 2; i++)
        {
            foreach (var player in _players)
            {
                var card = playingCards.First();
                card.IsFaceDown = false;
                player.HandCollection.Add(card);
                playingCards.Remove(card);
                
                await Task.Delay(LongPause);
            }
        }
        
        // First player pays the small blind
        var firstPlayer = _players[0];
        firstPlayer.Balance -= SmallBlind;
        firstPlayer.Contribution += SmallBlind;
        _currentPot += SmallBlind;
        _currentBet = SmallBlind;
        
        // Second player pays the big blind
        var secondPlayer = _players[1];
        secondPlayer.Balance -= BigBlind;
        secondPlayer.Contribution += BigBlind;
        _currentPot += BigBlind;
        _currentBet = BigBlind;
        
        // Play the pre-flop
        foreach (var player in _players[2..])
        {
            Console.WriteLine("State:");
            Console.WriteLine($"  Player: {player.Logic.Name}");
            Console.WriteLine($"  Balance: {player.Balance}");
            Console.WriteLine($"  Contribution: {player.Contribution}");
            Console.WriteLine($"  Hand: {player.HandCollection[0].CardType}, {player.HandCollection[1].CardType}");
            Console.WriteLine($"  Pot: {_currentPot}");
            Console.WriteLine($"  Current Bet: {_currentBet}");
            Console.WriteLine($"  Community Cards: {string.Join(", ", Cell1.Select(c => c.CardType))}");
            Console.WriteLine($"  Players Folded: {string.Join(", ", _players.Select(p => p.HandCollection.Count == 0))}");
            Console.WriteLine($"  Player Index: {_players.IndexOf(player)}");
            Console.WriteLine();
            var state = new RoundState
            {
                Phase = RoundPhase.PreFlop,
                Pot = _currentPot,
                PotContribution = player.Contribution,
                Hand = player.Hand,
                CommunityCards = (null, null, null, null, null),
                PlayerIndex = _players.IndexOf(player),
                PlayersFolded = _players.Select(p => p.HandCollection.Count == 0).ToList(),
                CurrentBet = _currentBet,
                Balance = player.Balance,
            };
            var action = await player.Logic.GetAction(state);
            await HandleAction(player, action);
            await Task.Delay(LongPause);
            Console.WriteLine($"Player {player.Logic.Name} action: {action.Action}" +
                              $"{(action.Amount.HasValue ? $" ({action.Amount})" : "")}");
        }
    }

    private Task PlayRound()
    {

        return Task.CompletedTask;
    }
    
    private async Task HandleAction(PokerPlayer player, BotAction action)
    {
        switch (action.Action)
        {
            case ActionType.Call:
                if (action.Amount.HasValue)
                {
                    player.Balance -= action.Amount.Value;
                    player.Contribution += action.Amount.Value;
                    _currentPot += action.Amount.Value;
                }
                break;
            case ActionType.Raise:
                if (action.Amount.HasValue && action.Amount > _currentBet)
                {
                    player.Balance -= action.Amount.Value;
                    player.Contribution += action.Amount.Value;
                    _currentPot += action.Amount.Value;
                    _currentBet = action.Amount.Value;
                }
                break;
            case ActionType.Check:
                break;
            case ActionType.Fold:
                foreach (var card in player.HandCollection)
                {
                    card.IsFaceDown = true;
                    await Task.Delay(ShortPause);
                }
                
                break;
        }
    }
    
    public override void ResetGame()
    {
        _currentBet = 0;
        _currentPot = 0;
        _players.Clear();
        Deck.Clear();
        foreach (var cards in _hands.Concat(_cells))
            cards.Clear();
    }

    /// <summary> Unused and to be refactored. </summary>
    public override bool CheckAndMoveCard(IList<PlayingCardViewModel> from, IList<PlayingCardViewModel> to, PlayingCardViewModel card, bool checkOnly = false) => false;
    /// <summary> Unused and to be refactored. </summary>
    public override IList<PlayingCardViewModel>? GetCardCollection(PlayingCardViewModel card) => null;
}