namespace AOC2024;

public class Day07
{
    public long Part1()
    {
        return GetInput().Sum(line => CouldBeTrue1(line) ? line.testValue : 0);
    }

    public long Part2()
    {
        return GetInput().Sum(line => CouldBeTrue2(line) ? line.testValue : 0);
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

    private bool CouldBeTrue2((long testValue, List<long> numbers) line)
    {
        long firstNumber = line.numbers.First();
        if (line.numbers.Count == 1)
        {
            return line.testValue == firstNumber;
        }
        long secondNumber = line.numbers[1];

        var added = new List<long> { firstNumber + secondNumber }.Concat(line.numbers[2..]).ToList();
        var multiplied = new List<long> { firstNumber * secondNumber }.Concat(line.numbers[2..]).ToList();
        var combined = new List<long> { long.Parse(firstNumber + secondNumber.ToString()) }.Concat(line.numbers[2..]).ToList();

        return (added.First() <= line.testValue && CouldBeTrue2((line.testValue, added)))
            || (multiplied.First() <= line.testValue && CouldBeTrue2((line.testValue, multiplied)))
            || (combined.First() <= line.testValue && CouldBeTrue2((line.testValue, combined)));
    }

    private static List<(long testValue, List<long> numbers)> GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day07\\input.txt")
            .Select(line => line.Split(": "))
            .Select(line => (long.Parse(line.First()), line.Last().Split(" ").Select(n => long.Parse(n)).ToList()))
            .ToList();
    }
}
