namespace AOC2024;

public class Day22
{
    public long Part1()
    {
        List<long> initialSecrets = GetInput();
        long sum = 0;

        foreach (long initialSecret in initialSecrets)
        {
            long secret = initialSecret;
            for (int i = 0; i < 2000; i++)
            {
                secret = ((secret * 64) ^ secret) % 16777216;
                secret = ((secret / 32) ^ secret) % 16777216;
                secret = ((secret * 2048) ^ secret) % 16777216;
            }
            sum += secret;
        }

        return sum;
    }

    public long Part2()
    {
        return 0;
    }

    private static List<long> GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day22\\input.txt")
            .Select(long.Parse)
            .ToList();
    }
}
