namespace AOC2025;

public class Day01
{
    public static long Part1()
    {
        var directions = GetDirections();
        var dial = 50;
        var zeroCount = 0;

        foreach (var direction in directions)
        {
            var dir = direction.Item1 == 'L' ? -1 : 1;

            dial = (dial + direction.Item2 * dir) % 100;
            if (dial == 0) zeroCount++;
        }

        return zeroCount;
    }

    public static long Part2()
    {
        var directions = GetDirections();
        var dial = 50;
        var zeroCount = 0;

        foreach (var direction in directions)
        {
            var dir = direction.Item1 == 'L' ? -1 : 1;

            for (int i = 0; i < direction.Item2; i++)
            {
                dial = (dial + dir) % 100;
                if (dial == 0) zeroCount++;
            }
        }

        return zeroCount;
    }

    private static List<(char, int)> GetDirections()
    {
        var directions = File.ReadAllLines("2025/Day01/input.txt")
            .Select(l => (l[0], int.Parse(l[1..])))
            .ToList();

        return directions;
    }
}
