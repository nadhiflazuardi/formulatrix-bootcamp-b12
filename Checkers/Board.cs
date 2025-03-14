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

  public bool MovePiece(Position oldPosition, Position newPosition)
  {
    return true;
  }
}