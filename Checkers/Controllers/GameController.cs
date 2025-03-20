namespace Checkers;
using Checkers.Interfaces;
using Checkers.Enums;

public class GameController
{
  private IBoard _board;
  private IPlayer _player1;
  private IPlayer _player2;
  private Dictionary<IPlayer, List<IPiece>> _playerPieces;
  private IDisplay _display;
  private bool _isGameRunning;
  private PieceColor _currentPlaying;
  public Action<IPlayer>? OnGameEnd;
  public Action<IPlayer, IPlayer, int, int>? OnTurnChanged;

  public GameController(IPlayer player1, IPlayer player2, IBoard board, IDisplay display)
  {
    _isGameRunning = true;
    _currentPlaying = PieceColor.Black;
    _board = board;
    _player1 = player1;
    _player2 = player2;
    _playerPieces = new Dictionary<IPlayer, List<IPiece>>
    {
    { _player1, _board.GenerateBlackPieces() },
    { _player2, _board.GenerateWhitePieces() }
    };
    _display = display;
  }
  private Piece? _mustContinueWithPiece;
  public void StartGame()
  {
    while (_isGameRunning)
    {
      _display.DisplayBoard(_board.Size, _board.Pieces);

      IPlayer currentPlayer = _currentPlaying == PieceColor.Black ? _player1 : _player2;
      OnTurnChanged?.Invoke(currentPlayer, currentPlayer == _player1 ? _player2 : _player1, GetTotalPiece(currentPlayer), GetTotalPiece(currentPlayer == _player1 ? _player2 : _player1));

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
        IPlayer winner = _currentPlaying == PieceColor.Black ? _player2 : _player1;
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

      Display.ShowInlineMessage("Enter the number of your piece choice: ");
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

      Display.ShowInlineMessage("Enter the number of your move choice: ");
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

      bool moveSuccessful = _board.MovePiece(oldPosition, newPosition, _playerPieces, _player1, _player2);

      if (moveSuccessful)
      {
        bool wasCapture = canCapturePieces.Count > 0;

        if (wasCapture)
        {
          RemovePiece(oldPosition, newPosition);

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

    if (row < 0 || row >= _board.Size || col < 0 || col >= _board.Size || _board.Pieces[row, col] == null)
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

    if (row < 0 || row >= _board.Size || col < 0 || col >= _board.Size || _board.Pieces[row, col] == null)
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

    if (IsValidMove(newRow, newColLeft) && _board.Pieces[newRow, newColLeft] == null)
    {
      validMoves.Add(new Position(newRow, newColLeft));
    }

    int newColRight = col + 1;

    if (IsValidMove(newRow, newColRight) && _board.Pieces[newRow, newColRight] == null)
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

      if (IsValidMove(adjacentRow, adjacentCol) &&
          _board.Pieces[adjacentRow, adjacentCol] != null &&
          _board.Pieces[adjacentRow, adjacentCol].PieceColor != pieceColor)
      {
        int landingRow = adjacentRow + rowDirections[i];
        int landingCol = adjacentCol + colDirections[i];

        if (IsValidCaptureMove(new Position(row, col), new Position(landingRow, landingCol)))
        {
          validMoves.Add(new Position(landingRow, landingCol));
        }
      }
    }
  }

  private bool IsValidMove(int row, int col)
  {
    return row >= 0 && row < _board.Size && col >= 0 && col < _board.Size;
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

  public bool IsValidCaptureMove(Position currentPosition, Position targetPosition)
  {
    // Check if the move is two squares away diagonally
    int rowDifference = targetPosition.Row - currentPosition.Row;
    int colDifference = targetPosition.Col - currentPosition.Col;
    if (Math.Abs(rowDifference) != 2 || Math.Abs(colDifference) != 2)
    {
      return false;
    }

    // Check if the intermediate square contains an opponent's piece
    int intermediateRow = currentPosition.Row + rowDifference / 2;
    int intermediateCol = currentPosition.Col + colDifference / 2;
    if (!IsValidMove(intermediateRow, intermediateCol) || _board.Pieces[intermediateRow, intermediateCol] == null)
    {
      return false;
    }
    Piece intermediatePiece = _board.Pieces[intermediateRow, intermediateCol];
    Piece currentPiece = _board.Pieces[currentPosition.Row, currentPosition.Col];
    if (intermediatePiece.PieceColor == currentPiece.PieceColor)
    {
      return false;
    }

    // Check if the target position is empty
    if (!IsValidMove(targetPosition.Row, targetPosition.Col) || _board.Pieces[targetPosition.Row, targetPosition.Col] != null)
    {
      return false;
    }

    return true;
  }

  public void RemovePiece(Position oldPosition, Position newPosition)
  {
    int oldRow = oldPosition.Row;
    int oldCol = oldPosition.Col;
    int newRow = newPosition.Row;
    int newCol = newPosition.Col;

    int capturedRow = (oldRow + newRow) / 2;
    int capturedCol = (oldCol + newCol) / 2;

    Piece capturedPiece = _board.Pieces[capturedRow, capturedCol];

    IPlayer opponentPlayer = capturedPiece.PieceColor == PieceColor.Black ? _player1 : _player2;
    _playerPieces[opponentPlayer].Remove(capturedPiece);

    _board.Pieces[capturedRow, capturedCol] = null;
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
    return $@"
  _______      ___      .___  ___.  _______      ______   ____    ____  _______ .______       __  
 /  _____|    /   \     |   \/   | |   ____|    /  __  \  \   \  /   / |   ____||   _  \     |  | 
|  |  __     /  ^  \    |  \  /  | |  |__      |  |  |  |  \   \/   /  |  |__   |  |_)  |    |  | 
|  | |_ |   /  /_\  \   |  |\/|  | |   __|     |  |  |  |   \      /   |   __|  |      /     |  | 
|  |__| |  /  _____  \  |  |  |  | |  |____    |  `--'  |    \    /    |  |____ |  |\  \----.|__| 
 \______| /__/     \__\ |__|  |__| |_______|    \______/      \__/     |_______|| _| `._____|(__)

There are no more {pieceColor} pieces.";
  }
}