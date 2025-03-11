using Checkers;

class Program
{
  static void Main()
  {
    Board board = new();
    IPlayer Player1 = new Player();
    IPlayer Player2 = new Player();

    GameController controller = new(Player1, Player2, board);
  }
}