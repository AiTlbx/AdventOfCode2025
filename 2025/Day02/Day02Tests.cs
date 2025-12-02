namespace AdventOfCode2025.Day02;

// --- Day 2: Gift Shop ---
// A young Elf added invalid product IDs to the gift shop database.
// Invalid IDs are numbers made of a sequence of digits repeated:
// Part 1: exactly twice - 55 (5 twice), 6464 (64 twice), 123123 (123 twice)
// Part 2: at least twice - also includes 111 (1 x3), 123123123 (123 x3), etc.
// No leading zeroes allowed (0101 isn't valid).
// Find all invalid IDs in the given ranges and sum them up.

[TestClass]
public sealed class Day02Tests
{
    private const string ExampleInput = """
        11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124
        """;

    private static string PuzzleInput => File.ReadAllText(@"Day02\input.txt");

    [TestMethod]
    public void Example()
    {
        Assert.AreEqual(1227775554L, SolvePart1(ExampleInput));
        Assert.AreEqual(4174379265L, SolvePart2(ExampleInput));
    }

    [TestMethod]
    public void Day2()
    {
        Console.WriteLine($"Part 1: {SolvePart1(PuzzleInput)}");
        Console.WriteLine($"Part 2: {SolvePart2(PuzzleInput)}");
    }

    private static long SolvePart1(string input)
    {
        var line = input.Trim();
        var ranges = line.Split(',');
        long sum = 0;

        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);

            sum += SumInvalidIdsInRange(start, end);
        }

        return sum;
    }

    private static long SumInvalidIdsInRange(long start, long end)
    {
        long sum = 0;

        for (int len = 1; len <= 10; len++)
        {
            long multiplier = (long)Math.Pow(10, len);

            long minPattern = len == 1 ? 1 : (long)Math.Pow(10, len - 1);
            long maxPattern = (long)Math.Pow(10, len) - 1;

            long minId = minPattern * multiplier + minPattern;
            long maxId = maxPattern * multiplier + maxPattern;

            if (maxId < start || minId > end) continue;

            long patternStart = Math.Max(minPattern, (start - 1) / (multiplier + 1) + 1);
            long patternEnd = Math.Min(maxPattern, end / (multiplier + 1));

            if (patternStart > patternEnd) continue;

            for (long pattern = patternStart; pattern <= patternEnd; pattern++)
            {
                long invalidId = pattern * multiplier + pattern;
                if (invalidId >= start && invalidId <= end)
                {
                    sum += invalidId;
                }
            }
        }

        return sum;
    }

    private static long SolvePart2(string input)
    {
        var line = input.Trim();
        var ranges = line.Split(',');
        var invalidIds = new HashSet<long>();

        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);

            FindInvalidIdsPart2(start, end, invalidIds);
        }

        return invalidIds.Sum();
    }

    private static void FindInvalidIdsPart2(long start, long end, HashSet<long> invalidIds)
    {
        int minDigits = start.ToString().Length;
        int maxDigits = end.ToString().Length;

        for (int totalDigits = minDigits; totalDigits <= maxDigits; totalDigits++)
        {
            for (int patternLen = 1; patternLen <= totalDigits / 2; patternLen++)
            {
                if (totalDigits % patternLen != 0) continue;

                int repeatCount = totalDigits / patternLen;
                if (repeatCount < 2) continue;

                long minPattern = patternLen == 1 ? 1 : (long)Math.Pow(10, patternLen - 1);
                long maxPattern = (long)Math.Pow(10, patternLen) - 1;

                long minId = CreateRepeatedId(minPattern, patternLen, repeatCount);
                long maxId = CreateRepeatedId(maxPattern, patternLen, repeatCount);

                if (maxId < start || minId > end) continue;

                for (long pattern = minPattern; pattern <= maxPattern; pattern++)
                {
                    long invalidId = CreateRepeatedId(pattern, patternLen, repeatCount);
                    if (invalidId >= start && invalidId <= end)
                    {
                        invalidIds.Add(invalidId);
                    }
                }
            }
        }
    }

    private static long CreateRepeatedId(long pattern, int patternLen, int repeatCount)
    {
        long result = 0;
        long multiplier = (long)Math.Pow(10, patternLen);
        for (int i = 0; i < repeatCount; i++)
        {
            result = result * multiplier + pattern;
        }
        return result;
    }
}
