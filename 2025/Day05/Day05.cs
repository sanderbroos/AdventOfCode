namespace AOC2025;

public class Day05
{
    public static long Part1()
    {
        var ranges = GetRanges();
        var ingredients = GetIngredients();
        var freshCount = 0;

        foreach (var ingredient in ingredients)
        {
            foreach (var range in ranges)
            {
                if (ingredient >= range[0] && ingredient <= range[1])
                {
                    freshCount++;
                    break;
                }
            }
        }

        return freshCount;
    }

    public static long Part2()
    {
        var ranges = GetRanges();
        var rangesMerged = true;

        while (rangesMerged)
        {
            rangesMerged = false;
            foreach (var range1 in ranges)
            {
                foreach (var range2 in ranges)
                {
                    if (range1 == range2) continue;

                    if (range2[0] >= range1[0] && range2[0] <= range1[1] && range2[1] > range1[1])
                    {
                        range1[1] = range2[1];
                        ranges.Remove(range2);
                        rangesMerged = true;
                        break;
                    }

                    if (range2[0] < range1[0] && range2[1] >= range1[0] && range2[1] <= range1[1])
                    {
                        range1[0] = range2[0];
                        ranges.Remove(range2);
                        rangesMerged = true;
                        break;
                    }

                    if (range2[1] <= range1[1] && range2[0] >= range1[0])
                    {
                        rangesMerged = true;
                        ranges.Remove(range2);
                        break;
                    }
                }
                if (rangesMerged) break;
            }
        }

        return ranges.Sum(r => r[1] - r[0] + 1);
    }

    private static List<long[]> GetRanges()
    {
        var ranges = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day05/inputRanges.txt")
            .Select(line => line.Split('-').Select(n => long.Parse(n)).ToArray()).ToList();

        return ranges;
    }

    private static List<long> GetIngredients()
    {
        var ranges = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day05/inputIngredients.txt")
            .Select(line => long.Parse(line)).ToList();

        return ranges;
    }
}
