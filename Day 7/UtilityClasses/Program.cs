using System.Diagnostics;

class CSConsole
{
  public static void ControllingConsole()
  {
    Console.WindowWidth = 100;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.CursorLeft = 10;
    Console.CursorTop = 5;
    Console.WriteLine("Welcome to the console!");
  }

  public static void RedirectOutput()
  {
    TextWriter oldOut = Console.Out;

    using (TextWriter writer = File.CreateText("output.txt"))
    {
      Console.SetOut(writer);
      Console.WriteLine("This goes into the file");
    }

    Console.SetOut(oldOut);
  }
}

class CSEnvironment
{
  public static void AcessingProperties()
  {
    Console.WriteLine("Machine Name: " + Environment.MachineName);
    Console.WriteLine("OS Version: " + Environment.OSVersion);
    Console.WriteLine("Current User: " + Environment.UserName);
    Console.WriteLine($"Tick count: {Environment.TickCount}");
  }

  public static void AccessingEnvironmentVariables()
  {
    string path = Environment.GetEnvironmentVariable("PATH");
    Console.WriteLine("System PATH: " + path);

    var path2 = Environment.GetEnvironmentVariables();
    Console.WriteLine($"\nPATH2: {path2}");
  }
}

class CSProcess
{
  public static void StartingProcess()
  {
    Process.Start("notepad.exe", "e:\\file.txt");
  }

  public static void CapturingOutput()
  {
    ProcessStartInfo psi = new ProcessStartInfo
    {
      FileName = "cmd.exe",
      Arguments = "/c ipconfig /all",
      RedirectStandardOutput = true,
      UseShellExecute = false
    };

    Process p = Process.Start(psi);
    string result = p.StandardOutput.ReadToEnd();
    Console.WriteLine(result);
  }
}

class Program
{
  static void Main()
  {
    // CSConsole.ControllingConsole();
    // CSConsole.RedirectOutput();

    // CSEnvironment.AcessingProperties();
    // CSEnvironment.AccessingEnvironmentVariables();

    // CSProcess.StartingProcess();
    CSProcess.CapturingOutput();
  }
}