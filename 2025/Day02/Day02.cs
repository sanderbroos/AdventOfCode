namespace AOC2025;

public class Day02
{
    public static long Part1()
    {
        var ranges = GetIDs();
        long sumOfInvalidIds = 0;

        foreach (var range in ranges)
        {
            for (var id = range.First(); id < range.Last() + 1; id++)
            {
                var idAsString = id.ToString();
                var length = idAsString.Length;
                if (length % 2 == 0 && idAsString[..(length / 2)] == idAsString[(length / 2)..])
                {
                    sumOfInvalidIds += id;
                }
            }
        }

        return sumOfInvalidIds;
    }

    public static long Part2()
    {
        var ranges = GetIDs();
        long sumOfInvalidIds = 0;

        foreach (var range in ranges)
        {
            for (var id = range.First(); id < range.Last() + 1; id++)
            {
                var idAsString = id.ToString();
                var length = idAsString.Length;

                for (var repititions = 2; repititions <= length; repititions++)
                {
                    if (length % repititions == 0)
                    {
                        List<string> partitions = [];
                        for (var i = 0; i < length; i += length / repititions)
                        {
                            partitions.Add(idAsString[i..(i + length / repititions)]);
                        }

                        if (partitions.ToHashSet().Count == 1)
                        {
                            sumOfInvalidIds += id;
                            break;
                        }
                    }
                }
            }
        }

        return sumOfInvalidIds;
    }

    private static List<List<long>> GetIDs()
    {
        var ranges = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day02/input.txt")[0]
            .Split(",")
            .Select(range => range.Split("-").Select(s => long.Parse(s)).ToList())
            .ToList();

        return ranges;
    }
}
