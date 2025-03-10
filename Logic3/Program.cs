class Program
{
  static void Foobazbarjazzhuzz(int n)
  {
    for (int i = 1; i <= n; i++)
    {
      string output = "";
      if (i % 3 == 0)
      {
        output += "foo";
      }
      if (i % 4 == 0)
      {
        output += "baz";
      }
      if (i % 5 == 0)
      {
        output += "bar";
      }
      if (i % 7 == 0)
      {
        output += "jazz";
      }
      if (i % 9 == 0)
      {
        output += "huzz";
      }

      if (!String.IsNullOrEmpty(output))
      {
        Console.WriteLine(output);
        continue;
      }

      Console.WriteLine(i);
    }
  }

  static void Main()
  {
    Foobazbarjazzhuzz(36);
  }
}