using System.Diagnostics;

namespace AOC2024;

public class Day23
{
    public static int Part1()
    {
        var computers = GetInput().Values;
        HashSet<HashSet<Computer>> interconnectedComputers = [];

        foreach (Computer computer in computers)
        {
            if (!computer.name.StartsWith('t'))
            {
                continue;
            }

            foreach (Computer connection in computer.connections)
            {
                foreach (Computer secondConnection in connection.connections)
                {
                    foreach (Computer thirdConnection in secondConnection.connections)
                    {
                        HashSet<Computer> interconnection = [computer, connection, secondConnection];
                        if (thirdConnection == computer && !interconnectedComputers.Any(hs => hs.SetEquals(interconnection)))
                        {
                            interconnectedComputers.Add(interconnection);
                        }
                    }
                }
            }
        }

        return interconnectedComputers.Count;
    }

    public string Part2()
    {
        var computers = GetInput().Values.ToList();
        HashSet<HashSet<Computer>> connectedGroups = [];

        foreach (Computer computer in computers)
        {
            foreach (Computer connection in computer.connections)
            {
                HashSet<Computer> initialGroup = [computer, connection];
                if (!connectedGroups.Any(hs => hs.SetEquals(initialGroup)))
                {
                    connectedGroups.Add(initialGroup);
                }
            }

            HashSet<HashSet<Computer>> newGroups = [];
            foreach (HashSet<Computer> group in connectedGroups)
            {
                if (group.All(c => c.connections.Contains(computer)))
                {
                    HashSet<Computer> newGroup = [.. group.Union([computer])];
                    if (!connectedGroups.Any(hs => hs.SetEquals(newGroup)))
                    {
                        newGroups.Add(newGroup);
                    }
                }
            }
            connectedGroups.UnionWith(newGroups);
        }

        return string.Join(',', connectedGroups.MaxBy(cg => cg.Count)!.OrderBy(c => c.name).Select(c => c.name));
    }

    private static Dictionary<string, Computer> GetInput()
    {
        var connections = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day23\\input.txt")
            .Select(line => line.Split('-'));
        Dictionary<string, Computer> computers = [];

        foreach (string[] connection in connections)
        {
            string name1 = connection[0];
            string name2 = connection[1];

            if (!computers.ContainsKey(name1))
            {
                computers[name1] = new Computer
                {
                    name = name1,
                };
            }
            if (!computers.ContainsKey(name2))
            {
                computers[name2] = new Computer
                {
                    name = name2,
                };
            }

            computers[name1].connections.Add(computers[name2]);
            computers[name2].connections.Add(computers[name1]);
        }

        return computers;
    }

    [DebuggerDisplay("{name}")]
    public class Computer
    {
        public string name = "";
        public HashSet<Computer> connections = [];
    }
}
