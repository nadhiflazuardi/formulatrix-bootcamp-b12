namespace Checkers.Interfaces;

public interface IDisplay
{
  public void DisplayBoard(int Size, Piece[,] Pieces);
  public string ChooseColor();
}