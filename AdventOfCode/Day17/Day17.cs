
using System.ComponentModel.DataAnnotations;

namespace AOC2024;

public class Day17
{
    long RegisterA = 37293246;
    long RegisterB = 0;
    long RegisterC = 0;
    int InstructionPointer = 0;
    // List<int> Program = [2,4,1,6,7,5,4,4,1,7,0,3,5,5,3,0];
    List<int> Program = [0, 3, 5, 4, 3, 0];
    string Output = "";

    public string Part1()
    {
        while (InstructionPointer < Program.Count)
        {
            Step();
        }

        return Output[..^1];
    }

    public long Part2()
    {
        string desiredOutput = string.Join(",", Program) + ",";

        for (long i = (long)Math.Pow(8, 15); i < Math.Pow(8, 16); i++)
        {
            RegisterA = i;
            InstructionPointer = 0;
            Output = "";

            bool valid = true;
            while (InstructionPointer < Program.Count)
            {
                if (!desiredOutput.StartsWith(Output))
                {
                    valid = false;
                    break;
                }

                Step();
            }


            if (valid && Output == desiredOutput)
            {
                return i;
            }
        }

        return 0;
    }

    private void Step()
    {
        int opcode = Program[InstructionPointer];
        int operand = Program[InstructionPointer + 1];

        switch (opcode)
        {
            case 0:
                adv(operand);
                break;
            case 1:
                bxl(operand);
                break;
            case 2:
                bst(operand);
                break;
            case 3:
                jnz(operand);
                break;
            case 4:
                bxc();
                break;
            case 5:
                Output += outOperation(operand) + ",";
                break;
            case 6:
                bdv(operand);
                break;
            case 7:
                cdv(operand);
                break;
        }

        InstructionPointer += 2;
    }

    private void adv(int operand)
    {
        long comboOperand = ComboOperand(operand);

        RegisterA = (long)(RegisterA / Math.Pow(2, comboOperand));
    }

    private void bxl(int operand)
    {
        RegisterB ^= operand;
    }

    private void bst(int operand)
    {
        long comboOperand = ComboOperand(operand);

        RegisterB = comboOperand % 8;
    }

    private void jnz(int operand)
    {
        if (RegisterA == 0)
        {
            return;
        }

        InstructionPointer = operand - 2;
    }

    private void bxc()
    {
        RegisterB ^= RegisterC;
    }

    private long outOperation(int operand)
    {
        return ComboOperand(operand) % 8;
    }

    private void bdv(int operand)
    {
        long comboOperand = ComboOperand(operand);

        RegisterB = (long)(RegisterA / Math.Pow(2, comboOperand));
    }

    private void cdv(int operand)
    {
        long comboOperand = ComboOperand(operand);

        RegisterC = (long)(RegisterA / Math.Pow(2, comboOperand));
    }

    private long ComboOperand(int operand)
    {
        switch (operand)
        {
            case 4:
                return RegisterA;
            case 5:
                return RegisterB;
            case 6:
                return RegisterC;
            case 7:
                throw new Exception("Whuu??");
        }

        return operand;
    }
}
