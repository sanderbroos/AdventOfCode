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
            List<string> sequencesRobot1 = getButtonSequences(code, NumericKeys);
            List<string> sequencesRobot2 = sequencesRobot1.SelectMany(s => getButtonSequences(s, DirectionalKeys)).ToList();
            List<string> sequencesRobot3 = sequencesRobot2.SelectMany(s => getButtonSequences(s, DirectionalKeys)).ToList();

            complexities.Add(sequencesRobot3.OrderBy(s => s.Length).First().Length * int.Parse(Regex.Match(code, @"\d+").Value));
        }

        return complexities.Sum();
    }

    public int Part2()
    {
        return 0;
    }

    List<string> getButtonSequences(string buttonsToPress, char?[,] buttons)
    {
        List<string> buttonSequences = [""];
        (int currentX, int currentY) = CoordOf(buttons, 'A');

        foreach (char button in buttonsToPress)
        {
            (int buttonX, int buttonY) = CoordOf(buttons, button);
            (int offsetX, int offsetY) = (buttonX - currentX, buttonY - currentY);

            char horizontalChar = offsetX > 0 ? '>' : '<';
            char verticalChar = offsetY > 0 ? 'v' : '^';

            List<string> newButtonSequences = [];
            if (buttons[currentY + offsetY, currentX] != null)
            {
                newButtonSequences.AddRange(buttonSequences.Select(bs => bs
                    + new string(verticalChar, Math.Abs(offsetY))
                    + new string(horizontalChar, Math.Abs(offsetX))
                    + 'A'));
            }
            if (buttons[currentY, currentX + offsetX] != null)
            {
                newButtonSequences.AddRange(buttonSequences.Select(bs => bs
                    + new string(horizontalChar, Math.Abs(offsetX))
                    + new string(verticalChar, Math.Abs(offsetY))
                    + 'A'));
            }

            buttonSequences = newButtonSequences.Distinct().ToList();

            (currentX, currentY) = (buttonX, buttonY);
        }

        return buttonSequences;
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
        return File.ReadAllLines("..\\..\\..\\..\\AdventOfCode\\Day21\\input.txt");
    }
}
