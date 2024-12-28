namespace AOC2024;

public class Day25
{
    public int Part1()
    {
        (List<int[]> locks, List<int[]> keys) = GetInput();
        int validCount = 0;

        foreach (int[] lockHeights in locks)
        {
            foreach (int[] keyHeights in keys)
            {
                if (lockHeights.Zip(keyHeights).All(z => z.First + z.Second <= 5))
                {
                    validCount++;
                }
            }
        }
        return validCount;
    }

    public int Part2()
    {
        return 0;
    }

    private static (List<int[]> locks, List<int[]> keys) GetInput()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day25\\input.txt");

        List<List<string>> schematics = [];
        List<string> schematic = [];

        foreach (string line in input)
        {
            if (line == "")
            {
                schematics.Add(schematic);
                schematic = [];
                continue;
            }
            schematic.Add(line);
        }

        List<int[]> locks = [];
        List<int[]> keys = [];

        foreach (var schematicToConvert in schematics)
        {
            if (schematicToConvert.First().Contains('#'))
            {
                locks.Add(ConvertToCounts(schematicToConvert[1..]));
            }
            else if (schematicToConvert.Last().Contains('#'))
            {
                keys.Add(ConvertToCounts(schematicToConvert[..^1]));
            }
            else
            {
                throw new Exception();
            }
        }

        return (locks, keys);
    }

    static int[] ConvertToCounts(List<string> schematic)
    {
        int[] counts = new int[schematic.First().Length];
        foreach (string line in schematic)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    counts[i]++;
                }
            }
        }
        return counts;
    }
}
