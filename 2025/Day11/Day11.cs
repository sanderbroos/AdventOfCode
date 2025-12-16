namespace AOC2025;

public class Day11
{
    public static long Part1()
    {
        var devices = GetInput();

        return NumberOfPathsOut(devices.Single(d => d.name == "you"));
    }

    public static long Part2()
    {
        return 0;
    }

    private static long NumberOfPathsOut(Device device)
    {
        if (device.name == "out") return 1;

        long count = 0;

        foreach (var outputDevice in device.outputDevices)
        {
            count += NumberOfPathsOut(outputDevice);
        }

        return count;
    }

    private static List<Device> GetInput()
    {
        var input = File.ReadAllLines("/Users/sanderbroos/Documents/Source/AdventOfCode/2025/Day11/input.txt");

        List<Device> devices = [];
        foreach (var line in input)
        {
            var splitUp = line.Split(": ");
            var device = devices.SingleOrDefault(d => d.name == splitUp[0]);
            if (device is null)
            {
                device = new Device { name = splitUp[0] };
                devices.Add(device);
            }

            foreach (var outputDeviceName in splitUp[1].Split(' '))
            {
                var outputDevice = devices.SingleOrDefault(d => d.name == outputDeviceName);
                if (outputDevice is null)
                {
                    outputDevice = new Device { name = outputDeviceName };
                    devices.Add(outputDevice);
                }
                device.outputDevices.Add(outputDevice);
            }
        }

        return devices;
    }

    public class Device
    {
        public string name;
        public List<Device> outputDevices = [];
    }
}
