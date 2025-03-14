namespace Checkers;

public class GameController
{
  private IBoard _board;
  private IPlayer Player1;
  private IPlayer Player2;
  public Dictionary<IPlayer, List<IPiece>> _playerPieces;
  private Display _display;
  private bool _isGameRunning;

  public GameController(IPlayer player1, IPlayer player2, IBoard board, Display display)
  {
    _isGameRunning = true;
    _board = board;
    Player1 = player1;
    Player2 = player2;
    _playerPieces = new Dictionary<IPlayer, List<IPiece>>
    {
    { Player1, _board.GenerateBlackPieces() },
    { Player2, _board.GenerateWhitePieces() }
    };
    _display = display;
  }

  

  public void StartGame()
  {
    while (_isGameRunning)
    {
      _display.DisplayBoard(_board.Size, _board.Pieces);
    }
  }

  public List<string> GetValidMoves(int row, int col)
  {
    List<string> validMoves = new();

    // Check if there is a piece at the specified position
    if (row < 0 || row >= 8 || col < 0 || col >= 8 || _board.Pieces[row, col] == null)
    {
      return validMoves; // Return empty list if no piece or invalid position
    }

    Piece selectedPiece = _board.Pieces[row, col];
    PieceColor pieceColor = selectedPiece.PieceColor;
    bool isKing = selectedPiece.IsKing; // Assuming Piece class has IsKing property

    // Direction of movement depends on piece color
    // White pieces move down the board (increasing row)
    // Black pieces move up the board (decreasing row)
    int forwardDirection = pieceColor == PieceColor.White ? 1 : -1;

    // Check regular moves (diagonal forward)
    CheckRegularMoves(row, col, forwardDirection, validMoves);

    // If piece is a king, also check moves in the backward direction
    if (isKing)
    {
      CheckRegularMoves(row, col, -forwardDirection, validMoves);
    }

    // Check capture moves
    CheckCaptureMoves(row, col, pieceColor, isKing, validMoves);

    return validMoves;
  }

  private void CheckRegularMoves(int row, int col, int direction, List<string> validMoves)
  {
    // Check diagonal left
    int newRow = row + direction;
    int newColLeft = col - 1;

    if (IsValidPosition(newRow, newColLeft) && _board.Pieces[newRow, newColLeft] == null)
    {
      validMoves.Add($"({newRow}, {newColLeft})");
    }

    // Check diagonal right
    int newColRight = col + 1;

    if (IsValidPosition(newRow, newColRight) && _board.Pieces[newRow, newColRight] == null)
    {
      validMoves.Add($"({newRow}, {newColRight})");
    }
  }

  private void CheckCaptureMoves(int row, int col, PieceColor pieceColor, bool isKing, List<string> validMoves)
  {
    // Directions to check: forward-left, forward-right, backward-left, backward-right
    int[] rowDirections = { 1, 1, -1, -1 };
    int[] colDirections = { -1, 1, -1, 1 };

    for (int i = 0; i < 4; i++)
    {
      // Skip backward directions if not a king
      if (!isKing && ((pieceColor == PieceColor.White && rowDirections[i] == -1) ||
                      (pieceColor == PieceColor.Black && rowDirections[i] == 1)))
      {
        continue;
      }

      int adjacentRow = row + rowDirections[i];
      int adjacentCol = col + colDirections[i];

      // Check if adjacent position is valid and contains an opponent piece
      if (IsValidPosition(adjacentRow, adjacentCol) &&
          _board.Pieces[adjacentRow, adjacentCol] != null &&
          _board.Pieces[adjacentRow, adjacentCol].PieceColor != pieceColor)
      {
        // Check if landing position after capture is valid and empty
        int landingRow = adjacentRow + rowDirections[i];
        int landingCol = adjacentCol + colDirections[i];

        if (IsValidPosition(landingRow, landingCol) && _board.Pieces[landingRow, landingCol] == null)
        {
          validMoves.Add($"({landingRow}, {landingCol})");
        }
      }
    }
  }

  private bool IsValidPosition(int row, int col)
  {
    return row >= 0 && row < 8 && col >= 0 && col < 8;
  }

  public bool HasValidMove(IPlayer player)
  {
    return true;
  }

  public List<Position> GetValidMove(IBoard board, List<IPiece> pieces)
  {
    return [];
  }

  public bool IsValidMove(IPiece piece, Position newPosition, IBoard board)
  {
    return true;
  }

  public Position NewPosition(IPiece piece, Position newPosition)
  {
    return newPosition;
  }

  public bool IsValidCaptureMove(IPiece piece, Position newPosition, IBoard board)
  {
    return true;
  }

  public void RemovePiece(IPiece piece)
  {

  }

  public void SwitchTurn()
  {

  }

  public int GetTotalPiece(IPlayer player)
  {
    return 0;
  }
}