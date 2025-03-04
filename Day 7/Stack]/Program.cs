class Program
{
  static void DisplayStack(Stack<string> stack)
  {
    Console.WriteLine(string.Join(", ", stack));
  }

  static void Main()
  {
    Stack<string> stack = new();

    stack.Push("Apple");
    stack.Push("Banana");
    stack.Push("Cherry");
    Console.WriteLine("Stack after pushing elements:");
    DisplayStack(stack);

    string removedItem = stack.Pop();
    Console.WriteLine($"Popped: {removedItem}");
    Console.WriteLine("Stack after popping an element:");
    DisplayStack(stack);

    Console.WriteLine($"Top of the stack: {stack.Peek()}");

    string searchItem = "Banana";
    Console.WriteLine(stack.Contains(searchItem) ? $"{searchItem} is in the stack." : $"{searchItem} is not in the stack.");

    Console.WriteLine($"Stack count: {stack.Count}");
  }
}
