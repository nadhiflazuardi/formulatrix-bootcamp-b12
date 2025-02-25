int n = 15;

for (int i = 1; i <= n; i++)
{
  if (i % 3 == 0 && i % 5 == 0)
  {
    Console.WriteLine("foobar");
  }
  else if (i % 3 == 0)
  {
    Console.WriteLine("foo");
  }
  else if (i % 5 == 0)
  {
    Console.WriteLine("bar");
  }
  else
  {
    Console.WriteLine(i);
  }
}