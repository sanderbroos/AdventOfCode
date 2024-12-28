namespace AOC2024;

public class Day15Part2
{
    public int GetSolution()
    {
        var map = GetInputMap();
        var moves = GetInputMoves();

        foreach (Move move in moves)
        {
            TryMoveRobot(map, move);
        }

        return SumOfBoxGPS(map);
    }

    private (int, int) CoordinatesOf(List<List<char?>> map, char? item)
    {
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] == item)
                {
                    return (y, x);
                }
            }
        }

        return (-1, -1);
    }

    private bool TryMoveRobot(List<List<char?>> map, Move move)
    {
        (int y, int x) = CoordinatesOf(map, '@');
        int newX = x + move.x;
        int newY = y + move.y;

        // empty
        if (map[newY][newX] == null)
        {
            map[y][x] = null;
            map[newY][newX] = '@';
            return true;
        }
        // wall
        else if (map[newY][newX] == '#')
        {
            return false;
        }
        // box
        else if (IsBox(map[newY][newX]))
        {
            if (TryMoveBox(map, newX, newY, move))
            {
                map[y][x] = null;
                map[newY][newX] = '@';
                return true;
            }
        }

        return false;
    }

    private bool TryMoveBox(List<List<char?>> map, int x, int y, Move move, bool moveBox = true)
    {
        if (map[y][x] == ']') { x -= 1; };

        int newX = x + move.x;
        int newY = y + move.y;

        List<(int x, int y)> coordsToCheck = move.x == 0 ? [(newX, newY), (newX + 1, newY)] :
            move.x == -1 ? [(newX, newY)] : [(newX + 1, newY)];

        if (coordsToCheck.Count == 2 && map[coordsToCheck[0].y][coordsToCheck[0].x] == '[')
        {
            coordsToCheck = [(newX, newY)];
        }

        // empty
        if (coordsToCheck.All(c => map[c.y][c.x] == null))
        {
            if (moveBox)
            {
                map[y][x] = null;
                map[y][x + 1] = null;
                map[newY][newX] = '[';
                map[newY][newX + 1] = ']';
            }
            return true;
        }
        // wall
        else if (coordsToCheck.Any(c => map[c.y][c.x] == '#'))
        {
            return false;
        }
        // box
        else if (coordsToCheck.Any(c => IsBox(map[c.y][c.x])))
        {
            if (coordsToCheck.Where(c => IsBox(map[c.y][c.x])).All(c => TryMoveBox(map, c.x, c.y, move, false)))
            {
                if (moveBox)
                {
                    coordsToCheck.Where(c => IsBox(map[c.y][c.x])).ToList().ForEach(c => TryMoveBox(map, c.x, c.y, move));
                    map[y][x] = null;
                    map[y][x + 1] = null;
                    map[newY][newX] = '[';
                    map[newY][newX + 1] = ']';
                }
                return true;
            }
        }

        return false;
    }

    private int SumOfBoxGPS(List<List<char?>> map)
    {
        var gpsSum = 0;

        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] == '[')
                {
                    gpsSum += 100 * y + x;
                }
            }
        }

        return gpsSum;
    }

    private bool IsBox(char? item)
    {
        return item == '[' || item == ']';
    }

    private List<List<char?>> GetInputMap()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day15\\inputMap.txt")
            .Select(line => line.Replace("#", "##").Replace("O", "[]").Replace(".", "..").Replace("@", "@."))
            .Select(line => line.Select(item => (char?)(item == '.' ? null : item)).ToList())
            .ToList();

        return input;
    }

    private List<Move> GetInputMoves()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day15\\inputMoves.txt")
            .First()
            .Select(getMoveVector)
            .ToList();

        return input;
    }

    internal class Move
    {
        internal int x;
        internal int y;
    }

    private Move getMoveVector(char move)
    {
        switch (move)
        {
            case '<':
                return new Move { x = -1, y = 0 };
            case '>':
                return new Move { x = 1, y = 0 };
            case '^':
                return new Move { x = 0, y = -1 };
            case 'v':
                return new Move { x = 0, y = 1 };
        }

        return null;
    }
}
