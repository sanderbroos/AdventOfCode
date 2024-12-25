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

    public int Part2()
    {
        return 0;
    }

    // private static List<Wire> GetWires()
    // {
    //     var gates = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day24\\inputGates.txt")
    //         .Select(line => line.Replace(" ->", "").Split(" "));

    //     Dictionary<string, Wire> wires = [];
    //     foreach (var gate in gates)
    //     {
    //         if (!wires.ContainsKey(gate[3]))
    //         {
    //             wires[gate[3]] = new Wire
    //             {
    //                 name = gate[3],

    //             }
    //         }
    //     }

    //     return gates;
    // }

    private static Dictionary<string, bool> GetWireDict()
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

    public class Wire
    {
        public string name;
        public bool? value = null;
        public string operation;
        public List<Wire> input;
        public List<Wire> output = [];
    }
}
