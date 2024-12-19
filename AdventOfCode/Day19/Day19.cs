namespace AOC2024;

public class Day19
{
    Dictionary<string, long> AlreadyCalculatedDesignCounts = [];

    public long Part1()
    {
        List<string> towels = GetTowels();
        List<string> designs = GetDesigns();

        return designs.Count(design => PossibleArrangementsCount(design, towels) > 0);
    }

    public long Part2()
    {
        List<string> towels = GetTowels();
        List<string> designs = GetDesigns();

        return designs.Sum(design => PossibleArrangementsCount(design, towels));
    }

    long PossibleArrangementsCount(string design, List<string> towels)
    {
        if (AlreadyCalculatedDesignCounts.ContainsKey(design))
        {
            return AlreadyCalculatedDesignCounts[design];
        }

        long count = 0;

        foreach (string towel in towels)
        {
            if (towel.Length > design.Length)
            {
                continue;
            }
            else if (design == towel)
            {
                count++;
            }
            else if (design[..towel.Length] == towel)
            {
                count += PossibleArrangementsCount(design[towel.Length..], towels);
            }
        }

        AlreadyCalculatedDesignCounts[design] = count;

        return count;
    }

    private static List<string> GetTowels()
    {
        var towels = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day19\\inputTowels.txt")[0]
            .Split(", ")
            .ToList();

        return towels;
    }

    private static List<string> GetDesigns()
    {
        var towels = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day19\\inputDesigns.txt")
            .ToList();

        return towels;
    }
}
