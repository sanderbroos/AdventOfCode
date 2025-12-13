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
        var redCoords = GetRedTileCoords();

        bool[,] grid = new bool[redCoords.Max(c => c[0]), redCoords.Max(c => c[1])];
        long[] lastCoord = redCoords.Last();
        int[] prevDir = null;
        List<(long[], int[])> leftTurns = [];
        List<(long[], int[])> rightTurns = [];

        foreach (var currentCoord in redCoords)
        {
            int[] dir = [currentCoord[0] - lastCoord[0] > 0 ? 1 : -1, currentCoord[1] - lastCoord[1] > 0 ? 1 : -1];

            if (prevDir is not null && prevDir[0] * dir[0] == 0)
            {
                if (prevDir[0] * dir[0] == 1 || prevDir[1] * dir[1] == -1)
                {
                    leftTurns.Add((currentCoord, dir));
                }
                else
                {
                    rightTurns.Add((currentCoord, dir));
                }
            }

            prevDir = dir;
            long[] coordToFill = [lastCoord[0], lastCoord[1]];

            while (grid[currentCoord[0], currentCoord[1]] == false)
            {
                coordToFill = [coordToFill[0] + dir[0], coordToFill[1] + dir[1]];
                grid[coordToFill[0], coordToFill[1]] = true;
            }
        }

        long[] startingCoord = [0, 0];
        if (leftTurns.Count > rightTurns.Count)
        {
            (var coord, var dir) = leftTurns[0];
            startingCoord = [coord[0] + dir[0] + dir[1], coord[1] + dir[1] - dir[0]];
        }
        else
        {
            (var coord, var dir) = rightTurns[0];
            startingCoord = [coord[0] + dir[0] - dir[1], coord[1] + dir[1] + dir[0]];
        }

        fillAreaFromCoord(grid, startingCoord);

        return 0;
    }

    private static void fillAreaFromCoord(bool[,] grid, long[] coord)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                if (grid[coord[0], coord[1]] == true) continue;
                grid[coord[0], coord[1]] = true;
                fillAreaFromCoord(grid, [coord[0] + i, coord[1] + j]);
            }
        }
    }

    private static List<long[]> GetRedTileCoords()
    {
        var redTileCoords = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day09/input.txt")
            .Select(line => line.Split(',').Select(s => long.Parse(s)).ToArray()).ToList();

        return redTileCoords;
    }
}
