namespace AOC2024;

public class Day16
{
    readonly Direction[] Directions = [
        new() { x = 1, y = 0},
        new() { x = -1, y = 0},
        new() { x = 0, y = 1},
        new() { x = 0, y = -1}
    ];

    int currentLowestScore = int.MaxValue;
    List<List<char>> maze;
    Coord mazeEnd;
    int sizeX;
    int sizeY;
    HashSet<(Coord, Direction)> seen = [];

    public int? Part1()
    {
        maze = GetInput();
        mazeEnd = coordOf('E');
        sizeX = maze.First().Count;
        sizeY = maze.Count;
        Coord reindeerStart = coordOf('S');
        seen.Add((reindeerStart, Directions.First()));

        List<Path> pathsToTry = [new Path {
            newCoord = reindeerStart,
            score = 0,
            currentDirection = Directions.First() }];

        List<Path> pathsToEnd = [];
        int i = 0;

        while (pathsToTry.Count > 0)
        {
            Path pathToTry = pathsToTry.First();
            pathsToTry.RemoveAt(0);

            i++;
            if (i % 500000 == 0)
            {
                Console.WriteLine(i.ToString() + " " + pathsToTry.Count.ToString());
            }

            if (pathToTry.score >= currentLowestScore)
            {
                pathsToTry.Remove(pathToTry);
                continue;
            }

            IEnumerable<Path> newPaths = getBranchingPaths(maze, pathToTry);

            foreach (Path newPath in newPaths)
            {
                if (seen.Any(s => s.Item1.Equals(newPath.newCoord) && s.Item2 == newPath.currentDirection))
                {
                    continue;
                }

                if (newPath.newCoord.Equals(mazeEnd))
                {
                    pathsToEnd.Add(newPath);
                    if (newPath.score < currentLowestScore)
                    {
                        currentLowestScore = newPath.score;
                    }
                }

                seen.Add((newPath.newCoord, newPath.currentDirection));
                pathsToTry.Add(newPath);

                // bool added = false;
                // for (int index = 0; index < pathsToTry.Count; index++) {
                //     if (pathsToTry[index].score >= newPath.score) {
                //         pathsToTry.Insert(index, newPath);
                //         added = true;
                //         break;
                //     }
                // }
                // if (!added) {
                //     pathsToTry.Add(newPath);
                // }
            }
        }

        return pathsToEnd.OrderBy(path => path.score).First().score;
    }

    public void Part2()
    {

    }

    IEnumerable<Path> getBranchingPaths(List<List<char>> maze, Path pathSoFar)
    {
        foreach (Direction newDirection in Directions)
        {
            Coord newCoord = new() { x = pathSoFar.newCoord.x + newDirection.x, y = pathSoFar.newCoord.y + newDirection.y };
            bool turn = newDirection != pathSoFar.currentDirection;
            int newScore = pathSoFar.score + 1 + (turn ? 1000 : 0);

            if (newScore >= currentLowestScore
                || isOutOfBounds(newCoord)
                || maze[newCoord.y][newCoord.x] == '#'
                || pathSoFar.CoordInPathSoFar(newCoord))
            {
                continue;
            }

            yield return new Path
            {
                pathSoFar = pathSoFar,
                newCoord = newCoord,
                score = newScore,
                currentDirection = newDirection
            };
        }
    }

    class Path
    {
        public Path? pathSoFar;
        public Coord newCoord;
        public int score;
        public Direction currentDirection;

        public bool CoordInPathSoFar(Coord coord)
        {
            return pathSoFar == null ? false : pathSoFar.newCoord.Equals(coord) || pathSoFar.CoordInPathSoFar(coord);
        }
    }

    class Coord
    {
        public int x;
        public int y;

        public bool Equals(Coord coord)
        {
            return coord.x == x && coord.y == y;
        }
    }

    class Direction
    {
        public int x;
        public int y;
    }

    Coord coordOf(char item)
    {
        for (int y = 0; y < maze.Count; y++)
        {
            for (int x = 0; x < maze[y].Count; x++)
            {
                if (maze[y][x] == item)
                {
                    return new Coord
                    {
                        x = x,
                        y = y
                    };
                }
            }
        }

        return null;
    }

    bool isOutOfBounds(Coord coord)
    {
        return coord.x < 0 || coord.x >= sizeX || coord.y < 0 || coord.y >= sizeY;
    }

    private static List<List<char>> GetInput()
    {
        var maze = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day16\\input.txt")
            .Select(line => line.ToList())
            .ToList();

        return maze;
    }
}
