namespace Bsports.Poker.BlindStructure;

public class AdditiveBlindStructure(int amount) : IBlindStructure
{
    public (int smallBlind, int bigBlind) CalculateBlinds(int smallBlind, int bigBlind, int round)
    {
        return (smallBlind + amount, bigBlind + amount);
    }
}