using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Effect effect;
    public SpriteRenderer spriteRenderer;
    
}
public enum Effect
{
    TripleShot,
    Shield,
    SlowMotion
}