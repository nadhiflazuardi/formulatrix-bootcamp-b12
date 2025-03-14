namespace Checkers;

public class GameController
{
  private IBoard _board;
  private IPlayer Player1;
  private IPlayer Player2;
  public Dictionary<IPlayer, List<IPiece>> _playerPieces;
  private Display _display;
  private bool _isGameRunning;
  private PieceColor _currentPlaying;

  public GameController(IPlayer player1, IPlayer player2, IBoard board, Display display)
  {
    _isGameRunning = true;
    _currentPlaying = PieceColor.Black;
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
  private Piece _mustContinueWithPiece;
  public void StartGame()
  {
    while (_isGameRunning)
    {
      _display.DisplayBoard(_board.Size, _board.Pieces);

      IPlayer currentPlayer = _currentPlaying == PieceColor.Black ? Player1 : Player2;
      Console.WriteLine($"\n{(_currentPlaying == PieceColor.Black ? $"{Player1.Name}'s (O)" : $"{Player2.Name}'s (X)")} turn");

      // Get all player's pieces that have valid moves
      List<Piece> movablePieces = new();
      List<Piece> canCapturePieces = new();
      Dictionary<Piece, List<Position>> allValidMoves = new();
      Dictionary<Piece, List<Position>> allCapturingMoves = new();

      // Check if we must continue with a specific piece due to a prior capture
      if (_mustContinueWithPiece != null)
      {
        Position pos = _mustContinueWithPiece.CurrentPosition;
        // Ensure the piece is still on the board
        if (_board.Pieces[pos.Row, pos.Col] == _mustContinueWithPiece)
        {
          List<Position> capturingMoves = GetCapturingMoves(pos.Row, pos.Col);
          if (capturingMoves.Count > 0)
          {
            canCapturePieces.Add(_mustContinueWithPiece);
            allCapturingMoves.Add(_mustContinueWithPiece, capturingMoves);
            movablePieces.Add(_mustContinueWithPiece);
          }
          else
          {
            Console.WriteLine("Debug1");
            // No more captures allowed; reset
            _mustContinueWithPiece = null;
          }
        }
        else
        {
          Console.WriteLine("Debug2");
          // The piece was captured (unlikely, but handle it)
          _mustContinueWithPiece = null;
        }
      }

      // If not in a multi-capture scenario, proceed to collect all pieces
      if (_mustContinueWithPiece == null)
      {
        foreach (IPiece piece in _playerPieces[currentPlayer])
        {
          // Skip if piece has been captured (no longer on the board)
          if (piece is Piece p && _board.Pieces[p.CurrentPosition.Row, p.CurrentPosition.Col] == null)
            continue;

          if (piece is Piece playerPiece)
          {
            Position pos = playerPiece.CurrentPosition;
            List<Position> moves = GetValidMoves(pos.Row, pos.Col);
            List<Position> capturingMoves = GetCapturingMoves(pos.Row, pos.Col);

            if (capturingMoves.Count > 0)
            {
              canCapturePieces.Add(playerPiece);
              allCapturingMoves.Add(playerPiece, capturingMoves);
            }

            if (moves.Count > 0)
            {
              movablePieces.Add(playerPiece);
              allValidMoves.Add(playerPiece, moves);
            }
          }
        }
      }

      // Check if player has any valid moves
      if (movablePieces.Count == 0)
      {
        Console.WriteLine($"{currentPlayer.Name} has no valid moves. Game over!");
        _isGameRunning = false;
        Console.ReadLine();
        break;
      }

      if (canCapturePieces.Count > 0)
      {
        Console.WriteLine($"You have {canCapturePieces.Count} piece(s) that can capture and must be played.");
      }

      // Display movable pieces
      Console.WriteLine("Choose a piece to move:");
      if (canCapturePieces.Count > 0)
      {
        for (int i = 0; i < canCapturePieces.Count; i++)
        {
          Piece piece = canCapturePieces[i];
          Console.WriteLine($"{i + 1}. Piece at row {piece.CurrentPosition.Row + 1}, column {piece.CurrentPosition.Col + 1} {(piece.IsKing ? "(King)" : "")}");
        }
      }
      else
      {
        for (int i = 0; i < movablePieces.Count; i++)
        {
          Piece piece = movablePieces[i];
          Console.WriteLine($"{i + 1}. Piece at row {piece.CurrentPosition.Row + 1}, column {piece.CurrentPosition.Col + 1} {(piece.IsKing ? "(King)" : "")}");
        }
      }

      // Get player's piece choice
      Console.Write("Enter the number of your piece choice: ");
      int pieceChoice;
      if (!int.TryParse(Console.ReadLine(), out pieceChoice) || pieceChoice < 1 || pieceChoice > movablePieces.Count || (canCapturePieces.Count > 0 && pieceChoice > canCapturePieces.Count))
      {
        Console.WriteLine("Invalid selection. Press Enter to try again.");
        Console.ReadLine();
        continue;
      }

      // Convert to 0-based index
      pieceChoice--;

      // Get the selected piece and its valid moves
      List<Position> validMoves;
      Piece selectedPiece;
      if (canCapturePieces.Count > 0)
      {
        selectedPiece = canCapturePieces[pieceChoice];
        validMoves = allCapturingMoves[selectedPiece];
      }
      else
      {
        selectedPiece = movablePieces[pieceChoice];
        validMoves = allValidMoves[selectedPiece];
      }

      // Display available moves
      Console.WriteLine("Available moves:");
      for (int i = 0; i < validMoves.Count; i++)
      {
        Console.WriteLine($"{i + 1}. Move to row {validMoves[i].Row + 1}, column {validMoves[i].Col + 1}");
      }

      // Get player's move choice
      Console.Write("Enter the number of your move choice: ");
      int moveChoice;
      if (!int.TryParse(Console.ReadLine(), out moveChoice) || moveChoice < 1 || moveChoice > validMoves.Count)
      {
        Console.WriteLine("Invalid selection. Press Enter to try again.");
        Console.ReadLine();
        continue;
      }

      // Convert to 0-based index
      moveChoice--;

      // Create positions
      Position oldPosition = selectedPiece.CurrentPosition;
      Position newPosition = validMoves[moveChoice];

      // Move the piece
      bool moveSuccessful = _board.MovePiece(oldPosition, newPosition, _playerPieces, Player1, Player2);

      if (moveSuccessful)
      {
        bool wasCapture = canCapturePieces.Count > 0;

        if (wasCapture)
        {
          // Check if the selected piece can capture again from the new position
          List<Position> nextCaptures = GetCapturingMoves(newPosition.Row, newPosition.Col);

          if (nextCaptures.Count > 0)
          {
            // The same piece can capture again; do not switch player
            _mustContinueWithPiece = _board.Pieces[newPosition.Row, newPosition.Col];
          }
          else
          {
            // No more captures; switch player
            _currentPlaying = _currentPlaying == PieceColor.White ? PieceColor.Black : PieceColor.White;
            _mustContinueWithPiece = null;
          }
        }
        else
        {
          // Move was not a capture; switch player
          _currentPlaying = _currentPlaying == PieceColor.White ? PieceColor.Black : PieceColor.White;
          _mustContinueWithPiece = null;
        }
      }
      else
      {
        Console.WriteLine("Move failed. Press Enter to try again.");
        Console.ReadLine();
      }
    }
  }

  public List<Position> GetValidMoves(int row, int col)
  {
    List<Position> validMoves = new();

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

  public List<Position> GetCapturingMoves(int row, int col)
  {
    List<Position> validMoves = new();

    // Check if there is a piece at the specified position
    if (row < 0 || row >= 8 || col < 0 || col >= 8 || _board.Pieces[row, col] == null)
    {
      return validMoves; // Return empty list if no piece or invalid position
    }

    Piece selectedPiece = _board.Pieces[row, col];
    PieceColor pieceColor = selectedPiece.PieceColor;
    bool isKing = selectedPiece.IsKing;

    // Check capture moves
    CheckCaptureMoves(row, col, pieceColor, isKing, validMoves);

    return validMoves;
  }

  private void CheckRegularMoves(int row, int col, int direction, List<Position> validMoves)
  {
    // Check diagonal left
    int newRow = row + direction;
    int newColLeft = col - 1;

    if (IsValidPosition(newRow, newColLeft) && _board.Pieces[newRow, newColLeft] == null)
    {
      validMoves.Add(new Position(newRow, newColLeft));
    }

    // Check diagonal right
    int newColRight = col + 1;

    if (IsValidPosition(newRow, newColRight) && _board.Pieces[newRow, newColRight] == null)
    {
      validMoves.Add(new Position(newRow, newColRight));
    }
  }

  private void CheckCaptureMoves(int row, int col, PieceColor pieceColor, bool isKing, List<Position> validMoves)
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
          validMoves.Add(new Position(landingRow, landingCol));
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