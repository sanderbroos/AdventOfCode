namespace AOC2024;

public class Day12 {
    public static int Part1() {
        List<List<char?>> plants = GetInput();

        int totalPrice = 0;
        List<(int, int)> checkedCoords = [];

        for (int i = 0; i < plants.Count; i++) {
            for (int j = 0; j < plants[0].Count; j++) {
                (int area, int perimeter) = CheckRegionFromCoords(plants, i, j, checkedCoords);
                totalPrice += area * perimeter;

                foreach (var coord in checkedCoords) {
                    plants[coord.Item1][coord.Item2] = null;
                }
            }
        }

        return totalPrice;
    }

    public static void Part2() {
        
    }

    public static (int area, int perimeter) CheckRegionFromCoords(List<List<char?>> plants, int i, int j, List<(int, int)> checkedCoords) {
        checkedCoords.Add((i, j));

        char? plantType = plants[i][j];
        if (!plantType.HasValue) {
            return (0, 0);
        }

        int area = 1;
        int perimeter = 0;

        (int, int)[] fourSides = [(i-1,j), (i+1,j), (i, j-1), (i, j+1)];

        foreach ((int i2, int j2) in fourSides) {
            if (i2 >= 0 && i2 < plants.Count && j2 >= 0 && j2 < plants[0].Count && plants[i2][j2] == plantType) {
                if (!checkedCoords.Any(c => c.Item1 == i2 && c.Item2 == j2)) {
                    (int, int) result = CheckRegionFromCoords(plants, i2, j2, checkedCoords);
                    area += result.Item1;
                    perimeter += result.Item2;
                }
            }
            else {
                perimeter += 1;
            }
        }

        return (area, perimeter);
    }

    private static List<List<char?>> GetInput() {
        List<List<char?>> input = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day12\\input.txt")
            .Select(line => line.Select(c => (char?) c).ToList())
            .ToList();

        return input;
    }
}
