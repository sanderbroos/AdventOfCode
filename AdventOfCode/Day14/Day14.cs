namespace AOC2024;

public class Day14 {
    public static readonly int sizeX = 101;
    public static readonly int sizeY = 103;

    public static int Part1() {
        var robots = GetInput();

        for (int second = 0; second < 100; second++) {
            foreach (Robot robot in robots) {
                robot.Step();
            }
        }

        int safetyScore = 1;

        for (int quadrant = 1; quadrant <= 4; quadrant++) {
            safetyScore *= robots.Count(r => r.InQuadrant() == quadrant);
        }

        return safetyScore;
    }

    public static void Part2() {
        var robots = GetInput();

        int second = 0;
        while (true) {
            second++;
            foreach (Robot robot in robots) {
                robot.Step();
            }

            for (int x = 0; x < sizeX; x++) {
                if (robots.Count(r => r.posX == x) > 30) {
                    Console.WriteLine(second);
                    PrintBoard(robots);
                    Thread.Sleep(500);
                    break;
                }
            }
        }
    }

    public class Robot {
        public int posX;
        public int posY;
        public int velocityX;
        public int velocityY;

        public void Step() {
            posX = Modulo(posX + velocityX, sizeX);
            posY = Modulo(posY + velocityY, sizeY);
        }

        public int InQuadrant() {
            if (posX < (float) sizeX / 2 - 1) {
                if (posY < (float) sizeY / 2 - 1) {
                    return 1;
                }
                if (posY > (float) sizeY / 2) {
                    return 3;
                }
            }
            if (posX > (float) sizeX / 2) {
                if (posY < (float) sizeY / 2 - 1) {
                    return 2;
                }
                if (posY > (float) sizeY / 2) {
                    return 4;
                }
            }

            return 0;
        }
    }

    private static void PrintBoard(List<Robot> robots) {
        string toPrint = "";

        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                toPrint += robots.Any(r => r.posX == x && r.posY == y) ? " O" : "  ";
            }

            toPrint += "\n";
        }
        
        toPrint += "\n\n\n";

        Console.WriteLine(toPrint);
    }

    private static int Modulo(int a, int b) {
        return (a%b + b) % b;
    }

    private static List<Robot> GetInput() {
        var robots = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day14\\input.txt")
            .Select(line => line.Replace("p=", ""))
            .Select(line => line.Split([",", " v="], StringSplitOptions.None).ToList())
            .Select(line => new Robot {
                posX = int.Parse(line[0]),
                posY = int.Parse(line[1]),
                velocityX = int.Parse(line[2]),
                velocityY = int.Parse(line[3])
            })
            .ToList();

        return robots;
    }
}
