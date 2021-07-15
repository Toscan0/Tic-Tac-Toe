using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingleton<T> : MonoBehaviour where T: GenericSingleton<T>
{
    private static T _instance;
    internal static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Error! Singleton not found: " + typeof(T));
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Init singleton
        _instance = (T) this;

        Init();
    }

    internal virtual void Init() { }
}
