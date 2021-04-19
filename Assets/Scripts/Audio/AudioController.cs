// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script implements a singleton pattern to an audio
// controller, which in turn is used to play various sound
// clips through two different audio sources (BGM and SFX.)
// --------------------------------------------------------
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip electricalHurtSound;
    [SerializeField] private AudioClip crystalHurtSound;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip electricalExplosionSound;
    [SerializeField] private AudioClip crystalShatter;
    [SerializeField] private AudioClip uiSound;
    [SerializeField] private AudioClip incomingSound;
    [SerializeField] private AudioClip jingleWinSound;
    [SerializeField] private AudioClip jingleLoseSound;

    // Singleton pattern so that all objects can access this instance.
    private static AudioController instance;

    // Helper methods to play sound clips.
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static void Attack()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.attackSound);
    }

    public static void Hurt()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.hurtSound);
    }

    public static void ElectricalHurt()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.electricalHurtSound);
    }

    public static void CrystalHurt()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.crystalHurtSound);
    }

    public static void PickUp()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.pickUpSound);
    }

    public static void Shoot()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.shootSound);
    }

    public static void Explode()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.explosionSound);
    }

    public static void ElectricalExplode()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.electricalExplosionSound);
    }

    public static void CrystalShatter()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.crystalShatter);
    }

    public static void Confirm()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.uiSound);
    }

    public static void Incoming()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.incomingSound);
    }

    public static void Win()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.bgmSource.Stop();
        instance.sfxSource.PlayOneShot(instance.jingleWinSound);
    }

    public static void Lose()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.bgmSource.Stop();
        instance.sfxSource.PlayOneShot(instance.jingleLoseSound);
    }
}
