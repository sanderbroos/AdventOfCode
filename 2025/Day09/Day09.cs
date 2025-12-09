namespace AOC2025;

public class Day09
{
    public static long Part1()
    {
        var coords = GetRedTileCoords();
        long largestArea = 0;

        foreach (var coord1 in coords)
        {
            foreach (var coord2 in coords)
            {
                if (coord1 == coord2) continue;

                var area = Math.Abs(coord1[0] - coord2[0] + 1) * (coord1[1] - coord2[1] + 1);
                largestArea = area > largestArea ? area : largestArea;
            }
        }

        return largestArea;
    }

    public static long Part2()
    {
        return 0;
    }

    private static List<long[]> GetRedTileCoords()
    {
        var redTileCoords = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day09/input.txt")
            .Select(line => line.Split(',').Select(s => long.Parse(s)).ToArray()).ToList();

        return redTileCoords;
    }
}
