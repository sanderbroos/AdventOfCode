using System.Text.RegularExpressions;

namespace AOC2024;

public class Day04
{
    public int Part1()
    {
        List<List<char>> letters = GetInput();

        return VerticalOccurrences(letters) + HorizontalOccurrences(letters) + DiagonalOccurrences(letters);
    }

    public int Part2()
    {
        List<List<char>> letters = GetInput();

        return CrossingMASses(letters);
    }

    int VerticalOccurrences(List<List<char>> letters)
    {
        int count = 0;

        for (int x = 0; x < letters.First().Count; x++)
        {
            string stringToSearch = "";

            for (int y = 0; y < letters.Count; y++)
            {
                stringToSearch += letters[y][x];
            }

            count += xmasCount(stringToSearch);
        }

        return count;
    }

    int HorizontalOccurrences(List<List<char>> letters)
    {
        int count = 0;

        foreach (List<char> row in letters)
        {
            string stringToSearch = string.Join("", row);
            count += xmasCount(stringToSearch);
        }

        return count;
    }

    int DiagonalOccurrences(List<List<char>> letters)
    {
        int count = 0;

        for (int x = 0; x < letters.First().Count; x++)
        {
            for (int y = 0; y < letters.Count; y++)
            {
                if (x == 0 || y == 0)
                {
                    string stringToSearch = "";

                    int xToSearch = x;
                    int yToSearch = y;

                    while (xToSearch < letters.First().Count && yToSearch < letters.Count())
                    {
                        stringToSearch += letters[yToSearch][xToSearch];

                        xToSearch++;
                        yToSearch++;
                    }

                    count += xmasCount(stringToSearch);
                }
                if (x == letters.First().Count - 1 || y == 0)
                {
                    string stringToSearch = "";

                    int xToSearch = x;
                    int yToSearch = y;

                    while (xToSearch >= 0 && yToSearch < letters.Count())
                    {
                        stringToSearch += letters[yToSearch][xToSearch];

                        xToSearch--;
                        yToSearch++;
                    }

                    count += xmasCount(stringToSearch);
                }
            }
        }

        return count;
    }

    int CrossingMASses(List<List<char>> letters)
    {
        List<(int x, int y)> masMiddles = [];

        for (int x = 0; x < letters.First().Count; x++)
        {
            for (int y = 0; y < letters.Count; y++)
            {
                if (x == 0 || y == 0)
                {
                    string stringToSearch = "";

                    int xToSearch = x;
                    int yToSearch = y;

                    while (xToSearch < letters.First().Count && yToSearch < letters.Count())
                    {
                        stringToSearch += letters[yToSearch][xToSearch];

                        xToSearch++;
                        yToSearch++;
                    }

                    masMiddles.AddRange(Regex.Matches(stringToSearch, "MAS").Union(Regex.Matches(stringToSearch, "SAM"))
                        .Select(m => (x + m.Index + 1, y + m.Index + 1)));
                }
                if (x == letters.First().Count - 1 || y == 0)
                {
                    string stringToSearch = "";

                    int xToSearch = x;
                    int yToSearch = y;

                    while (xToSearch >= 0 && yToSearch < letters.Count())
                    {
                        stringToSearch += letters[yToSearch][xToSearch];

                        xToSearch--;
                        yToSearch++;
                    }

                    masMiddles.AddRange(Regex.Matches(stringToSearch, "MAS").Union(Regex.Matches(stringToSearch, "SAM"))
                        .Select(m => (x - m.Index - 1, y + m.Index + 1)));
                }
            }
        }

        return masMiddles.Count(m => masMiddles.Count(m2 => m2 == m) == 2) / 2;
    }

    int xmasCount(string stringToCount)
    {
        return Regex.Matches(stringToCount, "XMAS").Count + Regex.Matches(stringToCount, "SAMX").Count;
    }

    private List<List<char>> GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day04\\input.txt")
            .Select(line => line.ToList())
            .ToList();
    }
}
