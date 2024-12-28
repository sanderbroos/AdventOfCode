namespace AOC2024;

public class Day20
{
    readonly (int x, int y)[] Directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    public int Part1()
    {
        List<List<char>> racetrack = GetInput();

        var pathWithoutCheat = PathWithoutCheat(racetrack);

        return NumberOfCheatPaths(racetrack, pathWithoutCheat, 100, 2);
    }

    public int Part2()
    {
        List<List<char>> racetrack = GetInput();

        var pathWithoutCheat = PathWithoutCheat(racetrack);

        return NumberOfCheatPaths(racetrack, pathWithoutCheat, 100, 20);
    }

    List<(int x, int y)> PathWithoutCheat(List<List<char>> racetrack)
    {
        int sizeX = racetrack.First().Count;
        int sizeY = racetrack.Count;
        var endCoord = CoordOf(racetrack, 'E');
        List<(int x, int y)> path = [CoordOf(racetrack, 'S')];
        var pathHasNotEnded = true;

        while (pathHasNotEnded)
        {
            var (lastX, lastY) = path.Last();
            pathHasNotEnded = false;

            foreach ((int dirX, int dirY) in Directions)
            {
                (int x, int y) newCoord = (lastX + dirX, lastY + dirY);

                if (IsOutOfBounds(newCoord.x, newCoord.y, sizeX, sizeY)
                    || (racetrack[newCoord.y][newCoord.x] == '#')
                    || path.Contains(newCoord))
                {
                    continue;
                }

                path.Add(newCoord);
                pathHasNotEnded = true;

                if (newCoord == endCoord)
                {
                    return path;
                }

                break;
            }
        }

        return [];
    }

    int NumberOfCheatPaths(List<List<char>> maze, List<(int x, int y)> path, int minimumSaved, int maxCheatLength)
    {
        int sizeX = maze.First().Count;
        int sizeY = maze.Count;

        int cheatCount = 0;

        foreach (var move in path)
        {
            int moveIndex = path.IndexOf(move);

            for (int newX = move.x - maxCheatLength; newX <= move.x + maxCheatLength; newX++)
            {
                for (int newY = move.y - maxCheatLength; newY <= move.y + maxCheatLength; newY++)
                {
                    int cheatLength = Math.Abs(newX - move.x) + Math.Abs(newY - move.y);
                    if (IsOutOfBounds(newX, newY, sizeX, sizeY)
                        || cheatLength > maxCheatLength
                        || maze[newY][newX] == '#')
                    {
                        continue;
                    }

                    (int x, int y) newCoord = (newX, newY);
                    int newIndex = path.IndexOf(newCoord);
                    if (newIndex - moveIndex >= minimumSaved + cheatLength)
                    {
                        cheatCount++;
                    }
                }
            }
        }

        return cheatCount;
    }

    bool IsOutOfBounds(int x, int y, int sizeX, int sizeY)
    {
        return x < 0 || x >= sizeX || y < 0 || y >= sizeY;
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

    private static List<List<char>> GetInput()
    {
        var coords = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day20\\input.txt")
            .Select(line => line.ToList())
            .ToList();

        return coords;
    }
}
