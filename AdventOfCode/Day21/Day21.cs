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
            string sequenceRobot1 = getButtonSequence(code, NumericKeys);
            string sequenceRobot2 = getButtonSequence(sequenceRobot1, DirectionalKeys);
            string sequenceRobot3 = getButtonSequence(sequenceRobot2, DirectionalKeys);

            complexities.Add(sequenceRobot3.Length * int.Parse(Regex.Match(code, @"\d+").Value));
        }

        return complexities.Sum();
    }

    public int Part2()
    {
        string[] codes = GetInput();
        List<int> complexities = [];

        foreach (string code in codes)
        {
            string robotSequence = getButtonSequence(code, NumericKeys);

            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(i.ToString() + " " + robotSequence.Length);
                robotSequence = getButtonSequence(robotSequence, DirectionalKeys);
            }

            complexities.Add(robotSequence.Length * int.Parse(Regex.Match(code, @"\d+").Value));
        }

        return complexities.Sum();
    }

    string getButtonSequence(string buttonsToPress, char?[,] buttons)
    {
        string buttonSequence = "";
        (int currentX, int currentY) = CoordOf(buttons, 'A');

        foreach (char button in buttonsToPress)
        {
            (int buttonX, int buttonY) = CoordOf(buttons, button);
            (int offsetX, int offsetY) = (buttonX - currentX, buttonY - currentY);

            char horizontalChar = offsetX > 0 ? '>' : '<';
            char verticalChar = offsetY > 0 ? 'v' : '^';

            if ((buttons[currentY + offsetY, currentX] != null && (offsetX > 0 || offsetY > 0)) || buttons[currentY, currentX + offsetX] == null)
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
