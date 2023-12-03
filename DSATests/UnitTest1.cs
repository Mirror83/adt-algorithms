namespace DSATests;
using DSA;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestPower()
    {
        Assert.That(Recursion.Power(2, 5), Is.EqualTo(32));
    }
    
    [Test]
    public void TestReverse()
    {
        var list = new List<int>();

        for (var i = 0; i < 100; i++)
        {
            list.Add(Random.Shared.Next());
        }

        var listCopy = list.TakeLast(list.Count).ToList();
        
        Recursion.Reverse(list, 0, list.Count);
        
        for (var i = 1; i < list.Count + 1; i++)
        {
            Assert.That(list[i - 1], Is.EqualTo(listCopy[^i]));
        }
    }
    
}