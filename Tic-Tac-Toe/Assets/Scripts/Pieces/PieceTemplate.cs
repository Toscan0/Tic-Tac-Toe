using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceTemplate", menuName = "ScriptableObjects/Piece")]
public class PieceTemplate : ScriptableObject
{
    [SerializeField]
    private PieceType type;

    [SerializeField]
    private Sprite sprite;

    public new PieceType GetType()
    {
        return type;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
