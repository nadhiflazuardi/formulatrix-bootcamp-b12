namespace Checkers;

public class Piece : IPiece
{
  public bool IsKing {get;}
  public PieceColor PieceColor {get;}
  public Position CurrentPosition {get;}

  public Piece(PieceColor pieceColor, Position position)
  {
    PieceColor = pieceColor;
    CurrentPosition = position;
  }
}