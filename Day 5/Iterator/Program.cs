class FibonacciSequence
{
  static void Main()
  {
    IEnumerable<int> Fibs(int reqFib)
    {
      int prevFib = 0, curFib = 1;
      for (int i = 0; i < reqFib; i++)
      {
        yield return prevFib;
        int newFib = prevFib + curFib;
        prevFib = curFib;
        curFib = newFib;
      }
    }

    foreach (int fib in Fibs(6))
    {
      Console.WriteLine(fib + " ");
    }
  }
}