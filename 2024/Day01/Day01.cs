namespace AOC2024;

public class Day01
{
    public static int Part1()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day01\\input.txt")
            .Select(line => line.Split("   "));

        var sortedNumbersTeam1 = input.Select(line => int.Parse(line[0])).OrderBy(num => num);
        var sortedNumbersTeam2 = input.Select(line => int.Parse(line[1])).OrderBy(num => num);

        var zippedNumbers = sortedNumbersTeam1.Zip(sortedNumbersTeam2);
        var distances = zippedNumbers.Select(zipped => Math.Abs(zipped.First - zipped.Second));

        return distances.Sum();
    }

    public static int Part2()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day01\\input.txt")
            .Select(line => line.Split("   "));

        var sortedNumbersTeam1 = input.Select(line => int.Parse(line[0]));
        var sortedNumbersTeam2 = input.Select(line => int.Parse(line[1]));

        return sortedNumbersTeam1.Sum(num1 => num1 * sortedNumbersTeam2.Where(num2 => num2 == num1).Count());
    }
}
