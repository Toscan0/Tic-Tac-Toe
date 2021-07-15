using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [Range(0, 2)]
    [SerializeField]
    private int row;
    [Range(0, 2)]
    [SerializeField]
    private int column;

    internal int GetRow()
    {
        return row;
    }

    internal int GetColumn()
    {
        return column;
    }
}
