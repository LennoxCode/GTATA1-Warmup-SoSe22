using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class SpriteChangeController : MonoBehaviour
{
    private int currSpriteIndex = 0;
    [SerializeField] private Animator animator;
    public void ChangeToNextSprite()
    {
        animator.SetTrigger("NextSprite");
        AudioController.AInstance.PlayClickSound();
    }

    public void ChangeToPreviousSprite()
    {
        animator.SetTrigger("PrevSprite");
        AudioController.AInstance.PlayClickSound();
    }
    
}
