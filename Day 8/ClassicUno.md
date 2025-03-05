```mermaid
classDiagram
  classDiagram
  class GameController {
    -int RoundCount
    -List<.Player> _players
    -Player CurrentPlayer
    -string CurrentCardColor
    -string CurrentCardValue
    -bool IsTurnEnded
    -int TurnDirection
    -Player RoundWinner
    -Card LastPlayedCard
    +GenerateCards():void
    +EndTurn():void
    +NextTurn():void
    +GetNextPlayer():Player
    +DrawCard(Player player):void
    +GetCardAmountInHand(Player player):int
    +ForcedDraw(Player player, int amount):void
    +SkipTurn(Player player):void
    +ReverseTurnOrder():void
    +SelectColor():void
    +RegenerateDrawPile(DiscardPile discardPile, DrawPile drawPile):void
    +ChallengeOnDrawFour(Player player):bool
    +CountRoundScore():int
    +Action OnRoundStart
    +Action OnRoundOver
    +Action<.Card>? OnCardPlay
  }

  class DrawPile {
    -List<.Card> _cards
    +Generate():List<.Card>
    +Shuffle()
    +DrawCard() : card
    +RemoveCardFromDeck(Card card)
    +AddCardsToPile(List<.Card> cards)
    +EmptyPile():void
  }

  class DiscardPile {
    -List<.Card> _cards
    +AddCard(Card card)
    +ReturnCard(DrawPile drawPile)
    +EmptyPile():void
  }

  class Card {
    -string Color
    -string Value
    +OnReturn(GameController controller)
  }
  <<abstract>> Card

  class RegularCard {
  }

  class ReverseCard {
    +OnPlay(Player player, GameController controller)
  }

  class DrawTwoCard {
    +OnPlay(Player player, GameController controller)
  }

  class DrawFourCard {
    +OnPlay(Player player, GameController controller)
  }

  class SkipCard {
    +OnPlay(Player player, GameController controller)
  }

  class WildCard {
    +OnPlay(Player player, GameController controller)
  }

  class IPlayable {
    +OnPlay(Player player, GameController controller)
  }
  <<interface>>IPlayable

  class CardScore {
    DrawTwo = 20,
    Skip = 20,
    Reverse = 20,
    DrawFour = 50,
    Wild = 50,
    number0 = 0,
    number1 = 1,
    number2 = 2,
    ...
  }
  <<enumeration>> CardScore

  class Player {
    +int Id: readonly
    +string Name
    +int Score
    -List<.Card> _hand

    +Player(int id, string name)
    +AddCardToHand(Card card)
    +RemoveCardInHand(Card card)
    +SayUno(Player player)
    +HasCard(Card card)
    +PlayCard(Card card, GameController controller)
    +HandCount()
    +AddScore(int currentRoundScore)
    +EmptyHand():void
  }

  IPlayable <|.. ReverseCard
  IPlayable <|.. SkipCard
  IPlayable <|.. WildCard
  IPlayable <|.. DrawTwoCard
  IPlayable <|.. DrawFourCard

  Card --> DrawTwoCard
  Card --> RegularCard
  Card --> ReverseCard
  Card --> SkipCard
  Card --> WildCard

  CardScore --* Card

  DiscardPile --> GameController

  DrawPile -- Card
  DrawPile --> GameController

  GameController --> Card

  Player --> Card
  Player --> GameController

  DrawTwoCard --> GameController
  RegularCard --> GameController
  ReverseCard --> GameController
  SkipCard --> GameController
  WildCard --> GameController
  WildCard --|> DrawFourCard
```