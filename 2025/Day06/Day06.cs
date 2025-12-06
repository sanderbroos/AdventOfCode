namespace AOC2025;

public class Day06
{
    public static long Part1()
    {
        var problems = GetProblems();
        long grandTotal = 0;

        for (var i = 0; i < problems[0].Length; i++)
        {
            var indexOperator = problems[4][i];
            long result = indexOperator == "+" ? 0 : 1;
            for (var j = 0; j < problems.Count - 1; j++)
            {
                if (indexOperator == "+")
                {
                    result += long.Parse(problems[j][i]);
                }
                else
                {
                    result *= long.Parse(problems[j][i]);
                }
            }
            grandTotal += result;
        }

        return grandTotal;
    }

    public static long Part2()
    {
        return 0;
    }

    private static List<string[]> GetProblems()
    {
        var problems = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day06/input.txt")
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();

        return problems;
    }
}
