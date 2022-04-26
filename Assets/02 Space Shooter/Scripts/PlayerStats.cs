using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private SpriteRenderer healthHUD;
    [SerializeField] private Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthHUD.sprite = healthSprites[health];

    }

    private void Update()
    {
        if (health < 0) return;
        healthHUD.sprite = healthSprites[health];
    }
}
