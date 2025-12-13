namespace AOC2025;

public class Day10
{
    public static long Part1()
    {
        var machines = GetInput();
        long sum = 0;

        foreach (var machine in machines)
        {
            sum += LeastPresses(machine.lights, machine.buttons);
        }

        return sum;
    }

    public static long Part2()
    {
        return 0;
    }

    public static long LeastPresses(bool[] lightDiagram, int[][] buttons)
    {
        var lightsQueue = new Queue<(int pressCount, bool[] lights)>([(0, new bool[lightDiagram.Length])]);

        while (lightsQueue.Count > 0)
        {
            var lights = lightsQueue.Dequeue();
            foreach (var button in buttons)
            {
                bool[] newLights = [.. lights.lights.Select((b, i) => button.Contains(i) ? !b : b)];
                if (lightDiagram.SequenceEqual(newLights))
                {
                    return lights.pressCount + 1;
                }
                lightsQueue.Enqueue((lights.pressCount + 1, newLights));
            }
        }

        return 0;
    }

    private static List<Machine> GetInput()
    {
        var input = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day10/input.txt")
            .Select(l => l.Split(" ").ToArray())
            .Select(l => new Machine
            {
                lights = [.. l[0][1..^1].Select(c => c == '#')],
                buttons = [.. l[1..^1].Select(a => a[1..^1].Split(',').Select(s => int.Parse(s)).ToArray())],
                joltages = [.. l[^1][1..^1].Split(',').Select(s => int.Parse(s))]
            }).ToList();

        return input;
    }

    public class Machine
    {
        public bool[] lights;
        public int[][] buttons;
        public int[] joltages;
    }
}
