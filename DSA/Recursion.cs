using System.Text;

namespace DSA;

public static class Recursion
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
    
}