namespace Checkers;
using Checkers.Enums;

public class Display {
  public string BoardHorizontalSymbol { get; set; }
  public string BoardVerticalSymbol { get; set; }

  public Display() {
    BoardHorizontalSymbol = "+---";
    BoardVerticalSymbol = "| ";
  }

  public void DisplayBoard(int Size, Piece[,] Pieces)
  {
    Console.Clear();
    Console.WriteLine("\n\n");
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

        char pieceSymbol = ' ';
        bool isKing = false;

        if (Pieces[row, col] != null)
        {
          if (Pieces[row, col].PieceColor == PieceColor.White)
          {
            pieceSymbol = 'X';
          }
          else
          {
            pieceSymbol = 'O';
          }

          isKing = Pieces[row, col].IsKing; // Check IsKing only if not null
        }

        Console.Write(BoardVerticalSymbol);

        // Change color to red if it's a King
        if (isKing)
        {
          Console.ForegroundColor = ConsoleColor.Red;
        }

        Console.Write(pieceSymbol + " ");
        Console.ResetColor(); // Reset to default color
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