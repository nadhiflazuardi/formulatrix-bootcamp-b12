namespace Checkers;
using Checkers.Interfaces;
using Checkers.Enums;

public class GameController
{
  private IBoard _board;
  private IPlayer Player1;
  private IPlayer Player2;
  public Dictionary<IPlayer, List<IPiece>> _playerPieces;
  private Display _display;
  private bool _isGameRunning;
  private PieceColor _currentPlaying;
  public Action<IPlayer>? OnGameEnd;
  public Action<IPlayer, List<IPiece>>? OnTurnChanged;

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
  private Piece? _mustContinueWithPiece;
  public void StartGame()
  {
    while (_isGameRunning)
    {
      _display.DisplayBoard(_board.Size, _board.Pieces);

      // Display.ShowMessage("\nPieces Left:");
      // Display.ShowMessage($"{Player1.Name}: {GetTotalPiece(Player1)}");
      // Display.ShowMessage($"{Player2.Name}: {GetTotalPiece(Player2)}");

      IPlayer currentPlayer = _currentPlaying == PieceColor.Black ? Player1 : Player2;
      OnTurnChanged?.Invoke(currentPlayer, _playerPieces[currentPlayer]);
      // Display.ShowMessage($"\n{(_currentPlaying == PieceColor.Black ? $"{Player1.Name}'s (O)" : $"{Player2.Name}'s (X)")} turn");

      List<Piece> movablePieces = new();
      List<Piece> canCapturePieces = new();
      Dictionary<Piece, List<Position>> allValidMoves = new();
      Dictionary<Piece, List<Position>> allCapturingMoves = new();

      if (_mustContinueWithPiece != null)
      {
        Position pos = _mustContinueWithPiece.CurrentPosition;

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
            _mustContinueWithPiece = null;
          }
        }
        else
        {
          _mustContinueWithPiece = null;
        }
      }

      if (_mustContinueWithPiece == null)
      {
        foreach (IPiece piece in _playerPieces[currentPlayer])
        {
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

      if (!HasValidMove(currentPlayer))
      {
        IPlayer winner = _currentPlaying == PieceColor.Black? Player2 : Player1;
        Display.ShowMessage(StatusGame(_currentPlaying));
        OnGameEnd?.Invoke(winner);
        GameOver();
        Console.ReadLine();
        break;
      }

      if (canCapturePieces.Count > 0)
      {
        Display.ShowMessage($"You have {canCapturePieces.Count} piece(s) that can capture and must be played.");
      }

      Display.ShowMessage("\nChoose a piece to move:");
      if (canCapturePieces.Count > 0)
      {
        for (int i = 0; i < canCapturePieces.Count; i++)
        {
          Piece piece = canCapturePieces[i];
          Display.ShowMessage($"{i + 1}. Piece at row {piece.CurrentPosition.Row + 1}, column {piece.CurrentPosition.Col + 1} {(piece.IsKing ? "(King)" : "")}");
        }
      }
      else
      {
        for (int i = 0; i < movablePieces.Count; i++)
        {
          Piece piece = movablePieces[i];
          Display.ShowMessage($"{i + 1}. Piece at row {piece.CurrentPosition.Row + 1}, column {piece.CurrentPosition.Col + 1} {(piece.IsKing ? "(King)" : "")}");
        }
      }

      Console.Write("Enter the number of your piece choice: ");
      int pieceChoice;
      if (!int.TryParse(Console.ReadLine(), out pieceChoice) || pieceChoice < 1 || pieceChoice > movablePieces.Count || (canCapturePieces.Count > 0 && pieceChoice > canCapturePieces.Count))
      {
        Display.ShowMessage("Invalid selection. Press Enter to try again.");
        Console.ReadLine();
        continue;
      }

      pieceChoice--;

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

      Display.ShowMessage("Available moves:");
      for (int i = 0; i < validMoves.Count; i++)
      {
        Display.ShowMessage($"{i + 1}. Move to row {validMoves[i].Row + 1}, column {validMoves[i].Col + 1}");
      }

      Console.Write("Enter the number of your move choice: ");
      int moveChoice;
      if (!int.TryParse(Console.ReadLine(), out moveChoice) || moveChoice < 1 || moveChoice > validMoves.Count)
      {
        Display.ShowMessage("Invalid selection. Press Enter to try again.");
        Console.ReadLine();
        continue;
      }

      moveChoice--;

      Position oldPosition = selectedPiece.CurrentPosition;
      Position newPosition = validMoves[moveChoice];

      bool moveSuccessful = _board.MovePiece(oldPosition, newPosition, _playerPieces, Player1, Player2);

      if (moveSuccessful)
      {
        bool wasCapture = canCapturePieces.Count > 0;

        if (wasCapture)
        {
          List<Position> nextCaptures = GetCapturingMoves(newPosition.Row, newPosition.Col);

          if (nextCaptures.Count > 0)
          {
            _mustContinueWithPiece = _board.Pieces[newPosition.Row, newPosition.Col];
          }
          else
          {
            SwitchTurn();
            _mustContinueWithPiece = null;
          }
        }
        else
        {
          SwitchTurn();
          _mustContinueWithPiece = null;
        }
      }
      else
      {
        Display.ShowMessage("Move failed. Press Enter to try again.");
        Console.ReadLine();
      }
    }
  }

