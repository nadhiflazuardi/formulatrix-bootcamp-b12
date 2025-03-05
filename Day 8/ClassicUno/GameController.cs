namespace ClassicUno;

class GameController
{
  public Action OnRoundOver;
  public Action OnRoundStart;

  Player player1 = new();
  Player player2 = new();
  DrawPile drawPile = new();
  List<Player> players;
  public bool IsTurnEnded = false;
  public Player CurrentPlayer;
  public int TurnDirection = 1;

  public GameController()
  {
    players = [player1, player2];
    CurrentPlayer = player1;

    OnRoundStart += GenerateCards;
    OnRoundStart += DistributeCards;
  }

  public void StartGame() {
    OnRoundStart?.Invoke();

    int round;
  }

  public void GenerateCards() {
    drawPile.Generate();
    drawPile.Shuffle();
  }

  public void DistributeCards() {

  }

  public Player GetNextPlayer()
  {
    int currentPlayerIndex = players.IndexOf(CurrentPlayer);
    int nextPlayerIndex;

    if (TurnDirection == 1)
    {
      if (currentPlayerIndex != players.Count - 1)
      {
        nextPlayerIndex = currentPlayerIndex + 1;
      }
      else
      {
        nextPlayerIndex = 0;
      }
    }
    else
    {
      if (currentPlayerIndex != 0)
      {
        nextPlayerIndex = currentPlayerIndex - 1;
      }
      else
      {
        nextPlayerIndex = players.Count - 1;
      }
    }

    return players[nextPlayerIndex];
  }

  public int CountRoundScore() {
    int totalScore = 0;
    foreach (Player player in players) {
      int playerScore = player.GetHandScore();
      totalScore += playerScore;
    }

    return totalScore;
  }
}