namespace Checkers;

public class Board : IBoard
{
  public int Size { get; set; }
  public Piece[,] Pieces { get; set; }
  public string BoardHorizontalSymbol { get; set; }
  public string BoardVerticalSymbol { get; set; }

  public Board()
  {
    Pieces = new Piece[8, 8];
    Size = 8;
    BoardHorizontalSymbol = "+---";
    BoardVerticalSymbol = "| ";
    GenerateWhitePieces();
    GenerateBlackPieces();
  }

  public void DisplayBoard()
  {
    Console.Clear();
    string header = " ";
    for (int i = 0; i < Size; i++)
    {
      header += $"   {i+1}";
    }
    Console.WriteLine(header);

    for (int row = 0; row < Size; row++)
    {
      Console.Write("  ");
      for (int col = 0; col < Size; col++)
      {
        Console.Write(BoardHorizontalSymbol);
      }

      Console.Write("+\n");

      for (int col = 0; col < Size; col++)
      {
        if (col == 0)
        {
          Console.Write(row + 1 + " ");
        }

        char pieceSymbol;
        if (Pieces[row, col] == null)
        {
          pieceSymbol = ' ';
        }
        else
        {
          if (Pieces[row, col].PieceColor == PieceColor.White)
          {
            pieceSymbol = 'X';
          }
          else
          {
            pieceSymbol = 'O';
          }
        }
        Console.Write(BoardVerticalSymbol + pieceSymbol + ' ');
      }

      Console.Write("|\n");
    }

    Console.Write("  ");
    for (int col = 0; col < Size; col++)
    {
      Console.Write(BoardHorizontalSymbol);
    }

    Console.Write("+");
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