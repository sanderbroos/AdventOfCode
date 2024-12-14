namespace AOC2024;

public class Day02 {
    public static int Part1() {
        var input = GetInput();

        return input.Count(report => IsReportSafe(report));
    }

    public static int Part2() {
        var input = GetInput();

        return input.Count(report => GetAllVariationsOfReportToConsider(report).Any(variation => IsReportSafe(variation)));
    }

    private static List<List<int>> GetInput() {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day02\\input.txt")
            .Select(line => line.Split(" ").Select(item => int.Parse(item)).ToList())
            .ToList();
    }

    private static bool IsReportSafe(List<int> report) {
        bool reportIsSafe = true;
        var firstDifference = report[1]-report[0];

        for (int i = 1; i < report.Count; i++) {
            var differenceWithPrevious = report[i] - report[i-1];
            if (differenceWithPrevious*firstDifference < 0
                || Math.Abs(differenceWithPrevious) < 1
                || Math.Abs(differenceWithPrevious) > 3) {
                reportIsSafe = false;
                break;
            }
        }

        return reportIsSafe;
    }

    private static List<List<int>> GetAllVariationsOfReportToConsider(List<int> report) {
        var variations = new List<List<int>>();

        for (int i = 0; i < report.Count; i++) {
            variations.Add([.. report]);
            variations.Last().RemoveAt(i);
        }

        return variations;
    }
}
