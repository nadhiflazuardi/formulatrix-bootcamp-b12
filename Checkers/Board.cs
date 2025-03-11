namespace Checkers;

public class Board : IBoard {
  public int Size {get; set;}
  public Piece[,] Grid {get; set;}

  public Board() {
    this.GenerateWhitePieces();
  }

  public List<Piece> GenerateWhitePieces() {
    List<Piece> whitePieces = new();

    // create white pieces
    for (int row = 0; row <= 2; row++)
    {
      if (row % 2 == 0)
      {
        for (int col = 1; col <= 7; col += 2)
        {
          Piece piece = new Piece(PieceCode.Regular, PieceColor.White, new Position(row, col));
          this.Grid[row, col] = piece;
        }
      }
      else
      {
        for (int col = 0; col <= 7; col += 2)
        {
          Piece piece = new Piece(PieceCode.Regular, PieceColor.White, new Position(row, col));
          this.Grid[row, col] = piece;
        }
      }
    }

    return whitePieces;
  }

  public bool MovePiece(Position oldPosition, Position newPosition) {
    return true;
  }
}