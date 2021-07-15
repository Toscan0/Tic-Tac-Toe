using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(NPCController))]
public class GameController : MonoBehaviour
{
    internal static Action<bool> OnPlayerTurn;
    internal static Action<PieceTemplate> OnPieceSelected;
    internal static Action<string> OnGameEnd;

    internal bool IsRunning { get; private set; } = true;
    internal PieceType NPC { get; set; }
    internal PieceType Player { get; set; }

    [SerializeField]
    private AudioClip win;
    [SerializeField]
    private AudioClip draw;
    [SerializeField]
    private AudioClip lost;
    [SerializeField]
    private PieceTemplate cross;
    [SerializeField]
    private PieceTemplate circle;
    [SerializeField]
    private  Slot[] slots;

    //saves the template of the piece (img and type)
    private PieceTemplate npcPiece;
    private PieceTemplate playerPiece; 
    private bool isPlayerTurn;
    private PieceType[,] board = new PieceType[3, 3];

    private PlayerController playerController;
    private NPCController npcController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        npcController = GetComponent<NPCController>();

        RandomPlayerSelecter();
    }

    private void Start()
    {
        npcController.Difficulty = DifficultyManager.Instance.SelectedDifficulty;

        CreateMap();
        if (!IsPlayerTurn)
        {
            NPCTurn();
        }
    }

    private void CreateMap()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                board[row, col] = PieceType.None;
            }
        }
    }

    private void RandomPlayerSelecter()
    {
        if (Random.Range(0, 100 + 1) <= 50)
        {
            IsPlayerTurn = true;

            Player = PieceType.X;
            playerPiece = cross;

            NPC = PieceType.O;
            npcPiece = circle;
        }
        else
        {
            IsPlayerTurn = false;

            Player = PieceType.O;
            playerPiece = circle;

            NPC = PieceType.X;
            npcPiece = cross;
        }

        OnPieceSelected?.Invoke(playerPiece);
    }

    private void NPCTurn()
    {
        Move move = npcController.Play(board);
        board[move.row, move.col] = NPC;

        UpdateMapView(move, npcPiece.GetSprite());

        if (CheckMatch())
        {
            IsRunning = false;

            SoundManager.Instance.PlaySound(lost);
            WinManager.Instance.PlayerWin = -1;
        }
        else
        {
            if (!CheckDraw())
            {
                IsPlayerTurn = true;
            }
        }
    }

    internal void PlayerMove(Move move)
    {
        // check if move is valid
        if(board[move.row, move.col] != PieceType.None)
        {
            return;
        }

        board[move.row, move.col] = Player;

        UpdateMapView(move, playerPiece.GetSprite());

        if (CheckMatch())
        {
            IsRunning = false;

            SoundManager.Instance.PlaySound(win);
            WinManager.Instance.PlayerWin = 1;
        }
        else
        {
            if (!CheckDraw())
            {
                IsPlayerTurn = false;
                NPCTurn();
            }
        }
    }

    private void UpdateMapView(Move newMove, Sprite sprite)
    {
        //update view
        foreach(var slot in slots)
        {
            if (slot.GetRow() == newMove.row && slot.GetColumn() == newMove.col)
            {
                slot.GetComponentInChildren<Image>().sprite = sprite;
                break;
            }
        } 
    }

    private bool CheckDraw()
    {
        if (IsGameEnd(board))
        {
            IsRunning = false;

            SoundManager.Instance.PlaySound(draw);
            WinManager.Instance.PlayerWin = 0;

            return true;
        }
        return false;
    }

    private bool CheckMatch()
    {
        bool match = false;

        // Line check
        for (int row = 0; row < board.GetLength(0); row++)
        {
            if (CheckLineMatch(board, row))
            {
                match = true;
                PlayEndAnim("L" + (row + 1));

                break;
            }
        }

        // Col check
        for (int col = 0; col < board.GetLength(1); col++)
        {
            if (CheckColMatch(board, col))
            {
                match = true;
                PlayEndAnim("C" + (col + 1));

                break;
            }
        }

        // faster than a loop
        bool rightDiagnoal = CheckRightDiagnoalMatch(board);
        bool leftDiagnoal = CheckLeftDiagnoalMatch(board);
        if (rightDiagnoal || leftDiagnoal)
        {
            match = true;

            PlayEndAnim("D" + (rightDiagnoal ? "Right" : "Left"));
        }

        return match;
    }

    private void PlayEndAnim(string animID)
    {
        OnGameEnd?.Invoke(animID);
    }

    #region BOARD_LOGIC_PUBLIC_FUCNS

    internal bool CheckLineMatch(PieceType[,] board, int row)
    {
        return board[row, 0] != EmptyCell() &&
            board[row, 0] == board[row, 1] &&
            board[row, 1] == board[row, 2];
    }

    internal bool CheckColMatch(PieceType[,] board, int col)
    {
        return board[0, col] != EmptyCell() &&
            board[0, col] == board[1, col] &&
            board[1, col] == board[2, col];
    }

    internal bool CheckRightDiagnoalMatch(PieceType[,] board)
    {
        return board[0, 0] != EmptyCell() &&
            board[0, 0] == board[1, 1] &&
            board[1, 1] == board[2, 2];
    }

    internal bool CheckLeftDiagnoalMatch(PieceType[,] board)
    {
        return board[0, 2] != EmptyCell() &&
            board[0, 2] == board[1, 1] &&
            board[1, 1] == board[2, 0];
    }

    internal PieceType EmptyCell()
    {
        return PieceType.None;
    }

    internal bool IsGameEnd(PieceType[,] board)
    {
        foreach (var pos in board)
        {
            if (pos == EmptyCell())
            {
                return false;
            }
        }
        return true;
    }

    internal bool IsFirstMove(PieceType[,] board)
    {
        bool empty = true;

        foreach (var pos in board)
        {
            if (pos != EmptyCell())
            {
                empty = false;
            }
        }
        return empty;
    }

    internal int MovesPlayed(PieceType[,] board)
    {
        int count = 0;

        foreach (var pos in board)
        {
            if (pos != EmptyCell())
            {
                count++;
            }
        }
        return count;
    }

    internal List<(int, int)> FindEmptyPlaces(PieceType[,] board)
    {
        List<(int, int)> emptyPlaces = new List<(int, int)>();

        // Check empty places
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == PieceType.None)
                {
                    emptyPlaces.Add((i, j));
                }
            }
        }

        return emptyPlaces;
    }

    #endregion

    // Getters && Setters
    internal bool IsPlayerTurn
    {
        get
        {
            return isPlayerTurn;
        }

        private set
        {
            isPlayerTurn = value;

            OnPlayerTurn?.Invoke(isPlayerTurn);
        }
    }

    // For debug
    private void PrintMap()
    {
        Debug.Log("-- Start printing -- ");

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Debug.Log(board[i, j]);
            }
        }

        Debug.Log("-- End printing -- ");
    }
}
