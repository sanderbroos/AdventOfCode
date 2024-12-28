namespace AOC2024;

public class Day11
{
    public int Part1()
    {
        List<long> stones = GetInput();

        for (var i = 0; i < 25; i++)
        {
            stones = stones.SelectMany(stone => Blink(stone)).ToList();
        }

        return stones.Count;
    }

    public long Part2()
    {
        List<long> input = GetInput();
        Dictionary<long, long> counts = [];
        foreach (long stone in input)
        {
            counts[stone] = input.Count(s => s == stone);
        }

        for (var i = 0; i < 75; i++)
        {
            List<long> stones = [.. counts.Keys];

            Dictionary<long, long> newCounts = [];

            foreach (long stone in stones)
            {
                if (counts[stone] == 0)
                {
                    continue;
                }

                List<long> newStones = Blink(stone);

                foreach (long newStone in newStones)
                {
                    if (!newCounts.ContainsKey(newStone))
                    {
                        newCounts[newStone] = 0;
                    }
                    newCounts[newStone] += counts[stone];
                }

                counts[stone] = 0;
            }

            foreach (long stoneNumber in newCounts.Keys)
            {
                if (!counts.ContainsKey(stoneNumber))
                {
                    counts[stoneNumber] = 0;
                }
                counts[stoneNumber] += newCounts[stoneNumber];
            }
        }

        return counts.Values.Sum();
    }

    private List<long> Blink(long stone)
    {
        List<long> newStones = [];

        int numDigits = (int)Math.Log(stone, 10) + 1;

        if (stone == 0)
        {
            newStones.Add(1);
        }
        else if (numDigits % 2 == 0)
        {
            long leftPart = (long)(stone / Math.Pow(10, numDigits / 2));
            long rightPart = stone - leftPart * (long)Math.Pow(10, numDigits / 2);

            newStones.Add(leftPart);
            newStones.Add(rightPart);
        }
        else
        {
            newStones.Add(stone * 2024);
        }

        return newStones;
    }

    private List<long> GetInput()
    {
        return [8069, 87014, 98, 809367, 525, 0, 9494914, 5];
    }
}
