// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// --------------------------------------------------------
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip uiSound;

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

    public static void Confirm()
    {
        if (instance == null)
        {
            Debug.LogError("Instance of AudioController does not exist!");
            return;
        }

        instance.sfxSource.PlayOneShot(instance.uiSound);
    }
}
