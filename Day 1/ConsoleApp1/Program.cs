// See https://aka.ms/new-console-template for more information
int x = int.MaxValue;
int y = unchecked(x + x);  // Will not throw an exception
Console.WriteLine(y);
