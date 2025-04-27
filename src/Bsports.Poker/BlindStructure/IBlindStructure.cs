namespace Bsports.Poker.BlindStructure;

public interface IBlindStructure
{
    public (int smallBlind, int bigBlind) CalculateBlinds(int smallBlind, int bigBlind, int round);
}