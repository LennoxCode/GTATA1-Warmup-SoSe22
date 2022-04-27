using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
/// <summary>
/// value holder for PowerUPs I created an Enum to design what effects each powerup has
/// I also opted to use an enum to represent what effect each powerup has.
/// there is also a static value to represent how long each powerups lasts
/// lastly I added the movement Object so powerups move across the screen
/// the actual logic of each powerup is implemented in the class it changes
/// e.g. shields on the ship and triple shot on the gun
/// </summary>
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