class Counter
{
  private int _threshold;
  private int _total;

  public Counter(int threshold)
  {
    _threshold = threshold;
    _total = 0;
  }

  public event EventHandler ThresholdReached;

  public void Add(int amount)
  {
    _total += amount;
    Console.WriteLine($"Current count: {_total}");

    if (_total >= _threshold && ThresholdReached != null)
    {
      OnThresholdReached(EventArgs.Empty);
    }
  }

  protected virtual void OnThresholdReached(EventArgs e)
  {
    ThresholdReached?.Invoke(this, e);
  }
}

class Program
{
  static void Counter_ThresholdReached(object sender, EventArgs e)
  {
    Console.WriteLine("The threshold was reached!");
    Console.WriteLine("Taking action in response to the event...");
  }

  static void Main()
  {
    Counter counter = new(10);
    counter.ThresholdReached += Counter_ThresholdReached;

    Console.WriteLine("Adding values to counter...");
    counter.Add(5);
    counter.Add(4);
    counter.Add(3);

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
  }
}