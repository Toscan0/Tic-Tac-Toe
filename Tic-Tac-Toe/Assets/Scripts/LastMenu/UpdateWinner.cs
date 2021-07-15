using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWinner : MonoBehaviour
{
    [SerializeField]
    private Text WinnerText;
    [SerializeField]
    private Text scoreText;

    private void Awake()
    {
        ScoreboardManager.OnDataFromPlayerPrefs += UpdateScoreboardText;
    }

    private void Start()
    {
        UpdateWinnerText();
    }

    private void UpdateScoreboardText((int, int, int) info)
    {
        scoreText.text = "Losses: " + info.Item1 + "\n" +
            "Draws: " + info.Item2 + "\n" +
            "Victories: " + info.Item3 + "\n";
    }

    private void UpdateWinnerText()
    {
        int winner = WinManager.Instance.PlayerWin;
        if (winner  == - 1)
        {
            WinnerText.text = "You Lost!";
        }
        else if(winner == 0)
        {
            WinnerText.text = "Draw!";
        }
        else if (winner == 1)
        {
            WinnerText.text = "You Win!";
        }
        else
        {
            Debug.LogError("Winner not indentified! Winner: " + winner);
        }
    }

    private void OnDestroy()
    {
        ScoreboardManager.OnDataFromPlayerPrefs -= UpdateScoreboardText;
    }
}
