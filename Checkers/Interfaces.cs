namespace Checkers;

public interface IBoard
{
  int Size { get; set; }
  Piece[,] Grid { get; set; }
  public bool MovePiece(Position oldPosition, Position newPosition);
}

public interface IDisplay
{

}

public interface IPlayer
{
  string Name { get; set; }
}

public interface IPiece
{
  bool IsKing { get; }
  PieceCode PieceCode { get; }
  PieceColor PieceColor { get; }
  Position CurrentPosition { get; }
}
