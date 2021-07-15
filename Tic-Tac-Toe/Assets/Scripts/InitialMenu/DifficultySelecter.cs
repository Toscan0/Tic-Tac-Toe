using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DifficultySelecter : MonoBehaviour
{
    internal static Action<int> OnDifficultySelected;

    [SerializeField]
    private Text label;
    [SerializeField]
    private Color mandatoryFieldColor;
    [SerializeField]
    private Color defaultColor;
    
    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    private void Start()
    {
        List<string> difficulties = DifficultyManager.Instance.GetDifficulties();
        PopulateList(difficulties);

        dropdown.value = (int) DifficultyManager.Instance.SelectedDifficulty;
        Dropdown_IndexChanged(dropdown.value);
    }

    private void PopulateList(List<string> difficulties)
    {
        List<string> names = new List<string>(difficulties);
        dropdown.AddOptions(names);
    }

    public void Dropdown_IndexChanged(int newIndex)
    {
        OnDifficultySelected?.Invoke(newIndex);
        DifficultyManager.Instance.SetSelectedDifficulty(newIndex);

        if (newIndex == 0)
        {
            label.color = mandatoryFieldColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
}
