using System;
using System.Collections.Generic;

public class RandomCollection<T>
{
    private readonly List<T> _items = new List<T>();
    private readonly Random _random = new Random();

    public void Add(T element)
    {
        if (_random.Next(2) == 0)
        {
            _items.Insert(0, element);
        }
        else
        {
            _items.Add(element);
        }
    }

    public T Get(int index)
    {
        if (IsEmpty())
            throw new InvalidOperationException("Collection is empty.");

        if (index < 0 || index >= _items.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of collection bound.");

        int randomIndex = _random.Next(0, index + 1);
        
        return _items[randomIndex];
    }

    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    public void PrintAll()
    {
        Console.WriteLine($"Collection: [{string.Join(", ", _items)}]");
    }
}


class SatisfactoryProgram
{
    static void OldMain(string[] args)
    {
        // --------TASK 1: Lambda Leap Year--------

        Func<int, bool> isLeapYear = year => DateTime.IsLeapYear(year);

        Console.WriteLine("--------TASK 1: Lambda Leap Year--------");
        Console.WriteLine($"Is 2024 a leap year? {isLeapYear(2024)}");
        Console.WriteLine($"Is 2023 a leap year? {isLeapYear(2023)}");

        // --------TASK 2--------

        Console.WriteLine("--------TASK 2: RandomCollection--------");

        RandomCollection<string> myCollection = new RandomCollection<string>();

        Console.WriteLine($"Is the collection initially empty? {myCollection.IsEmpty()}");

        myCollection.Add("Adam");
        myCollection.Add("Milosz");
        myCollection.Add("Aleks");
        myCollection.Add("Kinga");

        myCollection.PrintAll();

        Console.WriteLine($"Random element from indices 0-2: {myCollection.Get(2)}");
        Console.WriteLine($"Is the collection empty? {myCollection.IsEmpty()}");
    }
}