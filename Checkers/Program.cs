using Checkers;

class Program
{
  static void Main()
  {
    Display.ShowMessage("Welcome to Checkers!");

    Console.Write("Enter Player 1 name: ");
    string? player1Input = Console.ReadLine();
    string player1Name = string.IsNullOrWhiteSpace(player1Input) ? "Player 1" : player1Input;

    Console.Write("Enter Player 2 name: ");
    string? player2Input = Console.ReadLine();
    string player2Name = string.IsNullOrWhiteSpace(player2Input) ? "Player 2" : player2Input;

    Player player1 = new(player1Name);
    Player player2 = new(player2Name);

    Display.ShowMessage("\nPlayers created successfully!");
    Display.ShowMessage($"Player 1: {player1.Name}");
    Display.ShowMessage($"Player 2: {player2.Name}");

    Display.ShowMessage("\nPress any key to continue...");
    Console.ReadKey();

    Board board = new(8);
    Display display = new();

    GameController controller = new(player1, player2, board, display);

    controller.OnGameEnd = (winner) =>
    {
      Display.ShowMessage($"{winner.Name} wins!");
    };

    controller.OnTurnChanged = (player, pieces) =>
    {
      string symbol = player == player1 ? "O" : "X";
      Display.ShowMessage($"\n\n{player.Name}'s ({symbol}) turn. Pieces left: {pieces.Count}");
    };

    controller.StartGame();
  }
}