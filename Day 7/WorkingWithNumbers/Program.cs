class Casting
{
  public static void ImplicitCast()
  {
    int i = 23;
    double d = i;

    Console.WriteLine(i.GetType());
    Console.WriteLine(d.GetType());
  }

  public static void ExplicitCast()
  {
    double d = 23.5;
    int i = (int)d;

    Console.WriteLine(d.GetType());
    Console.WriteLine(i.GetType());
  }
}

class Rounding {
  public static void WithoutMath() {
    double d = 52.15;
    int i = Convert.ToInt16(d);

    Console.WriteLine(i);
  }


}

class Program {
  static void Main() {
    Casting.ImplicitCast();
    Console.WriteLine();
    Casting.ExplicitCast();
    Console.WriteLine();

    double d = 52.15;
    Console.WriteLine(Convert.ToInt16(d));
    Console.WriteLine(Math.Round(d));
    Console.WriteLine(Math.Floor(d));
    Console.WriteLine(Math.Ceiling(d));

    Console.WriteLine();
    Console.WriteLine(-d); 
    Console.WriteLine(Math.Abs(-d)); 
    Console.WriteLine(Math.Sqrt(d)); 
    Console.WriteLine(Math.Pow(2, d));
    Console.WriteLine(Math.Log(d, 10));
  }
}