using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : GenericSingleton<DifficultyManager>
{
    internal enum Difficulties
    {
        None,
        Easy,
        Medium,
        Hard,
    }

    static private Difficulties selectedDifficulty = Difficulties.Easy;
    internal Difficulties SelectedDifficulty
    {
        get
        {
            return selectedDifficulty;
        }
        private set
        {
            selectedDifficulty = value;
        }
    }

    internal List<string> GetDifficulties()
    {
        return new List<string>(Enum.GetNames(typeof(Difficulties)));
    }

    internal void SetSelectedDifficulty(int t)
    {
        SelectedDifficulty = (Difficulties)t;
    }
}
