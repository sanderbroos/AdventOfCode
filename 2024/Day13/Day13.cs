namespace AOC2024;

public class Day13
{
    public static double Part1()
    {
        var clawMachines = GetInput();

        return clawMachines.Sum(cm => Check(cm));
    }

    public static double Part2()
    {
        var clawMachines = GetInput()
            .Select(cm => new ClawMachine
            {
                ButtonA_X = cm.ButtonA_X,
                ButtonA_Y = cm.ButtonA_Y,
                ButtonB_X = cm.ButtonB_X,
                ButtonB_Y = cm.ButtonB_Y,
                PrizeX = cm.PrizeX + 10000000000000,
                PrizeY = cm.PrizeY + 10000000000000,
            });

        return clawMachines.Sum(cm => Check(cm));
    }

    public class ClawMachine
    {
        public long ButtonA_X;
        public long ButtonA_Y;
        public long ButtonB_X;
        public long ButtonB_Y;
        public long PrizeX;
        public long PrizeY;
    }

    private static double Check(ClawMachine cm)
    {
        double buttonANumerator = cm.PrizeY * cm.ButtonB_X - cm.PrizeX * cm.ButtonB_Y;
        double buttonADenominator = cm.ButtonB_X * cm.ButtonA_Y - cm.ButtonA_X * cm.ButtonB_Y;

        double buttonBNumerator = cm.PrizeY * cm.ButtonA_X - cm.PrizeX * cm.ButtonA_Y;
        double buttonBDenominator = cm.ButtonA_X * cm.ButtonB_Y - cm.ButtonB_X * cm.ButtonA_Y;

        double buttonAPresses = (double)buttonANumerator / buttonADenominator;
        double buttonBPresses = (double)buttonBNumerator / buttonBDenominator;

        if (buttonAPresses >= 0 && buttonBPresses >= 0 && buttonANumerator % buttonADenominator == 0 && buttonBNumerator % buttonBDenominator == 0)
        {
            return 3 * Math.Round(buttonAPresses) + 1 * Math.Round(buttonBPresses);
        }

        return 0;
    }

    private static IEnumerable<ClawMachine> GetInput()
    {
        var clawMachines = File.ReadAllText("..\\..\\..\\..\\AdventOfCode\\2024\\Day13\\input.txt")
            .Split("Button A: X+")
            .Skip(1)
            .Select(rec => rec.Replace("\n", "")
                .Split([", Y+", "Button B: X+", "Prize: X=", ", Y="], StringSplitOptions.None)
                .Select(num => (long)int.Parse(num))
                .ToList())
            .Select(rec => new ClawMachine
            {
                ButtonA_X = rec[0],
                ButtonA_Y = rec[1],
                ButtonB_X = rec[2],
                ButtonB_Y = rec[3],
                PrizeX = rec[4],
                PrizeY = rec[5]
            });

        return clawMachines;
    }
}
