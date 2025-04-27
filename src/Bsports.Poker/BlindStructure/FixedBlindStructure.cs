namespace Bsports.Poker.BlindStructure;

public class FixedBlindStructure : IBlindStructure
{
    public (int smallBlind, int bigBlind) CalculateBlinds(int smallBlind, int bigBlind, int round)
    {
        return (smallBlind, bigBlind);
    }
}