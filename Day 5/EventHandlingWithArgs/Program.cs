class CounterEventArgs : EventArgs
{
  public int CurrentValue { get; set; }
  public int ThresholdValue { get; set; }
  public DateTime TimeReached { get; set; }
}

class Counter
{
  private int _threshold;
  private int _total;

  public event EventHandler<CounterEventArgs> ThresholdReached;

  public Counter(int threshold)
  {
    _threshold = threshold;
    _total = 0;
  }

  public void Add(int amount)
  {
    _total += amount;
    Console.WriteLine($"Current count: {_total}");

    if (_total >= _threshold)
    {
      CounterEventArgs args = new CounterEventArgs
      {
        CurrentValue = _total,
        ThresholdValue = _threshold,
        TimeReached = DateTime.Now
      };

      OnThresholdReached(args);
    }
  }

  protected virtual void OnThresholdReached(CounterEventArgs e)
  {
    ThresholdReached?.Invoke(this, e);
  }
}

class Program
{
  static void Counter_ThresholdReached(object sender, CounterEventArgs e)
  {
    Console.WriteLine($"\n=== THRESHOLD REACHED EVENT ===");
    Console.WriteLine($"Time: {e.TimeReached}");
    Console.WriteLine($"Current Value: {e.CurrentValue}");
    Console.WriteLine($"Threshold: {e.ThresholdValue}");
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