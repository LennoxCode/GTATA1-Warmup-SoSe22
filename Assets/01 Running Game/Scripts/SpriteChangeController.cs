using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class SpriteChangeController : MonoBehaviour
{
    private int currSpriteIndex = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite[] possibleSprites;
    private SpriteRenderer playerSpriteRenderer;
    private void Awake()
    {
        playerSpriteRenderer = GameObject.Find("Character").GetComponent<RunCharacterController>().CharacterSprite;
    }

    public void ChangeToNextSprite()
    {
        animator.SetTrigger("NextSprite");
    }

    public void ChangeToPreviousSprite()
    {
        animator.SetTrigger("PrevSprite");
    }
}
