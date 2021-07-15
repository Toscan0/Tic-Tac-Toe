using System;
using System.Collections.Generic;
using UnityEngine;

using static DifficultyManager;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MiniMax))]
[RequireComponent(typeof(GameController))]
public class NPCController : MonoBehaviour
{
    internal Difficulties Difficulty { private get; set;  }

    [Tooltip("Value in %")]
    [SerializeField]
    private float mediumHardnessProbability = 70;

    private MiniMax miniMax;
    private GameController gameController;

    private void Awake()
    {
        miniMax = GetComponent<MiniMax>();
        gameController = GetComponent<GameController>();
    }

    internal Move Play(PieceType[,] board)
    {
        Move move = new Move();

        if(Difficulty == Difficulties.Easy)
        {
            move = EasyMove(board);
        }
        else if(Difficulty == Difficulties.Medium)
        {
            move = MediumMove(board);
        }
        else if (Difficulty == Difficulties.Hard)
        {
            move = HardMove(board);
        }
        else
        {
            Debug.LogError("Difficulty not recognized: " + Difficulty);
        }

        return move;
    }

    /*
     * NPC easy mode:
     *  The NPC selectes a random position to play
     */
    private Move EasyMove(PieceType[,] board)
    {
        List<(int, int)> emptyPlaces = gameController.FindEmptyPlaces(board);

        Move move = new Move();
        move = GetRandomMove(emptyPlaces);

        return move;
    }

    private Move MediumMove(PieceType[,] board)
    {
        Move move = new Move();

        if (Random.Range(0, 100 + 1) <= mediumHardnessProbability)
        { // Minimax
            move = miniMax.FindBestMove(board, true);
        }
        else
        { // Random

            List<(int, int)> emptyPlaces = gameController.FindEmptyPlaces(board);
            move = GetRandomMove(emptyPlaces);
        }

        return move;
    }

    /*
     * NPC hard mode:
     *  minimax with optimization for first move
     */
    private Move HardMove(PieceType[,] board)
    {
        Move move = new Move();

        if(gameController.IsFirstMove(board))
        { // Best first move is the corners

            move.row = 0;
            move.col = 0;
        }
        else if(gameController.MovesPlayed(board) == 1 
            && board[1, 1] == gameController.EmptyCell())
        { // Best first move for second player is the middle one
            move.row = 1;
            move.col = 1;
        }
        else
        {
            move = miniMax.FindBestMove(board, true);
        }

        return move;
    }

    // aux funcs

    // Choose a random positon from the emptys
    private Move GetRandomMove(List<(int, int)> emptyPlaces)
    {
        int count = emptyPlaces.Count;
        int index = Random.Range(0, count);

        Move move = new Move(emptyPlaces[index].Item1, emptyPlaces[index].Item2);

        return move;
    }
}
