using Checkers;

class Program
{
  static void Main()
  {
    Console.WriteLine("Welcome to Checkers!");

    // Ask for Player 1 name
    Console.Write("Enter Player 1 name: ");
    string? player1Input = Console.ReadLine();
    string player1Name = string.IsNullOrWhiteSpace(player1Input) ? "Player 1" : player1Input;

    // Ask for Player 2 name
    Console.Write("Enter Player 2 name: ");
    string? player2Input = Console.ReadLine();
    string player2Name = string.IsNullOrWhiteSpace(player2Input) ? "Player 2" : player2Input;

    // Create instances of Player class
    Player player1 = new Player(player1Name);
    Player player2 = new Player(player2Name);

    // Display confirmation
    Console.WriteLine("\nPlayers created successfully!");
    Console.WriteLine($"Player 1: {player1.Name}");
    Console.WriteLine($"Player 2: {player2.Name}");

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();

    Board board = new();
    Display display = new();

    GameController controller = new(player1, player2, board, display);
  }
}