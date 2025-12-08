namespace AOC2025;

public class Day08
{
    public static long Part1()
    {
        var junctionBoxes = GetJunctionBoxes();
        var orderedBoxes = CalculateDistances(junctionBoxes).OrderBy(kv => kv.Value).Select(kv => kv.Key).ToList();
        List<HashSet<int[]>> circuits = [];

        for (var i = 0; i < 1000; i++)
        {
            var box1 = orderedBoxes[i][0];
            var box2 = orderedBoxes[i][1];

            var circuitWithBox1 = circuits.FirstOrDefault(circuit => circuit.Contains(box1));
            var circuitWithBox2 = circuits.FirstOrDefault(circuit => circuit.Contains(box2));

            if (circuitWithBox1 is not null)
            {
                if (circuitWithBox2 is not null)
                {
                    if (circuitWithBox1 == circuitWithBox2)
                    {
                        continue;
                    }
                    circuitWithBox1.UnionWith(circuitWithBox2);
                    circuits.Remove(circuitWithBox2);
                }
                else
                {
                    circuitWithBox1.Add(box2);
                }
            }
            else if (circuitWithBox2 is not null)
            {
                circuitWithBox2.Add(box1);
            }
            else
            {
                circuits.Add([box1, box2]);
            }
        }

        return circuits.OrderByDescending(c => c.Count).Select(c => c.Count).ToList()[..3].Aggregate(1, (a, b) => a * b);
    }

    public static long Part2()
    {
        return 0;
    }

    private static List<int[]> GetJunctionBoxes()
    {
        var junctionBoxes = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day08/input.txt")
            .Select(line => line.Split(',').Select(s => int.Parse(s)).ToArray()).ToList();

        return junctionBoxes;
    }

    private static Dictionary<int[][], long> CalculateDistances(List<int[]> junctionBoxes)
    {
        Dictionary<int[][], long> distances = [];
        List<int[]> seenBoxes = [];

        foreach (var junctionBox1 in junctionBoxes)
        {
            foreach (var junctionBox2 in junctionBoxes)
            {
                if (junctionBox1 == junctionBox2 || seenBoxes.Contains(junctionBox2))
                {
                    continue;
                }

                distances[[junctionBox1, junctionBox2]] =
                    Square(junctionBox1[0] - junctionBox2[0])
                    + Square(junctionBox1[1] - junctionBox2[1])
                    + Square(junctionBox1[2] - junctionBox2[2]);
            }
            seenBoxes.Add(junctionBox1);
        }

        return distances;
    }

    private static long Square(long num)
    {
        return num * num;
    }
}
