using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Effect effect;
    public SpriteRenderer spriteRenderer;
    public static float duration = 3f;
}
public enum Effect
{
    TripleShot,
    Shield,
    SlowMotion
}