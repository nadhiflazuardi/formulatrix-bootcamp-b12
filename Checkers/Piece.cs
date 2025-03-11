namespace Checkers;

public class Piece : IPiece
{
  public bool IsKing {get;}
  public PieceCode PieceCode {get;}
  public PieceColor PieceColor {get;}
  public Position CurrentPosition {get;}

  public Piece(PieceCode pieceCode, PieceColor pieceColor, Position position)
  {
    PieceCode = pieceCode;
    PieceColor = pieceColor;
    CurrentPosition = position;
  }
}