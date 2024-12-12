namespace Day1;

public class Day1 {
    public static int Part1() {
        StreamReader sr = new("C:\\Users\\sande\\Documents\\AdventOfCode\\AdventOfCode\\Day1\\input.txt");
        var line = sr.ReadLine();

        var numbersTeam1 = new List<int>();
        var numbersTeam2 = new List<int>();
        while (line != null)
        {
            var splitLine = line.Split("   ");
            numbersTeam1.Add(int.Parse(splitLine[0]));
            numbersTeam2.Add(int.Parse(splitLine[1]));

            line = sr.ReadLine();
        }
        sr.Close();

        var sortedNumbersTeam1 = numbersTeam1.OrderBy(num => num);
        var sortedNumbersTeam2 = numbersTeam2.OrderBy(num => num);

        var zippedNumbers = sortedNumbersTeam1.Zip(sortedNumbersTeam2);
        var distances = zippedNumbers.Select(zipped => Math.Abs(zipped.First - zipped.Second));

        return distances.Sum();
    }
}
