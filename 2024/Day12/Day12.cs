namespace AOC2024;

public class Day12
{
    private List<List<Plant>> plants = [];

    public int Part1()
    {
        LoadPlants();

        int totalPrice = 0;

        for (int y = 0; y < plants.Count; y++)
        {
            for (int x = 0; x < plants[0].Count; x++)
            {
                (int area, List<PlantSide> plantSides) = CheckRegionFromCoords(y, x, []);
                totalPrice += area * plantSides.Count;
            }
        }

        return totalPrice;
    }

    public int Part2()
    {
        LoadPlants();

        int totalPrice = 0;

        for (int y = 0; y < plants.Count; y++)
        {
            for (int x = 0; x < plants[0].Count; x++)
            {
                (int area, List<PlantSide> plantSides) = CheckRegionFromCoords(y, x, []);
                totalPrice += area * CountRegionSides(plantSides);
            }
        }

        return totalPrice;
    }

    private (int area, List<PlantSide> perimeter) CheckRegionFromCoords(int y, int x, List<PlantSide> plantSides)
    {
        Plant plant = plants[y][x];
        if (plant.IsChecked)
        {
            return (0, plantSides);
        }
        plant.IsChecked = true;

        int area = 1;

        Dictionary<Direction, (int, int)> fourSides = new()
        {
            { Direction.Up, (y-1,x) },
            { Direction.Down, (y+1,x) },
            { Direction.Left, (y, x-1) },
            { Direction.Right, (y, x+1) }
        };

        foreach (KeyValuePair<Direction, (int y2, int x2)> item in fourSides)
        {
            bool outOfBounds = item.Value.y2 < 0 || item.Value.y2 >= plants.Count || item.Value.x2 < 0 || item.Value.x2 >= plants[0].Count;

            if (!outOfBounds && plants[item.Value.y2][item.Value.x2].Type == plant.Type)
            {
                area += CheckRegionFromCoords(item.Value.y2, item.Value.x2, plantSides).area;
            }
            else
            {
                plantSides.Add(new PlantSide
                {
                    x = x,
                    y = y,
                    direction = item.Key
                });
            }
        }

        return (area, plantSides);
    }

    private int CountRegionSides(List<PlantSide> plantSides)
    {
        int regionSidesCount = 0;

        while (plantSides.Count > 0)
        {
            regionSidesCount++;
            RemoveConnectedPlantSides(plantSides, plantSides[0]);
        }

        return regionSidesCount;
    }

    private void RemoveConnectedPlantSides(List<PlantSide> plantSides, PlantSide plantSide)
    {
        plantSides.Remove(plantSide);

        var adjacentPlantSides = plantSides.Where(ps => ps.direction == plantSide.direction
            && (Math.Abs(ps.x - plantSide.x) == 1 && ps.y == plantSide.y
                || Math.Abs(ps.y - plantSide.y) == 1 && ps.x == plantSide.x))
            .ToList();

        foreach (PlantSide adjacentPlantSide in adjacentPlantSides)
        {
            RemoveConnectedPlantSides(plantSides, adjacentPlantSide);
        }
    }

    private void LoadPlants()
    {
        plants = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day12\\input.txt")
            .Select(line => line.Select(c => new Plant { Type = c, IsChecked = false }).ToList())
            .ToList();
    }

    internal class Plant
    {
        internal char Type;
        internal bool IsChecked;
    }

    internal class PlantSide
    {
        internal int x;
        internal int y;
        internal Direction direction;
    }

    internal enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
}
