using System;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public static Action<(int, int, int)> OnDataFromPlayerPrefs;

    private void Start()
    {
        UpdatePlayerPrefs();
    }

    private void UpdatePlayerPrefs()
    {
        int losses = 0;
        int draws = 0;
        int victories = 0;

        if (PlayerPrefs.HasKey("Lost"))
        {
            losses = PlayerPrefs.GetInt("Lost");
        }
        if (PlayerPrefs.HasKey("Draw"))
        {
            draws = PlayerPrefs.GetInt("Draw");
        }
        if (PlayerPrefs.HasKey("Win"))
        {
            victories = PlayerPrefs.GetInt("Win");
        }

        int winner = WinManager.Instance.PlayerWin;
        if (winner == -1)
        {
            losses++;
        }
        else if (winner == 0)
        {
            draws++;
        }
        else if (winner == 1)
        {
            victories++;
        }

        PlayerPrefs.SetInt("Lost", losses);
        PlayerPrefs.SetInt("Draw", draws);
        PlayerPrefs.SetInt("Win", victories);

        OnDataFromPlayerPrefs?.Invoke((losses, draws, victories));
    }

    public void Button_ResetScore()
    {
        PlayerPrefs.DeleteAll();
        OnDataFromPlayerPrefs?.Invoke((0, 0, 0));
    }
}
