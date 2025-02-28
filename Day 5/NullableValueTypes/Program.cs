// class CheckNullability
// {
//   static void Main()
//   {
//     int? i = null;
//     Console.WriteLine(i == null);
//   }
// }

// class HasValue
// {
//   static void Main()
//   {
//     int? x = 20;

//     if (x.HasValue)
//     {
//       Console.WriteLine(x);
//       Console.WriteLine(x.Value);
//       Console.WriteLine(x == x.Value);
//     } else {
//       Console.WriteLine("x is null");
//     }
//   }
// }

// class NullCoalescing
// {
//   static void Main()
//   {
//     int? x = null;
//     int y = x ?? 5;
//     Console.WriteLine(y);

//     int? a = null, b = 1, c = 2;
//     Console.WriteLine(a ?? b ?? c);
//   }
// }

class NullConversion
{
  static void Main()
  {
    object o = "string";
    int? x = o as int?;
    Console.WriteLine(x.HasValue);
  }
}