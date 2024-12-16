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

    public int? Part1()
    {
        List<List<char>> maze = GetInput();
        Coord reindeerStart = coordOf(maze, 'S');

        Path? shortestPathToEnd = getShortestPathToEnd(maze, new Path {
            coords = [ reindeerStart ],
            score = 0 });

        return shortestPathToEnd?.score;
    }

    public void Part2()
    {
        
    }

    Path? getShortestPathToEnd(List<List<char>> maze, Path pathSoFar) {
        Coord lastCoord = pathSoFar.coords.Last();
        Direction currentDirection = pathSoFar.coords.Count > 1 ? Directions.Single(d => 
            d.x == lastCoord.x - pathSoFar.coords[^2].x &&
            d.y == lastCoord.y - pathSoFar.coords[^2].y)
            : Directions.First();

        if (maze[lastCoord.y][lastCoord.x] == 'E') {
            return pathSoFar;
        }

        List<Path> possiblePaths = [];

        foreach (Direction direction in Directions) {
            Coord newCoord = new() { x = lastCoord.x + direction.x, y = lastCoord.y + direction.y};
            bool turn = direction != currentDirection;
            int newScore = pathSoFar.score + 1 + (turn ? 1000 : 0);

            if (newScore >= currentLowestScore
                || isOutOfBounds(newCoord, maze[0].Count, maze.Count)
                || maze[newCoord.y][newCoord.x] == '#'
                || pathSoFar.coords.Any(c => c.x == newCoord.x && c.y == newCoord.y)) {
                continue;
            }

            Path? shortestPathFromHere = getShortestPathToEnd(maze, new Path {
                coords = [.. pathSoFar.coords, newCoord],
                score = newScore,
            } );

            if (shortestPathFromHere?.score <= currentLowestScore) {
                possiblePaths.Add(shortestPathFromHere);
            }
        }

        if (possiblePaths.Count > 0) {
            return possiblePaths.OrderBy(p => p.score).First();
        }

        return null;
    }

    class Path {
        public List<Coord> coords;
        public int score;
    }

    class Coord {
        public int x;
        public int y;
    }

    class Direction { 
        public int x;
        public int y;
    }

    Coord coordOf(List<List<char>> maze, char item) {
        for (int y = 0; y < maze.Count; y++) {
            for (int x = 0; x < maze[y].Count; x++) {
                if (maze[y][x] == item) {
                    return new Coord {
                        x = x,
                        y = y
                    };
                }
            }
        }

        return null;
    }

    bool isOutOfBounds(Coord coord, int sizeX, int sizeY) {
        return coord.x < 0 || coord.x >= sizeX || coord.y < 0 || coord.y >= sizeY;
    }

    private static List<List<char>> GetInput()
    {
        var maze = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day16\\input.txt")
            .Select(line => line.ToList())
            .ToList();

        return maze;
    }
}
