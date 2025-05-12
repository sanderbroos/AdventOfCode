namespace AOC2024;

public class Day19
{
    public static long Part1()
    {
        List<string> towels = GetTowels();
        List<string> designs = GetDesigns();
        var towelCalculator = new TowelArrangementCalculator(towels);

        return designs.Count(design => towelCalculator.GetTotalNumberOfArrangements(design) > 0);
    }

    public static long Part2()
    {
        List<string> towels = GetTowels();
        List<string> designs = GetDesigns();
        var towelCalculator = new TowelArrangementCalculator(towels);

        return designs.Sum(towelCalculator.GetTotalNumberOfArrangements);
    }

    private class TowelArrangementCalculator(List<string> towels)
    {
        private readonly List<string> _towels = towels;
        private readonly Dictionary<string, long> _cachedArrangementCounts = [];

        public long GetTotalNumberOfArrangements(string design)
        {
            if (!_cachedArrangementCounts.TryGetValue(design, out long cachedCount))
            {
                cachedCount = _towels.Sum(towel => CalcNumberOfArrangementsStartingWithTowel(design, towel));
                _cachedArrangementCounts[design] = cachedCount;
            }

            return cachedCount;
        }

        private long CalcNumberOfArrangementsStartingWithTowel(string design, string towel)
        {
            if (towel.Length > design.Length)
            {
                return 0;
            }
            else if (design == towel)
            {
                return 1;
            }
            else if (design[..towel.Length] == towel)
            {
                return GetTotalNumberOfArrangements(design[towel.Length..]);
            }

            return 0;
        }
    }

    private static List<string> GetTowels()
    {
        var towels = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day19\\inputTowels.txt")[0]
            .Split(", ")
            .ToList();

        return towels;
    }

    private static List<string> GetDesigns()
    {
        var towels = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day19\\inputDesigns.txt")
            .ToList();

        return towels;
    }
}
