namespace Checkers;

public interface IBoard
{
  int Size { get; set; }
  Piece[,] Pieces { get; set; }
  public bool MovePiece(Position oldPosition, Position newPosition, Dictionary<IPlayer, List<IPiece>> playerPieces, IPlayer player1, IPlayer player2);
  public List<IPiece> GenerateBlackPieces();
  public List<IPiece> GenerateWhitePieces();
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
  PieceColor PieceColor { get; }
  Position CurrentPosition { get; }
}
