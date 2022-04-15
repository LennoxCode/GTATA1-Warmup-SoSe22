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

    public static AudioController AInstance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(AInstance != null) Destroy(gameObject);
        AInstance = this;
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
