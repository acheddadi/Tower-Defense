using System.Collections;
using System.Collections.Generic;
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

    private static AudioController instance;

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
