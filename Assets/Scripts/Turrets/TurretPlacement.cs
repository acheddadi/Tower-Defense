using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    [SerializeField] private Color validPlacementColor;
    [SerializeField] private Color invalidPlacementColor;

    [SerializeField] private Vector3 colliderOffset;
    [SerializeField] private float colliderRadius;
    [SerializeField] private float colliderHeight;
    [SerializeField] private float floorMargin;


    private ParticleSystem[] particleSystems;
    private ParticleSystem.MainModule[] particleModules;

    private bool isPlacing = false;
    private bool lastValidPlacement = false;

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        if (particleSystems != null)
        {
            particleModules = new ParticleSystem.MainModule[particleSystems.Length];
            for (int i = 0; i < particleModules.Length; i++) particleModules[i] = particleSystems[i].main;
        }
    }

    private void LateUpdate()
    {
        if (particleSystems != null && isPlacing)
        {
            if (particleSystems[0].isStopped)
            {
                for (int i = 0; i < particleSystems.Length; i++)
                    particleSystems[i].Play();
            }
        }

        isPlacing = false;
    }

    public void PlaceTurret()
    {
        isPlacing = true;

        if (particleSystems != null)
        {
            bool validPlacement = IsValidPlacement();

            if (validPlacement != lastValidPlacement)
            {
                for (int i = 0; i < particleModules.Length; i++)
                {
                    if (validPlacement) particleModules[i].startColor = validPlacementColor;
                    else particleModules[i].startColor = invalidPlacementColor;
                }
            }

            lastValidPlacement = validPlacement;
        }
    }

    private bool IsValidPlacement()
    {
        Vector3 start = transform.position + colliderOffset - Vector3.up * (colliderHeight / 2.0f) + Vector3.up * floorMargin;
        Vector3 end = transform.position + colliderOffset + Vector3.up * (colliderHeight / 2.0f);

        return Physics.CheckCapsule(start, end, colliderRadius);
    }
}
