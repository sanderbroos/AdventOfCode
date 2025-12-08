using System.Reflection.Metadata;
using Microsoft.VisualBasic;

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
        var tachyonManifold = GetTachyonManifold();
        Dictionary<(int x, int y), long> cache = [];

        return TimelineCountFromPoint(tachyonManifold, tachyonManifold[0].IndexOf('S'), 1, cache);
    }

    private static long TimelineCountFromPoint(char[][] manifold, int x, int y, Dictionary<(int x, int y), long> cache)
    {
        if (cache.TryGetValue((x, y), out long value))
        {
            return value;
        }

        long timelineCount = 1;

        if (y >= manifold.Length - 1)
        {
            return timelineCount;
        }

        if (manifold[y + 1][x] == splitter)
        {
            timelineCount *= TimelineCountFromPoint(manifold, x - 1, y + 1, cache) + TimelineCountFromPoint(manifold, x + 1, y + 1, cache);
        }
        else
        {
            timelineCount *= TimelineCountFromPoint(manifold, x, y + 1, cache);
        }

        cache[(x, y)] = timelineCount;
        return timelineCount;
    }

    private static char[][] GetTachyonManifold()
    {
        var tachyonManifold = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day07/input.txt")
            .Select(line => line.ToArray()).ToArray();

        return tachyonManifold;
    }
}
