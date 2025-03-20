namespace Checkers;
using Checkers.Interfaces;
using Checkers.Enums;

public class Board : IBoard
{
  public int Size { get; set; }
  public Piece[,] Pieces { get; set; }

  public Board(int size)
  {
    Size = size;
    Pieces = new Piece[size, size];
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
        for (int col = 1; col <= Size - 1; col += 2)
        {
          Piece piece = new(PieceColor.White, new Position(row, col));
          Pieces[row, col] = piece;
          whitePieces.Add(piece);
        }
      }
      else
      {
        for (int col = 0; col <= Size - 1; col += 2)
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

    for (int row = Size - 3; row <= Size - 1; row++)
    {
      int startCol = GetStartingColumnForRow(row);
      for (int col = startCol; col < Size; col += 2)
      {
        Piece piece = new(PieceColor.Black, new Position(row, col));
        Pieces[row, col] = piece;
        blackPieces.Add(piece);
      }
    }

    return blackPieces;
  }

  private int GetStartingColumnForRow(int row)
  {
    if (Size % 2 == 0)
    {
      return row % 2 == 0 ? 1 : 0;
    }
    else
    {
      return row % 2 == 0 ? 0 : 1;
    }
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

    Pieces[newRow, newCol] = pieceToMove;
    pieceToMove.CurrentPosition = new Position(newRow, newCol);
    Pieces[oldRow, oldCol] = null;

    if ((pieceToMove.PieceColor == PieceColor.White && newRow == Size - 1) ||
        (pieceToMove.PieceColor == PieceColor.Black && newRow == 0))
    {
      pieceToMove.PromoteToKing();
      Display.ShowMessage($"Piece in {pieceToMove.CurrentPosition.ToString()} is promoted to king!");
    }

    return true;
  }
}