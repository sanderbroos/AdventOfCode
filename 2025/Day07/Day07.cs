using System.Reflection.Metadata;

namespace AOC2025;

public class Day07
{
    private const char beam = '|';
    private const char splitter = '^';

    public static long Part1()
    {
        var tachyonManifold = GetTachyonManifold();
        tachyonManifold[1][tachyonManifold[0].IndexOf('S')] = beam;
        var splitCount = 0;

        for (var i = 2; i < tachyonManifold.Length; i++)
        {
            for (var j = 0; j < tachyonManifold[i].Length; j++)
            {
                if (tachyonManifold[i - 1][j] == beam)
                {
                    if (tachyonManifold[i][j] == splitter)
                    {
                        tachyonManifold[i][j - 1] = beam;
                        tachyonManifold[i][j + 1] = beam;
                        splitCount++;
                    }
                    else
                    {
                        tachyonManifold[i][j] = beam;
                    }
                }
            }
        }


        return splitCount;
    }

    public static long Part2()
    {
        return 0;
    }

    private static char[][] GetTachyonManifold()
    {
        var tachyonManifold = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day07/input.txt")
            .Select(line => line.ToArray()).ToArray();

        return tachyonManifold;
    }
}
