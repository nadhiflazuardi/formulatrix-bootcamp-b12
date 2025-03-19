namespace Checkers;

public class Board : IBoard
{
  public int Size { get; set; }
  public Piece[,] Pieces { get; set; }

  public Board()
  {
    Pieces = new Piece[8, 8];
    Size = 8;
    GenerateWhitePieces();
    GenerateBlackPieces();
  }

  public List<IPiece> GenerateWhitePieces()
  {
    List<IPiece> whitePieces = new();

    for (int row = 0; row <= 2; row++)
    {
      if (row % 2 == 0)
      {
        for (int col = 1; col <= 7; col += 2)
        {
          Piece piece = new(PieceColor.White, new Position(row, col));
          Pieces[row, col] = piece;
          whitePieces.Add(piece);
        }
      }
      else
      {
        for (int col = 0; col <= 7; col += 2)
        {
          Piece piece = new(PieceColor.White, new Position(row, col));
          Pieces[row, col] = piece;
          whitePieces.Add(piece);
        }
      }
    }

    return whitePieces;
  }

  public List<IPiece> GenerateBlackPieces()
  {
    List<IPiece> blackPieces = new();

    for (int row = 5; row <= 7; row++)
    {
      if (row % 2 == 0)
      {
        for (int col = 1; col <= 7; col += 2)
        {
          Piece piece = new(PieceColor.Black, new Position(row, col));
          Pieces[row, col] = piece;
          blackPieces.Add(piece);
        }
      }
      else
      {
        for (int col = 0; col <= 7; col += 2)
        {
          Piece piece = new(PieceColor.Black, new Position(row, col));
          Pieces[row, col] = piece;
          blackPieces.Add(piece);
        }
      }
    }

    return blackPieces;
  }

  public bool MovePiece(Position oldPosition, Position newPosition, Dictionary<IPlayer, List<IPiece>> playerPieces, IPlayer player1, IPlayer player2)
  {
    int oldRow = oldPosition.Row;
    int oldCol = oldPosition.Col;
    int newRow = newPosition.Row;
    int newCol = newPosition.Col;

    if (Pieces[oldRow, oldCol] == null)
    {
      return false;
    }

    Piece pieceToMove = Pieces[oldRow, oldCol];

    if (Pieces[newRow, newCol] != null)
    {
      return false;
    }

    bool isCapture = Math.Abs(newRow - oldRow) == 2 && Math.Abs(newCol - oldCol) == 2;

    if (isCapture)
    {
      int capturedRow = (oldRow + newRow) / 2;
      int capturedCol = (oldCol + newCol) / 2;

      if (Pieces[capturedRow, capturedCol] == null ||
          Pieces[capturedRow, capturedCol].PieceColor == pieceToMove.PieceColor)
      {
        return false;
      }

      Piece capturedPiece = Pieces[capturedRow, capturedCol];

      IPlayer opponentPlayer = capturedPiece.PieceColor == PieceColor.Black ? player1 : player2;
      playerPieces[opponentPlayer].Remove(capturedPiece);

      Pieces[capturedRow, capturedCol] = null;
    }

    Pieces[newRow, newCol] = pieceToMove;
    Pieces[oldRow, oldCol] = null;

    if (pieceToMove.CurrentPosition.Row != newRow || pieceToMove.CurrentPosition.Col != newCol)
    {
      IPlayer pieceOwner = pieceToMove.PieceColor == PieceColor.Black ? player1 : player2;

      playerPieces[pieceOwner].Remove(pieceToMove);

      Piece updatedPiece = new Piece(pieceToMove.PieceColor, new Position(newRow, newCol));

      if (pieceToMove.IsKing)
      {
        updatedPiece.PromoteToKing();
      }

      if ((updatedPiece.PieceColor == PieceColor.White && newRow == 7) ||
          (updatedPiece.PieceColor == PieceColor.Black && newRow == 0))
      {
        updatedPiece.PromoteToKing();
        Console.WriteLine($"Piece in {updatedPiece.CurrentPosition.ToString()} is promoted to king!");
      }

      playerPieces[pieceOwner].Add(updatedPiece);

      Pieces[newRow, newCol] = updatedPiece;
    }

    if ((pieceToMove.PieceColor == PieceColor.White && newRow == 7) ||
        (pieceToMove.PieceColor == PieceColor.Black && newRow == 0))
    {
      pieceToMove.PromoteToKing();
      Console.WriteLine($"Piece in {pieceToMove.CurrentPosition.ToString()} is promoted to king!");
    }

    return true;
  }
}