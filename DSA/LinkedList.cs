using System.Text;

namespace DSA;

/// <summary>
/// A simple linked list implementation that can only hold integers of only integers.
/// Originally made only to try out the merge sort algorithm for linked lists.
/// </summary>
public class LinkedList
{
    public class Node
    {
        public int Data { get; set; }
        public Node? Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node? _head;


    /// <summary>
    /// Represents the number of elements currently in the list
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Adds data to the end of the linked list. Increases the length of the list by one.
    /// </summary>
    /// <param name="data">The item to be added</param>
    public void Insert(int data)
    {
        if (_head == null)
        {
            _head = new Node(data);
        }
        else
        {
            var current = _head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = new Node(data);
        }

        Length += 1;
    }

    /// <summary>
    /// Sorts the list in ascending order. The original list is mutated.
    /// It uses merge sort to do the sorting.
    /// </summary>
    public void Sort()
    {
        var newHead = MergeSort(_head);
        _head = newHead;
    }


    /// <summary>
    /// Merges two sorted linked lists
    /// </summary>
    /// <param name="head">A reference to the head of the first linked list</param>
    /// <param name="otherHead">A reference to the head of the second linked list</param>
    /// <returns>The head to the merged linked list</returns>
    private static Node? Merge(Node? head, Node? otherHead)
    {
        // New node with some dummy data
        var temp = new Node(-1);
        var merged = temp;

        // Iterate until one of the lists is exhausted
        while (head != null && otherHead != null)
        {
            if (head.Data < otherHead.Data)
            {
                temp.Next = head;
                head = head.Next;
            }
            else
            {
                temp.Next = otherHead;
                otherHead = otherHead.Next;
            }

            temp = temp.Next;
        }

        // Merge the elements of the remaining list (if any). Since both lists were sorted,
        // the remaining elements can just be added to the end of the merged list

        while (head != null)
        {
            temp.Next = head;
            head = head.Next;
            temp = temp.Next;
        }

        while (otherHead != null)
        {
            temp.Next = otherHead;
            otherHead = otherHead.Next;
            temp = temp.Next;
        }

        return merged.Next; // Remember, we have to skip over the first node with the dummy data
    }

    /// <summary>
    /// Finds the node at the middle position of the list
    /// </summary>
    /// <param name="head">The head of the linked list</param>
    /// <returns>The node at the middle position of the list</returns>
    private static Node? FindMid(Node? head)
    {
        if (head == null) return head;

        var slow = head;
        var fast = head.Next;

        while (fast?.Next != null)
        {
            slow = slow?.Next;
            fast = fast.Next.Next;
        }

        return slow;
    }

    private static Node? MergeSort(Node? head)
    {
        // 1. If the size of the list is 1 (or zero) return the head
        if (head?.Next == null) return head;

        // 2. Find mid using the Tortoise and Hare approach
        var mid = FindMid(head);

        // 3. Store the next of mid as the head of the right sub-list
        var otherHead = mid?.Next;

        // 4. Make mid point to null, effectively splitting the list into two
        if (mid != null) mid.Next = null;

        // 5. Call MergeSort recursively on both of the sub-lists and store the results
        var newHead = MergeSort(head);
        var newOtherHead = MergeSort(otherHead);

        // 6. Call Merge on the new heads
        var mergedHead = Merge(newHead, newOtherHead);

        // 7. Return the merged head
        return mergedHead;
    }

    public override string ToString()
    {
        StringBuilder sb = new("[");

        var current = _head;

        while (current != null)
        {
            if (current.Next == null) sb.Append(current.Data);
            else sb.Append(current.Data + ", ");

            current = current.Next;
        }

        sb.Append(']');

        return sb.ToString();
    }
}