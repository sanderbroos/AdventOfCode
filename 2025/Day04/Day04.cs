namespace AOC2025;

public class Day04
{
    private static char paperRoll = '@';

    public static long Part1()
    {
        var grid = GetGrid();
        var validRollsOfPaper = 0;

        for (var x = 0; x < grid[0].Count; x++)
        {
            for (var y = 0; y < grid.Count; y++)
            {
                var adjacentRollsOfPaper = 0;
                if (grid[y][x] != paperRoll) continue;

                for (var xoffset = -1; xoffset <= 1; xoffset++)
                {
                    for (var yoffset = -1; yoffset <= 1; yoffset++)
                    {
                        if (xoffset == 0 && yoffset == 0) continue;
                        var newX = x + xoffset;
                        var newY = y + yoffset;

                        if (newX < 0 || newX >= grid[0].Count || newY < 0 || newY >= grid.Count) continue;

                        if (grid[newY][newX] == paperRoll)
                        {
                            adjacentRollsOfPaper++;
                        }
                    }
                }

                if (adjacentRollsOfPaper < 4)
                {
                    validRollsOfPaper++;
                }
            }
        }

        return validRollsOfPaper;
    }

    public static long Part2()
    {
        return 0;
    }

    private static List<List<char>> GetGrid()
    {
        var ranges = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day04/input.txt")
            .Select(line => line.ToList()).ToList();

        return ranges;
    }
}
