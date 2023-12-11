using System.Text;
using System.Text.RegularExpressions;

namespace DSA;

public static partial class Recursion
{
    private static void DrawLine(int tickLength, string label = "")
    {
        StringBuilder sb = new();
        for (var i = 0; i < tickLength; i++)
        {
            sb.Append('-');
        }

        if (label.Length > 0)
        {
            sb.Append($" {label}");
        }

        Console.WriteLine(sb.ToString());
    }

    private static void DrawInterval(int centerLength)
    {
        if (centerLength <= 0) return;
        DrawInterval(centerLength - 1);
        DrawLine(centerLength);
        DrawInterval(centerLength - 1);
    }

    public static void DrawRuler(int inches, int majorLength)
    {
        DrawLine(majorLength, "0");
        for (var i = 1; i < inches + 1; i++)
        {
            DrawInterval(majorLength - 1);
            DrawLine(majorLength, i.ToString());
        }
    }

    public static bool BinarySearch(IList<int> collection, int target, int low, int high)
    {
        if (low > high) return false;

        var mid = (low + high) / 2;
        var candidate = collection[mid];


        if (target == candidate) return true;

        return target < candidate
            ? BinarySearch(collection, target, low, mid - 1)
            : BinarySearch(collection, target, mid + 1, high);
    }

    public static long DiskUsage(string path)
    {
        long total = 0;
        if (!Directory.Exists(path))
        {
            total += new FileInfo(path).Length;
            return total;
        }


        var dirInfo = new DirectoryInfo(path);
        total += ImmediateUsage(dirInfo);
        total += dirInfo.GetDirectories().Sum(dir => DiskUsage(dir.FullName));

        Console.WriteLine($"{total}: {path}");
        return total;
    }


    private static long ImmediateUsage(DirectoryInfo dirInfo)
    {
        return dirInfo.GetFiles().Sum(file => file.Length);
    }

    /// <summary>
    /// Reverse elements in implicit slice sequence[start:stop].
    /// Works by swapping the first and last elements and then recursively doing it for
    /// the other elements
    /// </summary>
    /// <param name="sequence">A collection that can be indexed</param>
    /// <param name="start">The start position of the implicit range</param>
    /// <param name="stop">The last position of the implicit range</param>
    /// <typeparam name="T">Type of the elements stored in <code cref="sequence">sequence</code></typeparam>
    public static void Reverse<T>(IList<T> sequence, int start, int stop)
    {
        // If there are at least 2 elements in the implicit range
        if (start >= stop - 1) return;

        (sequence[start], sequence[stop - 1]) = (sequence[stop - 1], sequence[start]);
        Reverse(sequence, start + 1, stop - 1);
    }

    /// <summary>
    /// Compute the value of x^n for integers x and n
    /// </summary>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static long Power(int x, int n)
    {
        if (n == 0) return 1;

        var partial = Power(x, n / 2);
        var result = partial * partial;

        if (n % 2 == 1) result *= x;

        return result;
    }

    public static int Max(IList<int> sequence)
    {
        if (!sequence.Any()) throw new ArgumentException("Argument cannot be an empty list!");
        return RecursiveMax(sequence, 0, sequence[0]);
    }

    private static int RecursiveMax(IList<int> sequence, int n, int currentMax)
    {
        if (n >= sequence.Count - 1) return currentMax;

        if (sequence[n + 1] > currentMax) currentMax = sequence[n + 1];
        return RecursiveMax(sequence, n + 1, currentMax);
    }

    public static (int, int) AltMaxMin(IList<int> sequence, int n)
    {
        if (n == 0) return (sequence[0], sequence[0]);

        var (currentMax, currentMin) = AltMaxMin(sequence.Skip(count: 1).ToList(), n - 1);
        var nextMax = currentMax > sequence[0] ? currentMax : sequence[0];
        var nextMin = currentMin < sequence[0] ? currentMin : sequence[0];

        return (nextMax, nextMin);
    }

    public static int ConvertToInt(this string str, int n)
    {
        if (string.IsNullOrEmpty(str) || MyRegex().IsMatch(str))
        {
            throw new ArgumentException("Invalid string");
        }

        if (n == 0) return 0;

        return ConvertToInt(str, n - 1) + str[n - 1].IntConverter() * PlaceValue(str, n - 1);
    }

    private static int IntConverter(this char ch)
    {
        return ch switch
        {
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            '0' => 0,
            _ => throw new ArgumentException("Only numeric chars (e.g. '1') are accepted")
        };
    }

    private static int PlaceValue(string str, int i)
    {
        return (int)Power(10, str.Length - 1- i);
    }

    public static bool IsPalindrome(this string str)
    {
        return RecursiveIsPalindrome(str, str.Length - 1);
        // return !str.Where((t, i) => t != str[str.Length - 1 - i]).Any();
    }

    private static bool RecursiveIsPalindrome(string str, int n)
    {
        if (n >= 0) return true;
        if (str[n] != str[str.Length - 1 - n]) return false;
        return RecursiveIsPalindrome(str, n - 1);
        
    }

    [GeneratedRegex("\\D")]
    private static partial Regex MyRegex();
}