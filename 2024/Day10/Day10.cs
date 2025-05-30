namespace AOC2024;

public class Day10
{
    readonly (int x, int y)[] Directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    public int Part1()
    {
        List<List<int>> topographicMap = GetInput();

        int scoreSum = 0;

        for (int y = 0; y < topographicMap.Count; y++)
        {
            for (int x = 0; x < topographicMap.First().Count; x++)
            {
                if (topographicMap[y][x] == 0)
                {
                    scoreSum += ReachablePeaks(topographicMap, x, y).Distinct().Count();
                }
            }
        }

        return scoreSum;
    }

    public int Part2()
    {
        List<List<int>> topographicMap = GetInput();

        int scoreSum = 0;

        for (int y = 0; y < topographicMap.Count; y++)
        {
            for (int x = 0; x < topographicMap.First().Count; x++)
            {
                if (topographicMap[y][x] == 0)
                {
                    scoreSum += ReachablePeaks(topographicMap, x, y).Count;
                }
            }
        }

        return scoreSum;
    }

    private List<(int x, int y)> ReachablePeaks(List<List<int>> topographicMap, int x, int y)
    {
        int currentHeight = topographicMap[y][x];
        List<(int x, int y)> reachablePeaks = [];

        foreach ((int xOffset, int yOffset) direction in Directions)
        {
            int adjacentX = x + direction.xOffset;
            int adjacentY = y + direction.yOffset;

            if (adjacentX >= 0 && adjacentX < topographicMap.First().Count
                && adjacentY >= 0 && adjacentY < topographicMap.Count
                && topographicMap[adjacentY][adjacentX] - currentHeight == 1)
            {
                if (topographicMap[adjacentY][adjacentX] == 9)
                {
                    reachablePeaks.Add((adjacentX, adjacentY));
                }
                else
                {
                    reachablePeaks.AddRange(ReachablePeaks(topographicMap, adjacentX, adjacentY));
                }
            }
        }

        return reachablePeaks;
    }

    private static List<List<int>> GetInput()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day10\\input.txt")
            .Select(line => line.Select(n => int.Parse(n.ToString())).ToList())
            .ToList();

        return input;
    }
}
