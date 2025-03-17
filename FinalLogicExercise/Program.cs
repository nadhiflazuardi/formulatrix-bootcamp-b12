class FooBar
{
  private Dictionary<int, string> _rules;
  public FooBar()
  {
    _rules = new Dictionary<int, string>
    {
      { 3, "foo" },
      { 4, "baz" },
      { 5, "bar" },
      { 7, "jazz" },
      { 9, "huzz" }
    };
  }

  public void Print(int n)
  {
    for (int i = 1; i <= n; i++)
    {
      string output = "";

      // sort the keys so that user's new rules
      // doesn't stack up at the end
      List<int> sortedKeys = new(_rules.Keys);
      sortedKeys.Sort();

      foreach (int key in sortedKeys)
      {
        if (i % key == 0)
        {
          output += _rules[key];
        }
      }

      if (!String.IsNullOrEmpty(output))
      {
        Console.WriteLine(output);
        continue;
      }

      Console.WriteLine(i);
    }
  }

  public void AddRule(int input, string output)
  {
    _rules.Add(input, output);
  }
}

class Program
{
  static void Main()
  {
    FooBar myClass = new();
    myClass.Print(60);
    myClass.AddRule(10, "tenzz");
    myClass.Print(60);
    myClass.AddRule(6, "sixzz");
    myClass.Print(60);
  }
}