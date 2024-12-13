namespace AOC2024;

public class Day13 {
    public static int Part1() {
        var clawMachines = GetInput();

        return 0;
        // return clawMachines;
    }

    public static int Part2() {
        return 0;
    }

    public class ClawMachine {
        public int ButtonA_X;
        public int ButtonA_Y;
        public int ButtonB_X;
        public int ButtonB_Y;
        public int PrizeX;
        public int PrizeY;
    }

    private static IEnumerable<ClawMachine> GetInput() {
        var clawMachines = File.ReadAllText("C:\\Users\\sande\\Documents\\AdventOfCode\\AdventOfCode\\Day13\\input.txt")
            .Split("Button A: X+")
            .Skip(1)
            .Select(rec => rec.Replace("\n", "")
                .Split(new string[] { ", Y+", "Button B: X+", "Prize: X=", ", Y=" }, StringSplitOptions.None)
                .Select(num => int.Parse(num))
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
