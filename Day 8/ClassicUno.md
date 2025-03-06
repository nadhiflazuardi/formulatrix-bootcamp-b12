---
title: Classic Uno Class Diagram
---
classDiagram
direction TB
    class GameController {
	    +int RoundCount
	    -List~.Player~ _players
	    -Player _currentPlayer
	    -string _currentCardColor
	    -string _currentCardValue
	    -bool _isTurnEnded
	    -int _turnDirection
	    -Player _roundWinner
	    +Card LastPlayedCard: readonly
	    +Action OnRoundStart
	    +Action~.Card~? OnCardPlay
	    +GameController(List<.Player> _players)
	    +StartRound() : void
        +DistributeCards(): void
	    +EndTurn(): void
        +PassTurn(): void
	    +GetNextPlayer() : Player
	    +ReverseTurnOrder() : void
        +ForcedDraw(Player player, int amount) : bool
	    +SkipTurn(Player player) : bool
	    +SelectColor() : bool
	    +DrawCard() : bool
        +HandleCardEffect(Card card): void
        +NotifyCardPlayer(Card card) : void
	    +GetCardAmountInHand(Player player) : int
	    +RegenerateDrawPile(DiscardPile discardPile, DrawPile drawPile) : bool
	    +ChallengeOnDrawFour(Player player) : bool
	    +CountRoundScore() : int
        +CheckRoundOver(): bool
        +CheckGameOver(): bool
    }
    class DrawPile {
	    -List~.Card~ _cards
	    +Generate() : List<.Card>
	    +Shuffle() : void
	    +DrawCard() : Card
	    +RemoveCardFromDeck(Card card) : bool
	    +AddCardsToPile(List<.Card> cards) : bool
	    +EmptyPile() : void
    }
    class DiscardPile {
	    -List~.Card~ _cards
	    +AddCard(Card card)
	    +ReturnCard(DrawPile drawPile)
	    +EmptyPile() : void
    }
    class Card {
	    +string Color: readonly
	    +string Value: readonly
        +Card(string color, string value)
    }
    class RegularCard {
	    +OnPlay(Player player): bool
    }
    class ReverseCard {
	    +OnPlay(Player player): bool
    }
    class DrawTwoCard {
	    +OnPlay(Player player): bool
    }
    class DrawFourCard {
	    +OnPlay(Player player): bool
    }
    class SkipCard {
	    +OnPlay(Player player): bool
    }
    class WildCard {
	    +OnPlay(Player player): bool
    }
    class IActionCard {
	    +OnPlay(Player player): bool
    }
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
    class Player {
	    +int Id: readonly
	    +string Name: readonly
	    -int _score
	    -List~.Card~ _hand
	    +Player(int id, string name)
	    +AddCardToHand(Card card): void
	    +RemoveCardInHand(Card card): bool
	    +SayUno(Player player): bool
	    +HasCard(Card card): bool
	    +PlayCard(Card card): bool
	    +HandCount(): int
        +GetScore(): int
	    +AddScore(int currentRoundScore): void
	    +EmptyHand() : void
    }

	<<abstract>> Card
	<<interface>> IActionCard
	<<enum>> CardScore

    IActionCard <|.. ReverseCard
    IActionCard <|.. SkipCard
    IActionCard <|.. WildCard
    IActionCard <|.. DrawTwoCard
    IActionCard <|.. DrawFourCard

    IDeck <|-- Deck

    Card <|-- DrawTwoCard
    Card <|-- RegularCard
    Card <|-- ReverseCard
    Card <|-- SkipCard
    Card <|-- WildCard
    Card *-- CardScore
    GameController <-- DrawPile
    GameController <-- DiscardPile
    Player "1" o-- "n" Card
    Player --> GameController
    WildCard <|-- DrawFourCard
