namespace AOC2025;

public class Day03
{
    public static long Part1()
    {
        var banks = GetBanks();
        long totalOutputJoltage = 0;

        foreach (var bank in banks)
        {
            var highest = bank[..^1].Max();

            var highestIndex = bank.IndexOf(highest);
            var highestFollowingHighest = bank[(highestIndex + 1)..].Max();
            totalOutputJoltage += int.Parse($"{highest}{highestFollowingHighest}");
        }

        return totalOutputJoltage;
    }

    public static long Part2()
    {
        var banks = GetBanks();
        long totalOutputJoltage = 0;

        foreach (var bank in banks)
        {
            var highest = bank[..^11].Max();
            var outputJoltage = $"{highest}";
            var lastIndex = bank.IndexOf(highest);

            for (int i = 1; i < 12; i++)
            {
                var upToIndex = bank.Count - 12 + i;

                var startingIndex = lastIndex + 1 > upToIndex ? upToIndex : lastIndex + 1;
                var highestFollowingLast = bank[startingIndex..(upToIndex + 1)].Max();
                outputJoltage += highestFollowingLast;
                lastIndex = bank.IndexOf(highestFollowingLast, startingIndex);
            }

            totalOutputJoltage += long.Parse(outputJoltage);
        }

        return totalOutputJoltage;
    }

    private static List<List<int>> GetBanks()
    {
        var ranges = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day03/input.txt")
            .Select(line => line.ToList().Select(c => int.Parse(c.ToString())).ToList()).ToList();

        return ranges;
    }
}
