﻿string[] options = ["Rock", "Paper", "Scissors"];
int playerOneHand = 0;
int playerTwoHand = 0;

while (true)
{
  Console.Clear();
  Console.WriteLine("Use ↑ ↓ to move, ENTER to select");
  Console.WriteLine("Player 1, please choose your hand:");

  for (int i = 0; i < options.Length; i++)
  {
    if (i == playerOneHand)
    {
      Console.ForegroundColor = ConsoleColor.Black;
      Console.BackgroundColor = ConsoleColor.White;
    }

    Console.WriteLine($"  {options[i]}  ");

    Console.ResetColor();
  }

  ConsoleKeyInfo keyInfo = Console.ReadKey(true);

  if (keyInfo.Key == ConsoleKey.UpArrow && playerOneHand > 0)
  {
    playerOneHand--;
  }
  else if (keyInfo.Key == ConsoleKey.DownArrow && playerOneHand < options.Length - 1)
  {
    playerOneHand++;
  }
  else if (keyInfo.Key == ConsoleKey.Enter)
  {
    break;
  }
}

while (true)
{
  Console.Clear();
  Console.WriteLine("Use ↑ ↓ to move, ENTER to select");
  Console.WriteLine("Player 2, please choose your hand:");

  for (int i = 0; i < options.Length; i++)
  {
    if (i == playerTwoHand)
    {
      Console.ForegroundColor = ConsoleColor.Black;
      Console.BackgroundColor = ConsoleColor.White;
    }

    Console.WriteLine($"  {options[i]}  ");

    Console.ResetColor();
  }

  ConsoleKeyInfo keyInfo = Console.ReadKey(true);

  if (keyInfo.Key == ConsoleKey.UpArrow && playerTwoHand > 0)
  {
    playerTwoHand--;
  }
  else if (keyInfo.Key == ConsoleKey.DownArrow && playerTwoHand < options.Length - 1)
  {
    playerTwoHand++;
  }
  else if (keyInfo.Key == ConsoleKey.Enter)
  {
    break;
  }
}

Console.Clear();

if (playerOneHand == playerTwoHand)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. It's a draw!");
}
else if (playerOneHand == 0 && playerTwoHand == 1)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. Player 2 wins!");
}
else if (playerOneHand == 0 && playerTwoHand == 2)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. Player 1 wins!");
}
else if (playerOneHand == 1 && playerTwoHand == 0)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. Player 1 wins!");
}
else if (playerOneHand == 1 && playerTwoHand == 2)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. Player 2 wins!");
}
else if (playerOneHand == 2 && playerTwoHand == 0)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. Player 2 wins!");
}
else if (playerOneHand == 2 && playerTwoHand == 1)
{
  Console.WriteLine($"Player 1 chose {options[playerOneHand]} and Player 2 chose {options[playerTwoHand]}. Player 1 wins!");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey(true);