int Calc(int x)
{
  return 10 / x;
}

try {
  Console.WriteLine("Trying to divide by zero.");
  int result = Calc(0);
  Console.WriteLine($"Result: {result}");
} catch (DivideByZeroException ex) {
  Console.WriteLine($"Error caught: {ex.Message}");
} finally {
  Console.WriteLine("This code block will always execute.");
}

Console.WriteLine("End of program.");