string[] coinFaces = ["heads", "tails"];
int selectedCoinFace = 0;

Console.WriteLine("Insert Player 1's name:");
string player1 = Console.ReadLine() ?? "Player 1";
Console.WriteLine("Insert Player 2's name:");
string player2 = Console.ReadLine() ?? "Player 2";

while (true)
{

  Console.Clear();
  Console.WriteLine($"{player1} as Player 1 will choose coin face to decide who gets the first turn.");

  for (int i = 0; i < coinFaces.Length; i++)
  {
    if (i == selectedCoinFace)
    {
      Console.ForegroundColor = ConsoleColor.Black;
      Console.BackgroundColor = ConsoleColor.White;
    }

    Console.WriteLine($"{coinFaces[i]}");

    Console.ResetColor();
  }

  ConsoleKeyInfo keyInfo = Console.ReadKey(true);

  if (keyInfo.Key == ConsoleKey.UpArrow && selectedCoinFace > 0)
  {
    selectedCoinFace--;
  }
  else if (keyInfo.Key == ConsoleKey.DownArrow && selectedCoinFace < coinFaces.Length - 1)
  {
    selectedCoinFace++;
  }
  else if (keyInfo.Key == ConsoleKey.Enter)
  {
    Console.WriteLine($"{player1} chose {coinFaces[selectedCoinFace]}");

    Random coin = new();
    int flip = coin.Next(2);

    Console.WriteLine($"The coin landed on {coinFaces[flip]}.");

    if (selectedCoinFace == flip)
    {
      Console.WriteLine($"{player1} chose correctly. {player1} will go first.");
    }
    else
    {
      Console.WriteLine($"{player1} chose incorrectly. {player2} will go first.");
    }

    break;
  }
}
