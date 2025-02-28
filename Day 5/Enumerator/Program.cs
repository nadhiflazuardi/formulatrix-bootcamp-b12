foreach (char c in "foo")
{
  Console.WriteLine(c);
}

Console.WriteLine("=========");

using (var enumerator = "bar".GetEnumerator())
{
  while (enumerator.MoveNext())
  {
    var element = enumerator.Current;
    Console.WriteLine(element);
  }
}