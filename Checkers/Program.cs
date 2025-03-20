using Checkers;

class Program
{
  static void Main()
  {
    Display display = new();
    Display.ShowMessage("Welcome to Checkers!");

    Console.Write("Enter Player 1 name: ");
    string? player1Input = Console.ReadLine();
    string player1Name = string.IsNullOrWhiteSpace(player1Input) ? "Player 1" : player1Input;

    string player1Color = display.ChooseColor();

    Console.Write("Enter Player 2 name: ");
    string? player2Input = Console.ReadLine();
    string player2Name = string.IsNullOrWhiteSpace(player2Input) ? "Player 2" : player2Input;

    Player playerBlack, playerWhite;

    if (player1Color.ToLower() == "black")
    {
      playerBlack = new Player(player1Name);
      playerWhite = new Player(player2Name);
    }
    else
    {
      playerBlack = new Player(player2Name);
      playerWhite = new Player(player1Name);
    }

    Display.ShowMessage("\nPlayers created successfully!");
    Display.ShowMessage($"Black: {playerBlack.Name}");
    Display.ShowMessage($"White: {playerWhite.Name}");

    Display.ShowMessage("\nPress any key to continue...");
    Console.ReadKey();

    Board board = new(8);

    GameController controller = new(playerBlack, playerWhite, board, display);

    controller.OnGameEnd = (winner) =>
    {
      Display.ShowMessage($"{winner.Name} wins!");
    };

    controller.OnTurnChanged = (player, pieces) =>
    {
      string symbol = player == playerBlack ? "O" : "X";
      Display.ShowMessage($"\n\n{player.Name}'s ({symbol}) turn. Pieces left: {pieces.Count}");
    };

    controller.StartGame();
  }
}