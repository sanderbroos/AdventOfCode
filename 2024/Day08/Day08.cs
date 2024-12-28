namespace AOC2024;

public class Day08
{
    public int Part1()
    {
        (List<(int x, int y, char frequency)> antennae, int sizeX, int sizeY) = GetInput();
        HashSet<(int x, int y)> antinodes = [];

        foreach (var antenna in antennae)
        {
            var otherAntennae = antennae.Except([antenna]).Where(a => a.frequency == antenna.frequency);

            foreach (var otherAntenna in otherAntennae)
            {
                (int x, int y) newAntinode = (2 * otherAntenna.x - antenna.x, 2 * otherAntenna.y - antenna.y);
                if (!IsOutOfBounds(newAntinode.x, newAntinode.y, sizeX, sizeY))
                {
                    antinodes.Add(newAntinode);
                }
            }
        }

        return antinodes.Count;
    }

    public int Part2()
    {
        (List<(int x, int y, char frequency)> antennae, int sizeX, int sizeY) = GetInput();
        HashSet<(int x, int y)> antinodes = [];

        foreach (var antenna in antennae)
        {
            var otherAntennae = antennae.Except([antenna]).Where(a => a.frequency == antenna.frequency);

            foreach (var otherAntenna in otherAntennae)
            {
                (int xDiff, int yDiff) = (otherAntenna.x - antenna.x, otherAntenna.y - antenna.y);
                (int x, int y) newForwardAntinode = (otherAntenna.x, otherAntenna.y);
                (int x, int y) newBackwardsAntinode = (antenna.x, antenna.y);
                while (!IsOutOfBounds(newForwardAntinode.x, newForwardAntinode.y, sizeX, sizeY))
                {
                    antinodes.Add(newForwardAntinode);
                    newForwardAntinode = (newForwardAntinode.x + xDiff, newForwardAntinode.y + yDiff);
                }
                while (!IsOutOfBounds(newBackwardsAntinode.x, newBackwardsAntinode.y, sizeX, sizeY))
                {
                    antinodes.Add(newBackwardsAntinode);
                    newBackwardsAntinode = (newBackwardsAntinode.x - xDiff, newBackwardsAntinode.y - yDiff);
                }
            }
        }

        return antinodes.Count;
    }

    bool IsOutOfBounds(int x, int y, int sizeX, int sizeY)
    {
        return x < 0 || x >= sizeX || y < 0 || y >= sizeY;
    }

    private static (List<(int x, int y, char frequency)> antennae, int sizeX, int sizeY) GetInput()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day08\\input.txt")
            .Select(line => line.ToList())
            .ToList();

        List<(int x, int y, char frequency)> antennae = [];
        int sizeX = input.First().Count;
        int sizeY = input.Count;

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                char value = input[y][x];

                if (value != '.')
                {
                    antennae.Add((x, y, value));
                }
            }
        }

        return (antennae, sizeX, sizeY);
    }
}
