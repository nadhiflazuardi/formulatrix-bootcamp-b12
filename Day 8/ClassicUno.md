---
title: Classic Uno Class Diagram
---
classDiagram
direction TB
    class ICard {
        <<interface>> 
        +Color Color
        +Score Score
        +Effect Effect
    }

    class IDeck {
        <<interface>>
        -List<.Card> _drawableCards
        -List<.Card> _discardedCards
        +GenerateCards()
        +Shuffle()
        +Draw()
        +MoveCardToDiscarded(Card card)
        +RecycleDiscarded()
    }

    class IPlayer {
        <<interface>> 
        +int Id
        +string Name
        +GetName()
    }

    class Deck {
        -List<.Card> _drawableCards
        -List<.Card> _discardedCards
        +GenerateCards(): void
        +Shuffle(): void
        +Draw(): Card
        +MoveCardToDiscarded(Card card): void
        +RecycleDiscarded(): void
    }

    class GameController {
        -IDeck _deck: readonly
        -Dictionary<.IPlayer, List<.Card>> _cardInHands
        -Dictionary<.IPlayer, int score> _playerScores
	    +int RoundCount
	    -IPlayer _currentPlayer
	    -bool _isTurnEnded
	    -int _turnDirection
	    -Player _roundWinner
	    +Card LastPlayedCard: readonly
	    +Action OnRoundStart
	    +Action<.Card> OnCardPlay

	    +GameController(List<.Player> players, IDeck deck)
	    +StartRound(): void
        +DistributeCards(): void
        +EndTurn(): void
        +NextTurn(): bool
        +PassTurn(): void
        +SayUno(): bool
        +DrawCard(Player player): bool
        +AddCardToHand(Player player, Card card): bool
        +RemoveCardFromhand(Player player, Card card): bool
        +HasCardInHand(Player player, Card card): bool
        +CountCardInHand(Player player): int
        +GetPlayerScore(Player player): int
        +AddPlayerScore(Player player, int amount): void

	    +ReverseTurnOrder(): void
        +ForcedDraw(Player player, int amount): bool
	    +SkipTurn(Player player): bool
	    +SelectColor(): bool
        +HandleCardEffect(Card card): void
        +NotifyCardPlayed(Card card): void
	    +GetCardAmountInHand(Player player): int
        +GetPreviousPlayer(): Player
	    +ChallengeOnDrawFour(Player player): bool
	    +CountRoundScore(): int
        +CheckRoundOver(): bool
        +CheckGameOver(): bool
    }

    class Card {
	    +Color Color: readonly
        +Effect Effect: readonly
        +Score Score: readonly
        +Card(Color color, Effect effect, Score score)
    }

    class Player {
	    +int Id: readonly
	    +string Name: readonly

	    +Player(int id, string name)
        +GetName(): string
    }

    class Color {
        <<enum>>
        Red,
        Green,
        Blue,
        Yellow,
    }

    class Effect {
        <<enum>>
        NoEffect,
        DrawTwo,
        Reverse,
        Skip,
        Wild,
        WildDrawFour
    }

    class Score {
        <<enum>>
	    DrawTwo = 20,
	    Skip = 20,
	    Reverse = 20,
	    Wild = 50,
	    WildDrawFour = 50,
	    Number0 = 0,
	    Number1 = 1,
	    Number2 = 2,
	    ...
    }

    ICard <|-- Card
    ICard <-- Deck

    IDeck <|-- Deck
    IDeck <-- GameController

    IPlayer <|-- Player
    IPlayer <-- GameController

    Card --> Color
    Card --> Effect
    Card --> Score
