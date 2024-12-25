namespace AOC2024;

public class Day05
{
    public int Part1()
    {
        return AllUpdates().validUpdates.Sum(u => int.Parse(u[u.Count / 2]));
    }

    public int Part2()
    {
        var rules = GetRules();
        var invalidUpdates = AllUpdates().invalidUpdates;

        foreach (var update in invalidUpdates)
        {
            bool updateIsValid = false;
            while (!updateIsValid)
            {
                updateIsValid = true;
                foreach (var page in update)
                {
                    var pageRules = rules.Where(r => r.First() == page);
                    var pageIndex = update.IndexOf(page);

                    foreach (var rule in pageRules)
                    {
                        var otherPage = rule.Last();
                        if (update.Contains(otherPage))
                        {
                            if (update.IndexOf(otherPage) < pageIndex)
                            {
                                updateIsValid = false;
                                update.Insert(pageIndex + 1, otherPage);
                                update.Remove(otherPage);
                            }
                        }
                    }
                    if (!updateIsValid)
                    {
                        break;
                    }
                }
            }
        }

        return invalidUpdates.Sum(u => int.Parse(u[u.Count / 2]));
    }

    public (List<List<string>> validUpdates, List<List<string>> invalidUpdates) AllUpdates()
    {
        var rules = GetRules();
        var updates = GetUpdates();

        List<List<string>> validUpdates = [];
        List<List<string>> invalidUpdates = [];

        foreach (var update in updates)
        {
            bool updateIsValid = true;
            foreach (var page in update)
            {
                var pageRules = rules.Where(r => r.First() == page);
                var pageIndex = update.IndexOf(page);

                foreach (var rule in pageRules)
                {
                    var otherPage = rule.Last();
                    if (update.Contains(otherPage))
                    {
                        if (update.IndexOf(otherPage) < pageIndex)
                        {
                            updateIsValid = false;
                            break;
                        }
                    }
                }

                if (!updateIsValid)
                {
                    break;
                }
            }

            if (updateIsValid)
            {
                validUpdates.Add(update);
            }
            else
            {
                invalidUpdates.Add(update);
            }
        }

        return (validUpdates, invalidUpdates);
    }

    private static List<List<string>> GetRules()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day05\\inputRules.txt")
            .Select(line => line.Split("|").ToList())
            .ToList();
    }

    private static List<List<string>> GetUpdates()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day05\\inputUpdates.txt")
            .Select(line => line.Split(",").ToList())
            .ToList();
    }
}
