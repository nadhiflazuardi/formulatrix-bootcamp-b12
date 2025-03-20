namespace Checkers.Interfaces;
using Checkers.Enums;

public interface IPiece
{
  bool IsKing { get; }
  PieceColor PieceColor { get; }
  Position CurrentPosition { get; }

  public void PromoteToKing();
}