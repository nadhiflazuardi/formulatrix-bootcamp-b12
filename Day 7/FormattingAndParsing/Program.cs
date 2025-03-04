class Formatting
{
  public static void Format()
  {
    Console.WriteLine(true.ToString());
  }
}

class Parsing
{
  public static void Parse()
  {
    int integerr;
    Console.WriteLine();
    Console.WriteLine(int.Parse("24"));
    Console.WriteLine(int.TryParse("24", out integerr));
    Console.WriteLine(integerr);
  }
}

class Program
{
  static void Main()
  {
    Formatting.Format();
    Parsing.Parse();
  }
}