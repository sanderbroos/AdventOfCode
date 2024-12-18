using System.Text.RegularExpressions;

namespace AOC2024;

public class Day03
{
    public int Part1()
    {
        string input = GetInput();

        var matches = Regex.Matches(input, @"mul\(([0-9]+)*,([0-9]+)*\)");

        return matches.Select(match => match.ToString().Replace("mul(", "").Replace(")", "").Split(",").Select(s => int.Parse(s)))
            .Select(nums => nums.First() * nums.Last())
            .Sum();
    }

    public int Part2()
    {
        string input = GetInput();

        string newInput = string.Empty;
        bool active = true;
        for (int i = 0; i < input.Length; i++)
        {
            if (i == input.IndexOf("do()", i))
            {
                active = true;
            }
            else if (i == input.IndexOf("don't()", i))
            {
                active = false;
            }

            if (active)
            {
                newInput += input[i];
            }
        }

        var matches = Regex.Matches(newInput, @"mul\(([0-9]+)*,([0-9]+)*\)");

        return matches.Select(match => match.ToString().Replace("mul(", "").Replace(")", "").Split(",").Select(s => int.Parse(s)))
            .Select(nums => nums.First() * nums.Last())
            .Sum();
    }

    private string GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day03\\input.txt")[0];
    }
}
