using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class MiniMax : MonoBehaviour
{
    private PieceType maximizer;
    private PieceType minimizer;

    private GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    /* findBestMove -> 
     *  if true find best move for NPC, 
     *  if false find best move for player
     */
    internal Move FindBestMove(PieceType[,] board, bool findBestMove)
    {
        float bestValue = -Mathf.Infinity;
        Move bestMove = new Move();

        // define minimizer and maximizer
        DefineMaxAndMin(findBestMove);

        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == gameController.EmptyCell())
                {
                    board[row, col] = maximizer;

                    float value = GetMiniMaxValue(board, 0, false);

                    board[row, col] = gameController.EmptyCell();

                    if (value > bestValue)
                    {
                        bestValue = value;

                        bestMove.row = row;
                        bestMove.col = col;
                    }
                }
            }
        }
        return bestMove;
    }

    private void DefineMaxAndMin(bool isMaxTheNPC)
    {
        if (!isMaxTheNPC)
        {
            minimizer = gameController.Player;
            maximizer = gameController.NPC;
        }
        else
        {
            maximizer = gameController.Player;
            minimizer = gameController.NPC;
        }
    }

    private int Evaluate(PieceType[,] board)
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            if (gameController.CheckLineMatch(board, row))
            {
                if (board[row, 0] == maximizer)
                {
                    return +10;
                }
                else if (board[row, 0] == minimizer)
                {
                    return -10;
                }
            }
        }

        for (int col = 0; col < board.GetLength(1); col++)
        {
            if (gameController.CheckColMatch(board, col))
            {
                if (board[0, col] == maximizer)
                {
                    return +10;
                }
                else if (board[0, col] == minimizer)
                {
                    return -10;
                }
            }
        }

        if (gameController.CheckRightDiagnoalMatch(board))
        {
            if (board[0, 0] == maximizer)
            {
                return +10;
            }
            else if (board[0, 0] == minimizer)
            {
                return -10;
            }
        }

        if (gameController.CheckLeftDiagnoalMatch(board))
        {
            if (board[0, 2] == maximizer)
            {
                return +10;
            }
            else if (board[0, 2] == minimizer)
            {
                return -10;
            }
        }

        return 0;
    }

    private float GetMiniMaxValue(PieceType[,] board,
        int depth, bool isMax)
    {
        float bestValue;

        int value = Evaluate(board);
        if (value == 10)
        {
            return value - depth;
        }

        if (value == -10)
        {
            return value + depth;
        }
            
        if (!gameController.IsGameEnd(board))
        {
            return 0;
        }
            
        // If this maximizer's move
        if (isMax)
        {
            bestValue = -Mathf.Infinity;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == gameController.EmptyCell())
                    {
                        // Make the move
                        board[row, col] = maximizer;

                        // Get the best value with this move
                        bestValue = Mathf.Max(bestValue,
                            GetMiniMaxValue(board, depth + 1, !isMax));

                        // Undo the move
                        board[row, col] = gameController.EmptyCell();
                    }
                }
            }
            return bestValue;
        }
        else
        {// If this minimizer's move
            bestValue = Mathf.Infinity;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == gameController.EmptyCell())
                    {
                        // Make the move
                        board[row, col] = minimizer;

                        // Get the best value with this move
                        bestValue = Math.Min(bestValue,
                            GetMiniMaxValue(board, depth + 1, !isMax));

                        // Undo the move
                        board[row, col] = gameController.EmptyCell();
                    }
                }
            }
            return bestValue;
        }
    }
}
