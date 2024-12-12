namespace Day2;

public class Day2 {
    public static int Part1() {
        var input = File.ReadAllLines("C:\\Users\\sande\\Documents\\AdventOfCode\\AdventOfCode\\Day2\\input.txt")
            .Select(line => line.Split(" ").Select(item => int.Parse(item)).ToList());

        var validLineCount = 0;
        foreach (var line in input) {
            bool lineValid = true;
            var firstDifference = line[1]-line[0];

            for (int i = 1; i < line.Count; i++) {
                var differenceWithPrevious = line[i] - line[i-1];
                if (differenceWithPrevious*firstDifference < 0
                    || Math.Abs(differenceWithPrevious) < 1
                    || Math.Abs(differenceWithPrevious) > 3) {
                    lineValid = false;
                    break;
                }
            }

            validLineCount += lineValid ? 1 : 0;
        }

        return validLineCount;
        
    }

    public static int Part2() {
        return 0;
    }
}
