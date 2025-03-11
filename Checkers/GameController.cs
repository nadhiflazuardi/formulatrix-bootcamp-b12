namespace Checkers;

public class GameController
{
  private IBoard _board;
  IPlayer Player1;
  IPlayer Player2;
  public Dictionary<IPlayer, List<IPiece>> _playerPieces;

  public GameController(IPlayer player1, IPlayer player2, IBoard board)
  {
    _board = board;
    Player1 = player1;
    Player2 = player2;
  }

  public void Preparation()
  {
  

    // create black pieces
    for (int row = 5; row <= 7; row++)
    {
      if (row % 2 == 0)
      {
        for (int col = 0; col <= 7; col += 2)
        {
          Piece piece = new Piece(PieceCode.Regular, PieceColor.Black, new Position(row, col));
          _board.Grid[row, col] = piece;
          _playerPieces[Player1].Add(piece);
        }
      }
      else
      {
        for (int col = 1; col <= 7; col += 2)
        {
          Piece piece = new Piece(PieceCode.Regular, PieceColor.Black, new Position(row, col));
          _board.Grid[row, col] = piece;
          _playerPieces[Player1].Add(piece);
        }
      }
    }
  }

  public bool HasValidMove(IPlayer player)
  {
    return true;
  }

  public List<Position> GetValidMove(IBoard board, List<IPiece> pieces)
  {
    return [];
  }

  public bool IsValidMove(IPiece piece, Position newPosition, IBoard board)
  {
    return true;
  }

  public Position NewPosition(IPiece piece, Position newPosition)
  {
    return newPosition;
  }

  public bool IsValidCaptureMove(IPiece piece, Position newPosition, IBoard board)
  {
    return true;
  }

  public void RemovePiece(IPiece piece)
  {

  }

  public void SwitchTurn()
  {

  }

  public int GetTotalPiece(IPlayer player)
  {
    return _playerPieces[player].Count;
  }
}