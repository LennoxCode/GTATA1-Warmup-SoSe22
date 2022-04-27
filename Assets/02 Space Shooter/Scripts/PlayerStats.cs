using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public  int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private SpriteRenderer healthHUD;
    [SerializeField] private Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        health = maxHealth;
        healthHUD.sprite = healthSprites[health];

    }

    private void Update()
    {
        if (health < 0) return;
        healthHUD.sprite = healthSprites[health];
    }

    public  void ResetHealth()
    {
        health = maxHealth;
        healthHUD.sprite = healthSprites[health];
    }
}
