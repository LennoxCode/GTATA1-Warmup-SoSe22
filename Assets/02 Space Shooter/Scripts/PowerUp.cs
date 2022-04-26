using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Effect effect;
    public SpriteRenderer spriteRenderer;
    public MovementObject movementObject;
    public static float duration = 3f;
}
public enum Effect
{
    TripleShot,
    Shield,
    SlowMotion
}