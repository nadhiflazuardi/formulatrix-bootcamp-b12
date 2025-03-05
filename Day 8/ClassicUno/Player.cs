namespace ClassicUno;

class Player {
  private int Id;
  private string Name;
  private List<Card> _hand;

  public bool HasCard(Card card) {
    return _hand.Contains(card);
  }
  public int HandCount() {
    return _hand.Count;
  }

  public void EmptyHand() {

  }

  public int GetHandScore() {
    CardScore[] cards = [
      CardScore.number3
    ];

    int handScore = 0;
    foreach (CardScore card in cards) {
      handScore += (int) card;
    }

    return handScore;
  }

  public bool PlayCard(Card card, GameController controller) {
    
  }
}