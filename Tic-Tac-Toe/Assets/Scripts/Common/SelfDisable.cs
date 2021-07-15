using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    public void DisableMe()
    {
        gameObject.SetActive(false);
    }
}
