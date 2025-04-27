using Bsports.Cards;
using Bsports.Poker.BlindStructure;

namespace Bsports.Poker;

public class PokerGame(
    IList<PokerPlayer> players,
    IDeckFactory deckFactory,
    IBlindStructure blindStructure,
    int smallBlind,
    int bigBlind)
{
    public int Round { get; private set; } = 0;
    public int SmallBlind { get; private set; } = smallBlind;
    public int BigBlind { get; private set; } = bigBlind;

    private IDeckFactory _deckFactory = deckFactory;
    private IBlindStructure _blindStructure = blindStructure;
    private IList<PokerPlayer> _players = players;
    
    private int _pot = 0;
    private int _currentBet = 0;


    public void Play()
    {
        Console.WriteLine($"Starting Poker game with {_players.Count} players");
        Console.WriteLine($"Small blind: {SmallBlind}");
        Console.WriteLine($"Big blind: {BigBlind}");
        Console.WriteLine($"Blind structure: {_blindStructure}");

        while (IsRoundPlayable())
        {
            PlayRound();
            Round++;

            (SmallBlind, BigBlind) = _blindStructure.CalculateBlinds(SmallBlind, BigBlind, Round);
            break;
        }
        
        Console.WriteLine("Game over");
        Console.WriteLine($"Final round: {Round}");
        Console.WriteLine($"Final blinds: {SmallBlind}, {BigBlind}");
        Console.WriteLine("Final players:");
        foreach (var player in _players)
            Console.WriteLine($"Player: {player.Name}, Balance: {player.Balance}");
    }

    private void PlayRound()
    {
        Console.WriteLine($"Playing round {Round}");
        
        var deck = _deckFactory.CreateDeck();
        deck.Shuffle();
        
        foreach (var player in _players)
        {
            var card1 = deck.Draw();
            var card2 = deck.Draw();
            Console.WriteLine($"Player {player.Name} draws {card1} and {card2}");
        }
        
        // First two players are the blinds
        PlayerRaise(_players[0], SmallBlind);
        PlayerRaise(_players[1], BigBlind);

        var stillBetting = true;
        while (stillBetting)
        {
            stillBetting = false;
            foreach (var player in _players)
            {
                var action = player.Logic.GetAction();
                
                if (player.RoundContribution < _currentBet)
                {
                    stillBetting = true;
                    PlayerRaise(player, _currentBet);
                }
            }
        }
        
        Console.WriteLine($"Pot: {_pot}");
        Console.WriteLine($"Current bet: {_currentBet}");
        
    }
    
    private void PlayerRaise(PokerPlayer player, int amount)
    {
        if (amount > player.Balance)
            throw new InvalidOperationException($"Player {player.Name} does not have enough balance to bet {amount}");
        if (amount < _currentBet)
            throw new InvalidOperationException($"Player {player.Name} must bet at least {_currentBet}");
        
        Console.WriteLine($"Player {player.Name} raises {amount}");
        var toAdd = amount - player.RoundContribution;
        player.Balance -= toAdd;
        player.RoundContribution += toAdd;
        _pot += toAdd;
        
        _currentBet = amount;
    }

    private bool IsRoundPlayable()
        // There must be at least 2 players with enough balance to play.
        => _players.Count(player => player.Balance >= BigBlind) >= 2;
}