namespace AOC2024;

public class Day06
{
    public int? Part1()
    {
        List<List<char>>? lab = LabWithTrail(GetInput());

        return lab?.Sum(row => row.Count(spot => spot == 'X'));
    }

    public int Part2()
    {
        List<List<char>> initialLab = GetInput();
        List<List<char>>? labWithTrail = LabWithTrail(initialLab);
        int obstructionCount = 0;
        for (int y = 0; y < labWithTrail?.Count; y++)
        {
            for (int x = 0; x < labWithTrail.First().Count; x++)
            {
                if (labWithTrail[y][x] == 'X' && LabWithTrail(initialLab, obstruction: (x, y)) == null)
                {
                    obstructionCount++;
                }
            }
        }
        return obstructionCount;
    }

    public List<List<char>>? LabWithTrail(List<List<char>> initialLab, (int x, int y)? obstruction = null)
    {
        List<List<char>> lab = initialLab.Select(row => row.ToList()).ToList();
        if (obstruction.HasValue)
        {
            lab[obstruction.Value.y][obstruction.Value.x] = '#';
        }

        (int guardX, int guardY) = CoordOf(lab, '^');
        (int dirX, int dirY) = (0, -1);
        (int sizeX, int sizeY) = (lab.First().Count, lab.Count);
        HashSet<(int guardX, int guardY, int dirX, int dirY)> visited = [];

        while (!IsOutOfBounds(guardX, guardY, sizeX, sizeY))
        {
            if (!visited.Add((guardX, guardY, dirX, dirY)))
            {
                return null;
            }
            lab[guardY][guardX] = 'X';
            (int newX, int newY) = (guardX + dirX, guardY + dirY);
            while (!IsOutOfBounds(newX, newY, sizeX, sizeY) && lab[newY][newX] == '#')
            {
                (dirX, dirY) = (-dirY, dirX);
                (newX, newY) = (guardX + dirX, guardY + dirY);
            }
            (guardX, guardY) = (newX, newY);

        }

        return lab;
    }

    (int x, int y) CoordOf(List<List<char>> maze, char item)
    {
        for (int y = 0; y < maze.Count; y++)
        {
            for (int x = 0; x < maze[y].Count; x++)
            {
                if (maze[y][x] == item)
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }

    bool IsOutOfBounds(int x, int y, int sizeX, int sizeY)
    {
        return x < 0 || x >= sizeX || y < 0 || y >= sizeY;
    }

    private static List<List<char>> GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day06\\input.txt")
            .Select(line => line.ToList())
            .ToList();
    }
}
