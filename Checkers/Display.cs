namespace Checkers;

public class Display {
  public void DisplayBoard(int Size, string BoardHorizontalSymbol, string BoardVerticalSymbol, Piece[,] Pieces)
  {
    Console.Clear();
    string header = " ";
    for (int i = 0; i < Size; i++)
    {
      header += $"   {i + 1}";
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
}