namespace Checkers;

public struct Position
{
  public int Row, Col;
  public Position(int row, int col)
  {
    Row = row;
    Col = col;
  }

  public override string ToString()
  {
    return $"r{Row+1}c{Col+1}";
  }
}