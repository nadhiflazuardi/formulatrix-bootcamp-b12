using Checkers;

class Program
{
  static void Main()
  {
    Display display = new();
    Display.ShowMessage(@"
  ______  __    __   _______   ______  __  ___  _______ .______          _______.
 /      ||  |  |  | |   ____| /      ||  |/  / |   ____||   _  \        /       |
|  ,----'|  |__|  | |  |__   |  ,----'|  '  /  |  |__   |  |_)  |      |   (----`
|  |     |   __   | |   __|  |  |     |    <   |   __|  |      /        \   \    
|  `----.|  |  |  | |  |____ |  `----.|  .  \  |  |____ |  |\  \----.----)   |   
 \______||__|  |__| |_______| \______||__|\__\ |_______|| _| `._____|_______/    
    ");

    Display.ShowInlineMessage("\nEnter Player 1 name: ");
    string? player1Input = Console.ReadLine();
    string player1Name = string.IsNullOrWhiteSpace(player1Input) ? "Player 1" : player1Input;

    string player1Color = display.ChooseColor();

    Display.ShowInlineMessage("\nEnter Player 2 name: ");
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
      Display.ShowMessage($"\n\n{winner.Name} wins!");
    };

    controller.OnTurnChanged = (player1, player2, pieceLeft1, pieceLeft2) =>
    {
      Display.ShowMessage("\n\nPieces Left:");
      Display.ShowMessage($"{player1.Name}: {pieceLeft1}");
      Display.ShowMessage($"{player2.Name}: {pieceLeft2}");

      string symbol = player1 == playerBlack ? "O" : "X";
      Display.ShowMessage($"\n{player1.Name}'s ({symbol}) turn.");
    };

    controller.StartGame();
  }
}