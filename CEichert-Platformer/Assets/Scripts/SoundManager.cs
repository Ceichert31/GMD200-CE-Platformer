using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundID
{
    Hurt,
    Jump,
    GroundSlam,
    TimeSlow,
    Bounce,
}
[RequireComponent(typeof(AudioSource))]
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

    public delegate void SoundController(SoundID soundID);
    public static SoundController soundManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Plays a one shot of the sound value passed through
    /// </summary>
    /// <param name="soundID"></param>
    void PlaySound(SoundID soundID)
    {
        switch (soundID)
        {
            case SoundID.Hurt:
                audioSource.PlayOneShot(hurtClip, clipVolume);
                break;

            case SoundID.Jump:
                audioSource.PlayOneShot(jumpclip, clipVolume);
                break;

            case SoundID.GroundSlam:
                audioSource.PlayOneShot(groundSlamClip, clipVolume);
                break;

            case SoundID.TimeSlow:
                audioSource.PlayOneShot(timeClip, clipVolume);
                break;

            case SoundID.Bounce:
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
