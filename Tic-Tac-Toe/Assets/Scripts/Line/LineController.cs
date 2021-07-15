using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class LineController : MonoBehaviour
{
    private Image image;
    private Animator animator;

    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        GameController.OnGameEnd += PlayAnim;
    }

    void Start()
    {
        image.enabled = false;
    }

    private void PlayAnim(string animID)
    {
        image.enabled = true;
        animator.SetTrigger(animID);
    }

    private void OnDestroy()
    {
        GameController.OnGameEnd -= PlayAnim;
    }

}
