using DSA;

var myList = new LinkedList();

for (int i = 0; i < 12; i++)
{
    myList.Insert((int)Random.Shared.NextInt64(0, 100));
}

Console.WriteLine($"Length: {myList.Length}");
Console.WriteLine($"Unsorted: {myList}");

myList.Sort();
Console.WriteLine($"Sorted: {myList}");


