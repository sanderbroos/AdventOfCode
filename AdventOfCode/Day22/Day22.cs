namespace AOC2024;

public class Day22
{
    public long Part1()
    {
        List<long> initialSecrets = GetInput();
        long sum = 0;

        foreach (long initialSecret in initialSecrets)
        {
            long secret = initialSecret;
            for (int i = 0; i < 2000; i++)
            {
                secret = NextSecret(secret);
            }
            sum += secret;
        }

        return sum;
    }

    public long Part2()
    {
        List<long> initialSecrets = GetInput();
        Dictionary<(long, long, long, long), long> totalPriceDict = [];

        foreach (long initialSecret in initialSecrets)
        {
            var diffs = FirstDiffs(initialSecret, 2000);
            var priceDict = GetPriceDictFromDiffs(diffs);

            foreach (var key in priceDict.Keys)
            {
                if (!totalPriceDict.ContainsKey(key))
                {
                    totalPriceDict[key] = 0;
                }
                totalPriceDict[key] += priceDict[key];
            }
        }

        return totalPriceDict.Values.Max();
    }

    long NextSecret(long secret)
    {
        secret = ((secret * 64) ^ secret) % 16777216;
        secret = ((secret / 32) ^ secret) % 16777216;
        secret = ((secret * 2048) ^ secret) % 16777216;
        return secret;
    }

    List<(long, long)> FirstDiffs(long initialSecret, int numberOfSecrets)
    {
        List<(long, long)> diffs = [];
        long secret = initialSecret;
        long price = initialSecret % 10;
        long newPrice;
        for (int i = 0; i < numberOfSecrets - 1; i++)
        {
            secret = NextSecret(secret);
            newPrice = secret % 10;
            diffs.Add((newPrice, newPrice - price));
            price = newPrice;
        }

        return diffs;
    }

    Dictionary<(long, long, long, long), long> GetPriceDictFromDiffs(List<(long price, long diff)> diffs)
    {
        Dictionary<(long, long, long, long), long> priceDict = [];

        for (int i = 0; i < diffs.Count - 3; i++)
        {
            (long, long, long, long) sequence = (diffs[i].diff, diffs[i + 1].diff, diffs[i + 2].diff, diffs[i + 3].diff);
            if (!priceDict.ContainsKey(sequence))
            {
                priceDict[sequence] = diffs[i + 3].price;
            }
        }

        return priceDict;
    }

    private static List<long> GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day22\\input.txt")
            .Select(long.Parse)
            .ToList();
    }
}
