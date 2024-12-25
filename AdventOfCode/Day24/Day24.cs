using System.Diagnostics;

namespace AOC2024;

public class Day24
{
    public long Part1()
    {
        var wires = GetWires();
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
            .Select(w => (w.Value ? 1 : 0).ToString()));

        return Convert.ToInt64(binaryNumber, 2);
    }

    public int Part2()
    {
        return 0;
    }

    private static Dictionary<string, bool> GetWires()
    {
        var initialWires = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day24\\inputWires.txt")
            .Select(line => line.Split(": "))
            .ToDictionary(line => line[0], line => int.Parse(line[1]) == 1);

        return initialWires;
    }

    private static List<string[]> GetGates()
    {
        var gates = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day24\\inputGates.txt")
            .Select(line => line.Replace(" ->", "").Split(" "))
            .ToList();

        return gates;
    }
}
