namespace AOC2024;

public class Day20
{
    readonly (int x, int y)[] Directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    public int Part1()
    {
        List<List<char>> maze = GetInput();

        // there is only one route from start to end
        var pathWithoutCheat = PathWithoutCheat(maze);

        return numberOfCheatPaths(maze, 100, pathWithoutCheat);
    }

    public void Part2()
    {

    }

    List<(int x, int y)> PathWithoutCheat(List<List<char>> maze)
    {
        int sizeX = maze.First().Count;
        int sizeY = maze.Count;
        (int x, int y) = CoordOf(maze, 'S');
        (int x, int y) endCoord = CoordOf(maze, 'E');

        List<List<(int x, int y)>> paths = [[(x, y)]];
        List<(int x, int y)> visited = [paths.First().First()];

        while (paths.Count > 0)
        {
            List<(int x, int y)> path = paths.First();
            paths.Remove(path);
            var lastInPath = path.Last();

            foreach ((int dirX, int dirY) in Directions)
            {
                (int x, int y) newCoord = (lastInPath.x + dirX, lastInPath.y + dirY);

                if (IsOutOfBounds(newCoord.x, newCoord.y, sizeX, sizeY)
                    || (maze[newCoord.y][newCoord.x] == '#')
                    || visited.Contains(newCoord))
                {
                    continue;
                }

                if (newCoord.x == endCoord.x && newCoord.y == endCoord.y)
                {
                    return path.Union([newCoord]).ToList();
                }

                visited.Add(newCoord);
                paths.Add(path.Union([newCoord]).ToList());
            }
        }

        return [];
    }

    int numberOfCheatPaths(List<List<char>> maze, int minimumSaved, List<(int x, int y)> path)
    {
        int sizeX = maze.First().Count;
        int sizeY = maze.Count;

        int cheatCount = 0;

        foreach (var move in path)
        {
            int moveIndex = path.IndexOf(move);

            foreach ((int cheat1dirX, int cheat1dirY) in Directions)
            {
                (int x, int y) cheat1 = (move.x + cheat1dirX, move.y + cheat1dirY);

                if (!IsOutOfBounds(cheat1.x, cheat1.y, sizeX, sizeY) && maze[cheat1.y][cheat1.x] == '#')
                {
                    foreach ((int cheat2dirX, int cheat2dirY) in Directions)
                    {
                        (int x, int y) cheat2 = (cheat1.x + cheat2dirX, cheat1.y + cheat2dirY);
                        if (!IsOutOfBounds(cheat2.x, cheat2.y, sizeX, sizeY) && maze[cheat2.y][cheat2.x] != '#')
                        {
                            int newIndex = path.IndexOf(cheat2);
                            if (newIndex - moveIndex > minimumSaved + 1)
                            {
                                cheatCount++;
                            }
                        }
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
        var coords = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day20\\input.txt")
            .Select(line => line.ToList())
            .ToList();

        return coords;
    }
}
