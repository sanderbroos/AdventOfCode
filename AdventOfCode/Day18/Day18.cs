namespace AOC2024;

public class Day18
{
    readonly (int x, int y)[] Directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    public int Part1(int upTo = 1024)
    {
        List<(int x, int y)> fallenBytes = GetInput()[.. upTo ];
        int endX = 70;
        int endY = 70;

        List<List<(int x, int y)>> paths = [ [ (0, 0) ] ];
        List<(int x, int y)> visited = [ (0, 0)];

        while (paths.Count > 0) {
            List<(int x, int y)> path = paths.First();
            paths.Remove(path);
            foreach ((int dirX, int dirY) in Directions) {
                (int X, int Y) newCoord = (path.Last().x + dirX, path.Last().y + dirY);

                if (isOutOfBounds(newCoord.X, newCoord.Y, endX, endY) || fallenBytes.Contains(newCoord) || visited.Contains(newCoord)) {
                    continue;
                }

                if (newCoord.X == endX && newCoord.Y == endY) {
                    return path.Count;
                }
                visited.Add(newCoord);
                paths.Add(path.Union([newCoord]).ToList());
            }
        }

        return 0;
    }

    public string? Part2()
    {
        var input = GetInput();
        for (int i = 1; i <= input.Count; i++) {
            if (Part1(i) == 0) {
                return input[i-1].ToString();
            }
            Console.WriteLine(i.ToString());// + " " + Part1(i).ToString());
        }

        return null;
    }

    bool isOutOfBounds(int x, int y, int sizeX, int sizeY) {
        return x < 0 || x > sizeX || y < 0 || y > sizeY;
    }

    private static List<(int x, int y)> GetInput()
    {
        var coords = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day18\\input.txt")
            .Select(line => line.Split(",").Select(int.Parse).ToList())
            .Select(line => ( line[0], line[1] ))
            .ToList();

        return coords;
        // bool[,] space = new bool[70, 70];

        // for (int i = 0; i < upTo; i++) {
        //     List<int> coord = coords[i];

        //     space[coord[1], coord[0]] = true;
        // }

        // return space;
    }

    // class Coord {
    //     public int x;
    //     public int y;

    //     public bool Equals(Coord coord) {
    //         return coord.x == x && coord.y == y;
    //     }
    // }
}
