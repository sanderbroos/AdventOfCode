using System.Text.RegularExpressions;

namespace AOC2024;

public class Day21
{
    char?[,] NumericKeys = { { '7', '8', '9' }, { '4', '5', '6' }, { '1', '2', '3' }, { null, '0', 'A' } };
    char?[,] DirectionalKeys = { { null, '^', 'A' }, { '<', 'v', '>' } };

    public int Part1()
    {
        string[] codes = GetInput();
        List<int> complexities = [];

        foreach (string code in codes)
        {
            string sequenceRobot1 = GetButtonSequence(code, NumericKeys);
            string sequenceRobot2 = GetButtonSequence(sequenceRobot1, DirectionalKeys);
            string sequenceRobot3 = GetButtonSequence(sequenceRobot2, DirectionalKeys);

            complexities.Add(sequenceRobot3.Length * int.Parse(Regex.Match(code, @"\d+").Value));
        }

        return complexities.Sum();
    }

    public long Part2()
    {
        string[] codes = GetInput();
        List<long> complexities = [];

        foreach (string code in codes)
        {
            string robotSequence = GetButtonSequence(code, NumericKeys);
            var sequenceCounts = GetSequenceCounts(robotSequence);

            for (int i = 0; i < 25; i++)
            {
                Dictionary<string, long> newSequenceCounts = [];

                foreach (string key in sequenceCounts.Keys)
                {
                    string keySequence = GetButtonSequence(key.Last().ToString(), DirectionalKeys, key.First());
                    Dictionary<string, long> keySequenceCounts = GetSequenceCounts(keySequence);

                    foreach (string secondKey in keySequenceCounts.Keys)
                    {
                        if (!newSequenceCounts.ContainsKey(secondKey))
                        {
                            newSequenceCounts[secondKey] = 0;
                        }
                        newSequenceCounts[secondKey] += sequenceCounts[key];
                    }
                }

                sequenceCounts = newSequenceCounts;
            }

            complexities.Add(sequenceCounts.Values.Sum() * int.Parse(Regex.Match(code, @"\d+").Value));
        }

        return complexities.Sum();
    }

    string GetButtonSequence(string buttonsToPress, char?[,] buttons, char startingButton = 'A')
    {
        string buttonSequence = "";
        (int currentX, int currentY) = CoordOf(buttons, startingButton);

        foreach (char button in buttonsToPress)
        {
            (int buttonX, int buttonY) = CoordOf(buttons, button);
            (int offsetX, int offsetY) = (buttonX - currentX, buttonY - currentY);

            char horizontalChar = offsetX > 0 ? '>' : '<';
            char verticalChar = offsetY > 0 ? 'v' : '^';

            // idk why it is better
            // it was a lucky guess
            // don't touch it!!
            if ((buttons[currentY + offsetY, currentX] != null && (offsetX > 0)) || buttons[currentY, currentX + offsetX] == null)
            {
                buttonSequence += new string(verticalChar, Math.Abs(offsetY))
                    + new string(horizontalChar, Math.Abs(offsetX))
                    + 'A';
            }
            else
            {
                buttonSequence += new string(horizontalChar, Math.Abs(offsetX))
                    + new string(verticalChar, Math.Abs(offsetY))
                    + 'A';
            }

            (currentX, currentY) = (buttonX, buttonY);
        }

        return buttonSequence;
    }

    Dictionary<string, long> GetSequenceCounts(string sequence)
    {
        sequence = 'A' + sequence;
        Dictionary<string, long> sequenceCounts = [];

        for (int seqIndex = 1; seqIndex < sequence.Length; seqIndex++)
        {
            char initialButton = sequence[seqIndex - 1];
            char targetButton = sequence[seqIndex];
            string key = new string([initialButton, targetButton]);
            if (!sequenceCounts.ContainsKey(key))
            {
                sequenceCounts[key] = 0;
            }
            sequenceCounts[key]++;
        }

        return sequenceCounts;
    }

    (int x, int y) CoordOf(char?[,] keys, char button)
    {
        for (int y = 0; y < keys.GetLength(0); y++)
        {
            for (int x = 0; x < keys.GetLength(1); x++)
            {
                if (keys[y, x] == button)
                {
                    return (x, y);
                }
            }
        }

        return (-1, -1);
    }

    private static string[] GetInput()
    {
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\2024\\Day21\\input.txt");
    }
}
