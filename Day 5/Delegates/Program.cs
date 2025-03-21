﻿class Program
{
  public static void DelegateMethod(string message)
  {
    Console.WriteLine(message);
  }

  public static void DelegateMethod2(string message) {
    Console.WriteLine("Success!");
  }

  public static void MethodWithCallback(int param1, int param2, Action<string> callback)
  {
    callback("The number is: " + (param1 + param2).ToString());
  }

  static void Main()
  {
    Action<string> handler = DelegateMethod;
    handler += DelegateMethod2;
    MethodWithCallback(1, 2, handler);
  }
}