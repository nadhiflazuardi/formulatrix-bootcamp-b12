class MultidimensionalArray
{
  public static void Matrix()
  {
    int[,] matrix = new int[2, 2];
    matrix[0, 0] = 1;
    matrix[0, 1] = 2;
    matrix[1, 0] = 3;
    matrix[1, 1] = 4;

    Console.WriteLine(matrix[1, 1]);
  }
}

class Searching
{
  public static void Search()
  {
    int[] numbers = { 1, 2, 3, 4, 5 };
    int index = Array.BinarySearch(numbers, 3);
    Console.WriteLine(index);
    Console.WriteLine();
  }
}

class Sorting
{
  public static void Sort()
  {
    int[] numbers = { 3, 1, 4, 2, 5 };
    Array.Sort(numbers);

    foreach (int number in numbers)
    {
      Console.WriteLine(number);
    }
    Console.WriteLine();
  }
}

class Program
{
  static void Main()
  {
    MultidimensionalArray.Matrix();
    Searching.Search();
    Sorting.Sort();
  }
}