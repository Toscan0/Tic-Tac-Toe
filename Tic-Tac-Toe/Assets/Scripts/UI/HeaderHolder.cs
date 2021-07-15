using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderHolder : MonoBehaviour
{
    [SerializeField]
    private Text difText;
    [SerializeField]
    private Image pieceImg;
    [SerializeField]
    private Text turnText;

    private void Awake()
    {
        GameController.OnPieceSelected += UpdatePieceImg;
        GameController.OnPlayerTurn += EnableTurnText;        
    }

    private void Start()
    {
        SetDifficultyText();
    }

    private void SetDifficultyText()
    {
        string dif = "Dfficulty: ";

        difText.text = dif + DifficultyManager.Instance.SelectedDifficulty;
    }

    private void EnableTurnText(bool b)
    {
        turnText.enabled = b;
    }

    private void UpdatePieceImg(PieceTemplate player)
    {
        pieceImg.sprite = player.GetSprite();
    }

    private void OnDestroy()
    {
        GameController.OnPieceSelected -= UpdatePieceImg;
        GameController.OnPlayerTurn -= EnableTurnText;
    }
}
