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
        var devices = GetInput();

        return NumberOfPathsPassingDacFft(devices.Single(d => d.name == "svr"), []);
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

    private static long NumberOfPathsPassingDacFft(Device device, Dictionary<(Device, bool, bool), long> cache, bool passedDac = false, bool passedFft = false)
    {
        if (device.name == "out" && passedDac && passedFft) return 1;

        var key = (device, passedDac, passedFft);
        if (cache.TryGetValue(key, out var cachedValue)) return cachedValue;

        if (device.name == "dac") passedDac = true;
        if (device.name == "fft") passedFft = true;

        long count = 0;

        foreach (var outputDevice in device.outputDevices)
        {
            count += NumberOfPathsPassingDacFft(outputDevice, cache, passedDac, passedFft);
        }

        cache[key] = count;

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
        public string name = "";
        public List<Device> outputDevices = [];
    }
}
