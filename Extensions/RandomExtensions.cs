namespace Vipl.Base.Extensions;

/// <summary> Extensions for <see cref="Random"/> </summary>
public static class RandomExtensions
{
    /// <summary> Make randomly distributed <see cref="IEnumerable{T}"/> of <see cref="decimal"/>
    /// with total sum of <paramref name="totalSum"/>. Number of returned decimal values is <paramref name="numberOfDecimals"/>
    /// Number of decimals for each value is <paramref name="numberOfDecimals"/> </summary>
    /// <param name="randomGenerator">Random number generator used for randomness</param>
    /// <param name="totalSum">Total sum of all returned elements.</param>
    /// <param name="numberOfElements">Number of returned elements.</param>
    /// <param name="numberOfDecimals">Precision of each returned elements. Defaults to 0.</param>
    /// <returns>Randomly distributed <see cref="IEnumerable{T}"/> of <see cref="decimal"/> with total sum of <paramref name="totalSum"/>. Number of returned decimal values is <paramref name="numberOfDecimals"/>
    /// Number of decimals for each value is <paramref name="numberOfDecimals"/></returns>
    public static IEnumerable<decimal> RandomDistribution(this Random randomGenerator, decimal totalSum,
        int numberOfElements, int numberOfDecimals = 0)
    {
        if (totalSum == 0)
        {
            return Enumerable.Range(1, numberOfElements).Select(_ => (decimal) 0);
        }

        var randomList = Enumerable.Range(1, numberOfElements).Select(_ => randomGenerator.NextDouble()).ToArray();
        var scaleFactor = randomList.Sum(x => x) / (double) totalSum;
        decimal totalReturned = 0;
        var result = new List<decimal>();
        foreach (var i in randomList.Take(randomList.Length - 1))
        {
            var newReturn = decimal.Round((decimal) (i / scaleFactor), numberOfDecimals);
            totalReturned += newReturn;
            result.Add(newReturn);
        }

        while (totalSum - totalReturned <= 0)
        {
            for (var i = 0; i < result.Count; i++)
            {
                if (result[i] == 0) continue;
                result[i]--;
                totalReturned--;
                if (totalSum - totalReturned <= 0)
                {
                    break;
                }
            }
        }

        result.Add(totalSum - totalReturned);
        return result;
    }
}