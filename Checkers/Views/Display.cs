namespace Checkers;
using Checkers.Enums;

public class Display
{
  public string BoardHorizontalSymbol { get; set; }
  public string BoardVerticalSymbol { get; set; }

  public Display()
  {
    BoardHorizontalSymbol = "+---";
    BoardVerticalSymbol = "| ";
  }

  public void DisplayBoard(int Size, Piece[,] Pieces)
  {
    Console.Clear();
    ShowMessage("\n\n");
    string header = " ";
    for (int i = 0; i < Size; i++)
    {
      header += $"   {i + 1}";
    }
    ShowMessage(header);

    for (int row = 0; row < Size; row++)
    {
      ShowInlineMessage("  ");
      for (int col = 0; col < Size; col++)
      {
        ShowInlineMessage(BoardHorizontalSymbol);
      }

      ShowInlineMessage("+\n");

      for (int col = 0; col < Size; col++)
      {
        if (col == 0)
        {
          ShowInlineMessage(row + 1 + " ");
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

        ShowInlineMessage(BoardVerticalSymbol);

        // Change color to red if it's a King
        if (isKing)
        {
          Console.ForegroundColor = ConsoleColor.Red;
        }

        ShowInlineMessage(pieceSymbol + " ");
        Console.ResetColor(); // Reset to default color
      }

      ShowInlineMessage("|\n");
    }

    ShowInlineMessage("  ");
    for (int col = 0; col < Size; col++)
    {
      ShowInlineMessage(BoardHorizontalSymbol);
    }

    ShowInlineMessage("+");
  }

  public static void ShowMessage(string text)
  {
    Console.WriteLine(text);
  }

  public static void ShowInlineMessage(string text)
  {
    Console.Write(text);
  }
}