  public List<Position> GetValidMoves(int row, int col)
  {
    List<Position> validMoves = new();

    if (row < 0 || row >= 8 || col < 0 || col >= 8 || _board.Pieces[row, col] == null)
    {
      return validMoves;
    }

    Piece selectedPiece = _board.Pieces[row, col];
    PieceColor pieceColor = selectedPiece.PieceColor;
    bool isKing = selectedPiece.IsKing;

    int forwardDirection = pieceColor == PieceColor.White ? 1 : -1;

    CheckRegularMoves(row, col, forwardDirection, validMoves);

    if (isKing)
    {
      CheckRegularMoves(row, col, -forwardDirection, validMoves);
    }

    CheckCaptureMoves(row, col, pieceColor, isKing, validMoves);

    return validMoves;
  }

  public List<Position> GetCapturingMoves(int row, int col)
  {
    List<Position> validMoves = new();

    if (row < 0 || row >= 8 || col < 0 || col >= 8 || _board.Pieces[row, col] == null)
    {
      return validMoves;
    }

    Piece selectedPiece = _board.Pieces[row, col];
    PieceColor pieceColor = selectedPiece.PieceColor;
    bool isKing = selectedPiece.IsKing;

    CheckCaptureMoves(row, col, pieceColor, isKing, validMoves);

    return validMoves;
  }

  private void CheckRegularMoves(int row, int col, int direction, List<Position> validMoves)
  {
    int newRow = row + direction;
    int newColLeft = col - 1;

    if (IsValidPosition(newRow, newColLeft) && _board.Pieces[newRow, newColLeft] == null)
    {
      validMoves.Add(new Position(newRow, newColLeft));
    }

    int newColRight = col + 1;

    if (IsValidPosition(newRow, newColRight) && _board.Pieces[newRow, newColRight] == null)
    {
      validMoves.Add(new Position(newRow, newColRight));
    }
  }

  private void CheckCaptureMoves(int row, int col, PieceColor pieceColor, bool isKing, List<Position> validMoves)
  {
    int[] rowDirections = { 1, 1, -1, -1 };
    int[] colDirections = { -1, 1, -1, 1 };

    for (int i = 0; i < 4; i++)
    {
      if (!isKing && ((pieceColor == PieceColor.White && rowDirections[i] == -1) ||
                      (pieceColor == PieceColor.Black && rowDirections[i] == 1)))
      {
        continue;
      }

      int adjacentRow = row + rowDirections[i];
      int adjacentCol = col + colDirections[i];

      if (IsValidPosition(adjacentRow, adjacentCol) &&
          _board.Pieces[adjacentRow, adjacentCol] != null &&
          _board.Pieces[adjacentRow, adjacentCol].PieceColor != pieceColor)
      {
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
    foreach (IPiece piece in _playerPieces[player])
    {
      if (piece is Piece p && _board.Pieces[p.CurrentPosition.Row, p.CurrentPosition.Col] == null)
        continue;

      if (piece is Piece playerPiece)
      {
        Position pos = playerPiece.CurrentPosition;
        List<Position> moves = GetValidMoves(pos.Row, pos.Col);

        if (moves.Count > 0)
        {
          return true;
        }
      }
    }
    return false;
  }

  public void GameOver()
  {
    _isGameRunning = false;
  }

  public void SwitchTurn()
  {
    _currentPlaying = _currentPlaying == PieceColor.White ? PieceColor.Black : PieceColor.White;
  }

  public int GetTotalPiece(IPlayer player)
  {
    return _playerPieces[player].Count;
  }

  public string StatusGame(PieceColor pieceColor)
  {
    return $"There are no more {pieceColor} pieces. Game over!";
  }
}