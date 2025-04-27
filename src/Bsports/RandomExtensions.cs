namespace Bsports;

public static class RandomExtensions
{
    /// <summary> Performs an in-place shuffle of the elements in the specified list using the specified random number generator. </summary>
    /// <param name="random"> The random number generator to use. </param>
    /// <param name="list"> The list to shuffle. </param>
    /// <typeparam name="T"> The type of elements in the list. </typeparam>
    /// <remarks>This implementation mirrors that of <see cref="Random.Shuffle{T}(Span{T})"/></remarks>
    public static void Shuffle<T>(this Random random, IList<T> list)
    {
        // This is the Random.Shuffle<T>(Span<T> values) method.
        int length = list.Count;
        for (int index1 = 0; index1 < length - 1; ++index1)
        {
            int index2 = random.Next(index1, length);
            if (index2 != index1)
            {
                T obj = list[index1];
                list[index1] = list[index2];
                list[index2] = obj;
            }
        }
    }
}