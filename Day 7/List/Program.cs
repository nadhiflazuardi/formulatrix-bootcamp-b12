class Program
{
  static void DisplayList(List<int> list)
  {
    Console.WriteLine(string.Join(", ", list));
  }

  static void Main()
  {
    List<int> numbers = new List<int>();

    numbers.Add(10);
    numbers.Add(20);
    numbers.Add(30);
    Console.WriteLine("List after adding elements:");
    DisplayList(numbers);

    numbers.Remove(20);
    Console.WriteLine("List after removing 20:");
    DisplayList(numbers);

    numbers.Insert(1, 25); 
    Console.WriteLine("List after inserting 25 at index 1:");
    DisplayList(numbers);

    int[] array = numbers.ToArray();
    Console.WriteLine("Array elements:");
    Console.WriteLine(string.Join(", ", array));

    int searchElement = 90;
    int index = numbers.IndexOf(searchElement);
    Console.WriteLine(index != -1 ? $"Element {searchElement} found at index {index}" : $"Element with index {index} not found");

    numbers.Sort();
    Console.WriteLine("List after sorting:");
    DisplayList(numbers);
  }
}
