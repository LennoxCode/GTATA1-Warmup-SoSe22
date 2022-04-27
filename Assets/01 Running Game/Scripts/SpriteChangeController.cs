using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
/// <summary>
/// Controls the sprite change of each character
/// I opted to use the unity animation module and creating a graph.
/// i used trigger to instantly jump to the next character sprite
/// and everything is connected in a circle so if next is pressed on the
/// last sprite the first one gets selected again. every sprite also
/// has a jump animation associated with it
/// </summary>
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
