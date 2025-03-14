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

    // create white pieces
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

    // create black pieces
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

  // Update the Board class with this MovePiece method
  public bool MovePiece(Position oldPosition, Position newPosition, Dictionary<IPlayer, List<IPiece>> playerPieces, IPlayer player1, IPlayer player2)
  {
    int oldRow = oldPosition.Row;
    int oldCol = oldPosition.Col;
    int newRow = newPosition.Row;
    int newCol = newPosition.Col;

    // Check if old position has a piece
    if (Pieces[oldRow, oldCol] == null)
    {
      return false;
    }

    // Get the piece to move
    Piece pieceToMove = Pieces[oldRow, oldCol];

    // Check if new position is empty
    if (Pieces[newRow, newCol] != null)
    {
      return false;
    }

    // Check if this is a capture move
    bool isCapture = Math.Abs(newRow - oldRow) == 2 && Math.Abs(newCol - oldCol) == 2;

    if (isCapture)
    {
      // Calculate the position of the captured piece
      int capturedRow = (oldRow + newRow) / 2;
      int capturedCol = (oldCol + newCol) / 2;

      // Check if there is a piece to capture
      if (Pieces[capturedRow, capturedCol] == null ||
          Pieces[capturedRow, capturedCol].PieceColor == pieceToMove.PieceColor)
      {
        return false;
      }

      // Get the captured piece
      Piece capturedPiece = Pieces[capturedRow, capturedCol];

      // Remove the captured piece from the player's pieces collection
      IPlayer opponentPlayer = capturedPiece.PieceColor == PieceColor.Black ? player1 : player2;
      playerPieces[opponentPlayer].Remove(capturedPiece);

      // Remove the captured piece from the board
      Pieces[capturedRow, capturedCol] = null;
    }

    // Move the piece to the new position
    Pieces[newRow, newCol] = pieceToMove;
    Pieces[oldRow, oldCol] = null;

    // Create a new piece with updated position if needed
    if (pieceToMove.CurrentPosition.Row != newRow || pieceToMove.CurrentPosition.Col != newCol)
    {
      // Get the owner of the piece
      IPlayer pieceOwner = pieceToMove.PieceColor == PieceColor.Black ? player1 : player2;

      // Remove the old piece from the player's collection
      playerPieces[pieceOwner].Remove(pieceToMove);

      // Create a new piece with the updated position
      Piece updatedPiece = new Piece(pieceToMove.PieceColor, new Position(newRow, newCol));

      // If the original piece was a king, make the new one a king too
      // (You'll need to adjust this based on your Piece class implementation)
      if (pieceToMove.IsKing)
      {
        // Set the new piece to be a king (assuming you have a way to do this)
        updatedPiece.PromoteToKing();
      }

      // Add the new piece to the player's collection
      playerPieces[pieceOwner].Add(updatedPiece);

      // Update the board with the new piece
      Pieces[newRow, newCol] = updatedPiece;
    }

    // Check if the piece should be promoted to king
    // Assuming white pieces are promoted at row 7 and black pieces at row 0
    if ((pieceToMove.PieceColor == PieceColor.White && newRow == 7) ||
        (pieceToMove.PieceColor == PieceColor.Black && newRow == 0))
    {
      pieceToMove.PromoteToKing();
    }

    return true;
  }
}