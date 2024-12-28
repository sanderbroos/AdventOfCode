namespace AOC2024;

public class Day15Part1
{
    public int GetSolution()
    {
        var map = GetInputMap();
        var moves = GetInputMoves();

        foreach (Move move in moves)
        {
            (int robotY, int robotX) = CoordinatesOf(map, '@');
            TryMove(map, robotX, robotY, move);
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

    private bool TryMove(List<List<char?>> map, int x, int y, Move move)
    {
        char? itemToMove = map[y][x];
        int newX = x + move.x;
        int newY = y + move.y;

        // empty
        if (map[newY][newX] == null)
        {
            map[y][x] = null;
            map[newY][newX] = itemToMove;
            return true;
        }
        // wall
        else if (map[newY][newX] == '#')
        {
            return false;
        }
        // box
        else if (map[newY][newX] == 'O')
        {
            if (TryMove(map, newX, newY, move))
            {
                map[y][x] = null;
                map[newY][newX] = itemToMove;
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
                if (map[y][x] == 'O')
                {
                    gpsSum += 100 * y + x;
                }
            }
        }

        return gpsSum;
    }

    private List<List<char?>> GetInputMap()
    {
        var input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day15\\inputMap.txt")
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
