using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        DifficultySelecter.OnDifficultySelected += DifficultyUpdated;

        button = GetComponent<Button>();
    }

    private void Start()
    {
        SetInteractable(false);
    }

    public void Button_Restart()
    {
        LoadSceneManager.Instance.LoadPreviousScene();
    }

    private void SetInteractable(bool b)
    {
        button.interactable = b;
    }

    private void DifficultyUpdated(int i)
    {
        if(i <= 0)
        {
            SetInteractable(false);
        }
        else
        {
            SetInteractable(true);
        }
    }

    private void OnDestroy()
    {
        DifficultySelecter.OnDifficultySelected -= DifficultyUpdated;
    }
}