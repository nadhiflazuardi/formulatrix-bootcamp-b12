namespace Checkers;
using Checkers.Interfaces;
using Checkers.Enums;

public class Piece : IPiece
{
  public bool IsKing {get; private set;}
  public PieceColor PieceColor {get;}
  public Position CurrentPosition {get; set;}

  public Piece(PieceColor pieceColor, Position position)
  {
    PieceColor = pieceColor;
    CurrentPosition = position;
    IsKing = false;
  }

  public void PromoteToKing() {
    IsKing = true;
  }

  public void Move(Position position) {
    CurrentPosition = position;
  }
}