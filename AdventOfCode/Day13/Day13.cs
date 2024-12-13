namespace AOC2024;

public class Day13 {
    public static double Part1() {
        var clawMachines = GetInput();

        return clawMachines.Sum(cm => Check(cm));
    }

    public static int Part2() {
        return 0;
    }

    public class ClawMachine {
        public float ButtonA_X;
        public float ButtonA_Y;
        public float ButtonB_X;
        public float ButtonB_Y;
        public float PrizeX;
        public float PrizeY;
    }

    private static double Check(ClawMachine cm) {
        var buttonAPresses = Math.Round((cm.PrizeY * cm.ButtonB_X - cm.PrizeX * cm.ButtonB_Y) / (cm.ButtonB_X * cm.ButtonA_Y - cm.ButtonA_X * cm.ButtonB_Y), 3);
        var buttonBPresses = Math.Round((cm.PrizeY * cm.ButtonA_X - cm.PrizeX * cm.ButtonA_Y) / (cm.ButtonA_X * cm.ButtonB_Y - cm.ButtonB_X * cm.ButtonA_Y), 3);

        if (buttonAPresses >= 0 && buttonBPresses >= 0 && buttonAPresses == Math.Round(buttonAPresses) && buttonBPresses == Math.Round(buttonBPresses)) {
            return 3 * Math.Round(buttonAPresses) + 1 * Math.Round(buttonBPresses);
        }

        return 0;
    }

    private static IEnumerable<ClawMachine> GetInput() {
        var clawMachines = File.ReadAllText("..\\..\\..\\..\\AdventOfCode\\Day13\\input.txt")
            .Split("Button A: X+")
            .Skip(1)
            .Select(rec => rec.Replace("\n", "")
                .Split([", Y+", "Button B: X+", "Prize: X=", ", Y="], StringSplitOptions.None)
                .Select(num => (float)int.Parse(num))
                .ToList())
            .Select(rec => new ClawMachine {
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
