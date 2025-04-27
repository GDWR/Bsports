using Bsports;
using Bsports.Cards;
using Bsports.Poker;
using Bsports.Poker.BlindStructure;

var botLoader = new BotLoader<IPokerBot>();

var pokerGame = PokerGameBuilder.Empty()
    .WithDeckFactory(new TraditionalDeckFactory())
    .StartingBalance(1000)
    .StartingBlinds(25, 50)
    .WithBlindStructure(new AdditiveBlindStructure(25))
    .AddBots(botLoader.LoadBots("./plugins"))
    .Build();

pokerGame.Play();