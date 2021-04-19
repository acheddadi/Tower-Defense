// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to control the particle system used for
// the turret's placement system. The script checks for valid
// placements based on wether the turret is grounded and if it
// is currently colliding with any other object.
// --------------------------------------------------------
using System;
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
        // Initialize our variables.
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
        // Make sure reference to particle system exists, and that we're placing our overlay.
        if (isPlacing && particleModules != null)
        {
            // If nothing is in the way of our turret, and we're grounded, then this is a valid placement.
            isValidPlacement = inboundColliders.Count == 0 && Physics.Raycast(transform.position + Vector3.up, Vector3.down, Mathf.Infinity);

            // If this is a valid placement, switch to our blue colour, if not then switch to red.
            currentColour = isValidPlacement ? validPlacementColor : invalidPlacementColor;

            // If we've got the wrong colour.
            if (!particleModules[0].startColor.color.Equals(currentColour))
            {
                // Change the colour for each particle system.
                for (int i = 0; i < particleModules.Length; i++)
                {
                    particleModules[i].startColor = currentColour;
                    particleSystems[i].Simulate(particleSystems[i].time, true, true);
                    particleSystems[i].Play();
                }
            }
        }
    }

    // If something is in our way, keep track of it.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger) inboundColliders.AddFirst(other);
    }

    // If whatever was in our way left, remove it from our list.
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

    // Helper method to play our particle systems.
    public void PlaceTurretOverlay()
    {
        if (isPlacing) return;
        AudioController.PlacingTurret();
        isPlacing = true;
        if (particleSystems != null)
            for (int i = 0; i < particleSystems.Length; i++) particleSystems[i].Play();
    }

    // Helper method to clear our particle systems and spawn a turret.
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
            AudioController.PlaceTurret();
            GameObject turret = Instantiate(turretPrefab);
            turret.transform.position = transform.position;
            turret.transform.rotation = transform.rotation;
            return true;
        }

        return false;
    }
}
