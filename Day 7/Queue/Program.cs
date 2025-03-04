class Program
{
  static void DisplayQueue(Queue<string> queue)
  {
    Console.WriteLine(string.Join(", ", queue));
  }
  static void Main()
  {
    Queue<string> queue = new Queue<string>();

    queue.Enqueue("Apple");
    queue.Enqueue("Banana");
    queue.Enqueue("Cherry");
    Console.WriteLine("Queue after enqueuing elements:");
    DisplayQueue(queue);

    string removedItem = queue.Dequeue();
    Console.WriteLine($"Dequeued: {removedItem}");
    Console.WriteLine("Queue after dequeuing an element:");
    DisplayQueue(queue);

    Console.WriteLine($"Front of the queue: {queue.Peek()}");

    string searchItem = "Banana";
    Console.WriteLine(queue.Contains(searchItem) ? $"{searchItem} is in the queue." : $"{searchItem} is not in the queue.");

    Console.WriteLine($"Queue count: {queue.Count}");
  }
}