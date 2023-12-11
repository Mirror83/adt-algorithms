using System.Text;
using System.Text.RegularExpressions;

namespace DSA;

public static partial class Arrays
{
    /// <summary>
    /// This takes a sorted, possible rotated about an unkown pivot k and returns
    /// the index of the target value or -1 if the target value is not in the array
    /// </summary>
    public static int SearchRotated(int[] nums, int target)
    {
        int rotationIndex = FindRotationIndex(nums);
        int max, min, mid;

        if (rotationIndex != -1)
        {
            if (nums[rotationIndex] == target)
            {
                return nums[rotationIndex];
            }
            else if (target > nums[rotationIndex] || target < nums[rotationIndex + 1])
            {
                return -1;
            }
            else if (target < nums[rotationIndex] && target >= nums[0])
            {
                min = 0;
                max = rotationIndex - 1;
            }
            else
            {
                min = rotationIndex + 1;
                max = nums.Length - 1;
            }
        }
        else
        {
            min = 0;
            max = nums.Length - 1;
        }

        // Now, we perform a traditional binary search since the array is assumed to be sorted
        while (min <= max)
        {
            mid = (min + max) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (target < nums[mid])
            {
                max = mid - 1;
            }
            else if (target > nums[mid])
            {
                min = mid + 1;
            }
        }

        // If the target is not found
        return -1;

    }

    /// <summary>
    /// Takes in a sorted, possibly rotated array at an unkown index k 
    /// such that 1 <= k < "rotatedArray.Length" and the resulting array is 
    /// [nums[k], nums[k+1], ..., nums[n - 1], nums[0], nums[1], ..., nums[k-1]] and from it,
    /// returns k if the array is rotated or -1 if it is not
    /// </summary>
    static int FindRotationIndex(int[] rotatedArray)
    {
        int current = rotatedArray[0];
        for (int i = 0; i < rotatedArray.Length - 1; i++)
        {
            if (current > rotatedArray[i + 1]) return i;
            current = rotatedArray[i + 1];
        }
        return -1;
    }

    // Returns the number of elements in nums greater than or equal to mid
    // The valueOccurrenceMap ensures that repeated elements are counted only once
    // Used with the Kth largest algorithm
    static int CountGreater(int[] nums, int mid)
    {
        int count = 0;
        Dictionary<int, int> valueOccurrenceMap = new();

        for (int i = 0; i < nums.Length; i++)
        {
            if (valueOccurrenceMap.ContainsKey(nums[i]))
                valueOccurrenceMap[nums[i]] += 1;
            else
                valueOccurrenceMap.Add(nums[i], 1);

            if (nums[i] >= mid && valueOccurrenceMap[nums[i]] == 1)
                count++;
        }
            

        return count;
    }

    /// <summary>
    /// Takes in an unsorted array of integers and an integer k and returns the kth largest element in sorted order,
    /// without sorting the array
    /// </summary>
    /// <param name="nums">An unsorted array of integers</param>
    /// <param name="k">A value k such that 1 <= k <= nums.Length</param>
    /// <returns>
    /// The kth largest element in sorted order,
    /// without sorting the array or -1 if k is out of range, or if the value is not found 
    /// </returns>
    public static int KthLargest(int[] nums, int k)
    {
        if (k < 1 || k > nums.Length) return -1;

        int min = nums[0];
        int max = nums[0];

        // calculate minimum and maximum the array.
        for (int i = 0; i < nums.Length; i++)
        {
            min = Math.Min(min, nums[i]);
            max = Math.Max(max, nums[i]);
        }

        // Our answer range lies between minimum
        // and maximum element of the array on which Binary
        // Search is applied
        while (min <= max)
        {
            int mid = (min + max) / 2;

            int countGreater = CountGreater(nums, mid);
            if (countGreater > k)
                min = mid + 1;
            else if (countGreater == k)
                return mid;
            else
                max = mid - 1;
        }

        return -1;
    }

    /// <summary>
    /// Takes in an array sorted in non-descending order and returns a list
    /// containing the indices of the two numbers in that array that add up to the target.
    /// One may not use the same element twice.
    /// </summary>
    /// <param name="nums">An array sorted in non-descending order.</param>
    /// <param name="target">The value which two numbers in the array are supposed to add up to.</param>
    /// <returns></returns>
    public static List<int> TwoSum(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; i < nums.Length; j++)
            {
                int sum = nums[i] + nums[j];
                if (sum == target) return new List<int> {i + 1, j + 1};
                if (sum > target) break;
            }
        }

        return new List<int> { };
    }

    /// <summary>
    /// Using the two pointer technique
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    // public static int[] TwoSum(int[] nums, int target)
    // {
        
    // }


    [GeneratedRegex(@"[A-Za-z\d]")]
    private static partial Regex MyRegex();

    public static bool ValidPalindrome(string s)
    {
        if (s.Length == 0) return false;

        StringBuilder sb = new ();
        Regex regex = MyRegex();

        foreach (char c in s) 
        { 
            if (regex.IsMatch(c.ToString()))
            {
                sb.Append(c.ToString().ToLower());
            }
        }

        s = sb.ToString();

        int i = 0;
        int j = s.Length - 1;

        while (i < s.Length)
        {
            if (s[i] != s[j]) return false;
            i++;
            j--;
        }

        return true;
    }

    public static string PrintElements(this List<int> nums)
    {
        StringBuilder sb = new("[");
        for (int i = 0; i < nums.Count - 1; i++)
        {
            sb.Append(nums[i] + ", ");
        }
        sb.Append($"{nums[^1]}]");

        return sb.ToString();
    }

}