namespace AdventOfCode2025.Day01;

[TestClass]
public sealed class Day01Tests
{
    private const string ExampleInput = """
        L68
        L30
        R48
        L5
        R60
        L55
        L1
        L99
        R14
        L82
        """;

    private static string PuzzleInput => File.ReadAllText(@"Day01\input.txt");

    [TestMethod]
    public void Example()
    {
        Assert.AreEqual(3, SolvePart1(ExampleInput));
        Assert.AreEqual(6, SolvePart2(ExampleInput));
    }

    [TestMethod]
    public void Day1()
    {
        var result = SolvePart2(PuzzleInput);
        Console.WriteLine($"Day 1 Part 2 Answer: {result}");
        Assert.IsTrue(result > 0);
    }

    private static int SolvePart1(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var position = 50;
        var zeroCount = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            if (direction == 'L')
                position = ((position - distance) % 100 + 100) % 100;
            else
                position = (position + distance) % 100;

            if (position == 0)
                zeroCount++;
        }

        return zeroCount;
    }

    private static int SolvePart2(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var position = 50;
        var zeroCount = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            int start, end;
            if (direction == 'L')
            {
                start = position - distance;
                end = position - 1;
                position = ((position - distance) % 100 + 100) % 100;
            }
            else
            {
                start = position + 1;
                end = position + distance;
                position = (position + distance) % 100;
            }

            // Count multiples of 100 in range [start, end]
            zeroCount += FloorDiv100(end) - FloorDiv100(start - 1);
        }

        return zeroCount;
    }

    private static int FloorDiv100(int a) => a >= 0 ? a / 100 : (a - 99) / 100;
}
