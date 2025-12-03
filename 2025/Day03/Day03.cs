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
            var outputJoltage = "";
            var start = 0;

            for (int end = bank.Count - 11; end <= bank.Count; end++)
            {
                var highest = bank[start..end].Max();
                outputJoltage += highest;
                start = bank.IndexOf(highest, start) + 1;
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
