using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
