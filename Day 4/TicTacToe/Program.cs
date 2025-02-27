string[] grid = ["1", "2", "3", "4", "5", "6", "7", "8", "9"];
bool isPlayer1Turn = true;
int turn = 0;

void DrawBoard()
{
  for (int i = 0; i < 3; i++)
  {
    for (int j = 0; j < 3; j++)
    {
      Console.Write($"| {grid[j + i * 3]} |");
    }

    if (i < 2)
    {
      Console.WriteLine("\n---------------");
    }
  }
}

bool CheckWinner()
{
  bool row1 = grid[0] == grid[1] && grid[1] == grid[2];
  bool row2 = grid[3] == grid[4] && grid[4] == grid[5];
  bool row3 = grid[6] == grid[7] && grid[7] == grid[8];
  bool col1 = grid[0] == grid[3] && grid[3] == grid[6];
  bool col2 = grid[1] == grid[4] && grid[4] == grid[7];
  bool col3 = grid[2] == grid[5] && grid[5] == grid[8];
  bool diag1 = grid[0] == grid[4] && grid[4] == grid[8];
  bool diag2 = grid[2] == grid[4] && grid[4] == grid[6];

  return row1 || row2 || row3 || col1 || col2 || col3 || diag1 || diag2;
}

while (!CheckWinner() && turn < 9)
{
  DrawBoard();

  Console.WriteLine("\n\nEnter between 1-9");
  string input = Console.ReadLine();

  if (grid.Contains(input) && input != "X" && input != "O")
  {
    int gridIndex = Convert.ToInt32(input) - 1;

    if (isPlayer1Turn)
    {
      grid[gridIndex] = "X";
    }
    else
    {
      grid[gridIndex] = "O";
    }

    turn++;
  }
  else
  {
    Console.WriteLine("Invalid input! Your turn will be skipped!");
  }

  isPlayer1Turn = !isPlayer1Turn;
}

if (CheckWinner())
{
  isPlayer1Turn = !isPlayer1Turn;
  if (isPlayer1Turn)
  {
    Console.WriteLine("Player 1 wins!");
  }
  else
  {
    Console.WriteLine("Player 2 wins!");
  }
}
else
{
  Console.WriteLine("Tie!");
}