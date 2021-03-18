using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TurretPlacement : MonoBehaviour
{
    [SerializeField] private Color validPlacementColor;
    [SerializeField] private Color invalidPlacementColor;
    [SerializeField] private GameObject turretPrefab;

    private ParticleSystem[] particleSystems;
    private ParticleSystem.MainModule[] particleModules;

    private LinkedList<Collider> inboundColliders = new LinkedList<Collider>();
    private Color currentColour;
    private bool isPlacing = false;
    private bool isValidPlacement = false;

    // Start is called before the first frame update
    void Start()
    {
        currentColour = validPlacementColor;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        if (particleSystems != null)
        {
            particleModules = new ParticleSystem.MainModule[particleSystems.Length];
            for (int i = 0; i < particleModules.Length; i++) particleModules[i] = particleSystems[i].main;
        }
        else Debug.LogError("No particle system detected.");
    }

    private void FixedUpdate()
    {
        if (isPlacing && particleModules != null)
        {
            isValidPlacement = inboundColliders.Count == 0 && Physics.Raycast(transform.position + Vector3.up, Vector3.down, Mathf.Infinity);
            currentColour = isValidPlacement ? validPlacementColor : invalidPlacementColor;

            if (!particleModules[0].startColor.color.Equals(currentColour))
            {
                for (int i = 0; i < particleModules.Length; i++)
                {
                    particleModules[i].startColor = currentColour;
                    particleSystems[i].Simulate(particleSystems[i].time, true, true);
                    particleSystems[i].Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger) inboundColliders.AddFirst(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            try
            {
                inboundColliders.Remove(other);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public void PlaceTurretOverlay()
    {
        if (isPlacing) return;

        isPlacing = true;
        if (particleSystems != null)
            for (int i = 0; i < particleSystems.Length; i++) particleSystems[i].Play();
    }

    public bool RemoveTurretOverlay()
    {
        isPlacing = false;
        if (particleSystems != null)
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop();
                particleSystems[i].Clear();
            }
        
        if (isValidPlacement)
        {
            GameObject turret = Instantiate(turretPrefab);
            turret.transform.position = transform.position;
            turret.transform.rotation = transform.rotation;
            return true;
        }

        return false;
    }
}
