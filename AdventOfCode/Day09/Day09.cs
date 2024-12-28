namespace AOC2024;

public class Day09
{
    public long Part1()
    {
        var fileBlocks = GetInput();

        while (fileBlocks.Contains(null))
        {
            fileBlocks[fileBlocks.IndexOf(null)] = fileBlocks.Last();
            fileBlocks.RemoveAt(fileBlocks.Count - 1);

            while (!fileBlocks.Last().HasValue)
            {
                fileBlocks.RemoveAt(fileBlocks.Count - 1);
            }
        }

        return Checksum(fileBlocks);
    }

    public long Part2()
    {
        var fileBlocks = GetInput2();
        int lastId = fileBlocks.Last().id ?? -1;

        for (int fileId = lastId; fileId >= 0; fileId--)
        {
            var fileBlock = fileBlocks.Single(fb => fb.id == fileId);
            for (int i = 0; i < fileBlocks.IndexOf(fileBlock); i++)
            {
                if (fileBlocks[i].id == null)
                {
                    if (fileBlocks[i].count > fileBlock.count)
                    {
                        fileBlocks[i] = (fileBlocks[i].id, fileBlocks[i].count - fileBlock.count);
                        fileBlocks.Insert(i, fileBlock);
                    }
                    else if (fileBlocks[i].count == fileBlock.count)
                    {
                        fileBlocks[i] = fileBlock;
                    }
                    else
                    {
                        continue;
                    }
                    fileBlocks[fileBlocks.LastIndexOf(fileBlock)] = (null, fileBlock.count);
                    break;
                }
            }
        }

        List<int?> individualFileBlocks = [];
        foreach (var (id, count) in fileBlocks)
        {
            individualFileBlocks.AddRange(Enumerable.Repeat(id, count));
        }

        return Checksum(individualFileBlocks);
    }

    long Checksum(List<int?> fileBlocks)
    {
        long checksum = 0;
        for (int i = 0; i < fileBlocks.Count; i++)
        {
            checksum += i * fileBlocks[i] ?? 0;
        }
        return checksum;
    }

    private static List<int?> GetInput()
    {
        var diskmap = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day09\\input.txt").First();
        List<int?> fileBlocks = [];

        for (int i = 0; i < diskmap.Length; i++)
        {
            int number = int.Parse(diskmap[i].ToString());

            if (i % 2 == 0)
            {
                fileBlocks.AddRange(Enumerable.Repeat((int?)i / 2, number));
            }
            else
            {
                fileBlocks.AddRange(Enumerable.Repeat((int?)null, number));
            }
        }

        return fileBlocks;
    }

    private static List<(int? id, int count)> GetInput2()
    {
        var diskmap = File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day09\\input.txt").First();
        List<(int?, int)> fileBlocks = [];

        for (int i = 0; i < diskmap.Length; i++)
        {
            int number = int.Parse(diskmap[i].ToString());
            if (number == 0) { continue; }

            if (i % 2 == 0)
            {
                fileBlocks.Add(((int?)i / 2, number));
            }
            else
            {
                fileBlocks.Add((null, number));
            }
        }

        return fileBlocks;
    }
}
