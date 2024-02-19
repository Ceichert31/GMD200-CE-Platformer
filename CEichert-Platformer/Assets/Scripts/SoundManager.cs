using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource audioSource;

    [Header("SFX Settings")]
    [SerializeField] private float clipVolume = 0.5f;

    [SerializeField] private AudioClip 
        hurtClip,
        jumpclip,
        groundSlamClip,
        timeClip,
        bounceClip;

    public delegate void SoundController (int soundID);
    public static SoundController soundManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Plays a one shot of the sound value passed through
    /// </summary>
    /// <param name="soundID"></param>
    void PlaySound(int soundID)
    {
        switch (soundID)
        {
            case 0:
                audioSource.PlayOneShot(hurtClip, clipVolume);
                break;

            case 1:
                audioSource.PlayOneShot(jumpclip, clipVolume);
                break;

            case 2:
                audioSource.PlayOneShot(groundSlamClip, clipVolume);
                break;

            case 3:
                audioSource.PlayOneShot(timeClip, clipVolume);
                break;

            case 4:
                audioSource.PlayOneShot(bounceClip, clipVolume);
                break;
        }
    }

    private void OnEnable()
    {
        soundManager += PlaySound;
    }
    private void OnDisable()
    {
        soundManager -= PlaySound;
    }
}
