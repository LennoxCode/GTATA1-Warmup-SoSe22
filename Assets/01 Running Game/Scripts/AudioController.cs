using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls all the audio output of the game
/// every soundfile is saved here and can be called with an according function
/// I used the singleton pattern here by declaring a static instance variable
/// and thus making this accessible from anywhere without finding the GameObject
/// i dont use this pattern very often but for such a simple application it is perfect
/// </summary>
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private AudioClip collisionSound;

    [SerializeField] private AudioClip touchGroundSound;
    [SerializeField] private AudioClip clickSound;
    public static AudioController AInstance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(AInstance != null) Destroy(gameObject);
        AInstance = this;
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound, 0.3f);
    }

    public void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound, 0.4f);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayGroundHitSound()
    {
        audioSource.PlayOneShot(touchGroundSound);
    }
}
