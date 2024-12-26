namespace AOC2024;

public class Day07
{
    public long Part1()
    {
        return GetInput().Sum(line => CouldBeTrue1(line) ? line.testValue : 0);
    }

    public long Part2()
    {
        return 0;
    }

    private bool CouldBeTrue1((long testValue, List<long> numbers) line)
    {
        long lastNumber = line.numbers.Last();
        if (line.numbers.Count == 1)
        {
            return line.testValue == lastNumber;
        }

        return (line.testValue % lastNumber == 0 && CouldBeTrue1((line.testValue / lastNumber, line.numbers[..^1])))
            || (line.testValue - lastNumber > 0 && CouldBeTrue1((line.testValue - lastNumber, line.numbers[..^1])));
    }

    private static List<(long testValue, List<long> numbers)> GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day07\\smallInput.txt")
            .Select(line => line.Split(": "))
            .Select(line => (long.Parse(line.First()), line.Last().Split(" ").Select(n => long.Parse(n)).ToList()))
            .ToList();
    }
}
