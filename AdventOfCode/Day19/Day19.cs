namespace AOC2024;

public class Day19
{
    public int Part1(int upTo = 1024)
    {
        List<string> towels = GetTowels();
        List<string> designs = GetDesigns();

        return designs.Count(design => isDesignPossible(design, towels));
    }

    public void Part2()
    {

    }

    bool isDesignPossible(string design, List<string> towels)
    {
        foreach (string towel in towels)
        {
            if (towel.Length > design.Length)
            {
                continue;
            }

            string subDesign = design[..towel.Length];
            if (design == towel || (subDesign == towel && isDesignPossible(design[towel.Length..], towels)))
            {
                return true;
            }
        }

        return false;
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
