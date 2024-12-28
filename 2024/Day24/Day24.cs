namespace AOC2024;

public class Day24
{
    public long Part1()
    {
        var wires = GetWireDict();
        var gates = GetGates();

        while (gates.Count > 0)
        {
            for (int i = gates.Count - 1; i >= 0; i--)
            {
                var gate = gates[i];

                if (wires.ContainsKey(gate[0]) && wires.ContainsKey(gate[2]))
                {
                    wires[gate[3]] =
                        gate[1] == "AND" ? wires[gate[0]] && wires[gate[2]] :
                        gate[1] == "OR" ? wires[gate[0]] || wires[gate[2]] :
                        gate[1] == "XOR" ? wires[gate[0]] ^ wires[gate[2]] : false;

                    gates.RemoveAt(i);
                }
            }
        }

        var binaryNumber = string.Join("", wires.Where(w => w.Key.StartsWith('z'))
            .OrderByDescending(w => w.Key)
            .Select(w => w.Value ? 1 : 0));

        return Convert.ToInt64(binaryNumber, 2);
    }

    public string Part2()
    {
        var wires = GetWireDict();
        var gates = GetGates();
        List<(string, string)> swaps = [("fdw", "z35"), ("hcc", "z11"), ("bpf", "z05"), ("hqc", "qcw")];

        // foreach (var swap in swaps)
        // {
        //     var first = gates.Single(g => g[3] == swap.Item1);
        //     var second = gates.Single(g => g[3] == swap.Item2);
        //     first[3] = swap.Item2;
        //     second[3] = swap.Item1;
        // }

        var originalGates = gates.Select(g => g.ToArray()).ToList();

        Dictionary<string, int> outputLevels = [];
        foreach (var wire in wires.Keys)
        {
            outputLevels[wire] = 0;
        }

        while (gates.Count > 0)
        {
            for (int i = gates.Count - 1; i >= 0; i--)
            {
                var gate = gates[i];
                if (outputLevels.ContainsKey(gate[0]) && outputLevels.ContainsKey(gate[2]))
                {
                    outputLevels[gate[3]] = Math.Max(outputLevels[gate[0]], outputLevels[gate[2]]) + 1;
                    gates.Remove(gate);
                }
            }
        }

        foreach (var gate in originalGates)
        {
            var orderedInputs = new List<string> { gate[0], gate[2] }.OrderBy(s => outputLevels[s]).ThenBy(s => s);
            gate[0] = orderedInputs.First();
            gate[2] = orderedInputs.Last();
        }

        foreach (var level in outputLevels.Values.Distinct().OrderBy(v => v))
        {
            Console.WriteLine(
                level.ToString()
                + ": "
                + string.Join("  ", outputLevels
                                        .Where(kv => kv.Value == level)
                                        .OrderBy(kv => kv.Key)
                                        .Select(kv => '[' + string.Join(' ', (originalGates.SingleOrDefault(g => g[3] == kv.Key) ?? [kv.Key]).Select(s => (s + (outputLevels.ContainsKey(s) ? ":" + originalGates.SingleOrDefault(g => g[3] == s)?.ElementAt(1) : "")).PadRight(6))) + ']')));
        }

        return string.Join(',', swaps.SelectMany(s => new List<string> { s.Item1, s.Item2 }).OrderBy(s => s));
    }

    private static Dictionary<string, bool> GetWireDict()
    {
        var initialWires = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day24\\inputWires.txt")
            .Select(line => line.Split(": "))
            .ToDictionary(line => line[0], line => int.Parse(line[1]) == 1);

        return initialWires;
    }

    private static List<string[]> GetGates()
    {
        var gates = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day24\\inputGates.txt")
            .Select(line => line.Replace(" ->", "").Split(" "))
            .ToList();

        return gates;
    }
}
