namespace Bsports.Poker.BlindStructure;

public class MultiplicativeBlindStructure(int factor) : IBlindStructure
{
    public (int smallBlind, int bigBlind) CalculateBlinds(int smallBlind, int bigBlind, int round)
    {
        return (smallBlind * factor, bigBlind * factor);
    }
}