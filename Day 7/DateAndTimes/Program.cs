class CSTimeSpan {
  public static void Constructing() {
    Console.WriteLine(new TimeSpan(2, 30, 0));
    Console.WriteLine(TimeSpan.FromHours(2.5));
    Console.WriteLine(TimeSpan.FromMinutes(150));
  }

  public static void Operations() {
    TimeSpan span1 = TimeSpan.FromHours(2);
    TimeSpan span2 = TimeSpan.FromMinutes(30);
    TimeSpan result = span1 + span2;
    Console.WriteLine("\n"+result);
  }
}

class CSDateTime {
  public static void Construction() {
    DateTime dt1 = new DateTime(2021, 12, 15, 8, 30, 0, DateTimeKind.Local);

    Console.WriteLine("\n" + dt1);
  }

  public static void WorkingWithDateTime() {
    DateTime dt1 = new DateTime(2021, 1, 1);

    DateTime dt3 = dt1.AddDays(10);

    Console.WriteLine("\n" + dt1);
    Console.WriteLine(dt3);
  }
}

class Program {
  static void Main() {
    // CSTimeSpan.Constructing();
    // CSTimeSpan.Operations();

    CSDateTime.Construction();
    CSDateTime.WorkingWithDateTime();
  }
}