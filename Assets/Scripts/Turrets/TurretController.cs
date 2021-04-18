// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to control turrets and have them
// move appropriately when locked on a target. The turret
// object will tilt and swivel to always look at its target
// and will fire a projectile based on set conditions.
// --------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurretController : MonoBehaviour
{
    [SerializeField] private float lookAtSpeed = 2.0f;
    [SerializeField] private Transform childToTilt;
    [SerializeField] private float firingDelay = 2.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1.0f;
    [SerializeField] private float projectileDamage = 10.0f;

    private Animator animator;

    private LinkedList<Collider> inboundEnemy = new LinkedList<Collider>();
    private RaycastHit raycastHit;
    private float firingTimer = 0.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there are enemies surrounding me.
        if (inboundEnemy.First != null)
        {
            // Remove target if destroyed.
            if (inboundEnemy.First.Value == null)
            {
                inboundEnemy.RemoveFirst();
                return;
            }

            // Rotate base to face the target.
            Vector3 direction = inboundEnemy.First.Value.transform.position + Vector3.up * 0.25f - transform.position;
            Quaternion swivelRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, Vector3.up).normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, swivelRotation, lookAtSpeed * Time.deltaTime);

            // If target is directly infront of us, start tilting the cannon to have a better shot.
            if (Vector3.Dot(transform.forward, Vector3.ProjectOnPlane(inboundEnemy.First.Value.transform.position - transform.position, Vector3.up).normalized) > 0.5f)
            {
                direction = inboundEnemy.First.Value.transform.position + Vector3.up * 0.25f - childToTilt.position;
                Quaternion tiltRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, childToTilt.right).normalized);
                childToTilt.rotation = Quaternion.Lerp(childToTilt.rotation, tiltRotation, lookAtSpeed * Time.deltaTime);
            }

            // If our next round is ready to fire.
            if (firingTimer > firingDelay)
            {
                // Check if there is a target in our firing range.
                if (Physics.Raycast(childToTilt.transform.position, childToTilt.transform.forward, out raycastHit, Mathf.Infinity, LayerMask.GetMask("Enemies")))
                {
                    // If the target is an enemy, fire.
                    if (raycastHit.collider.tag == "Enemy") Fire();
                }
            }

            firingTimer += Time.deltaTime;
        }

    }

    // Helper method to play firing animation.
    private void Fire()
    {
        animator.SetTrigger("Fire");
        firingTimer = 0.0f;
    }

    // Detect enemies that are in range.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !other.isTrigger)
            inboundEnemy.AddLast(other);
    }

    // Remove enemies that are too far.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            try
            {
                inboundEnemy.Remove(other);
            } catch (Exception e)
            {
                Debug.Log("Undocumented enemy just exited my trigger!\n" + e);
            }
        }

        firingTimer = 0.0f;
    }

    // Helper method to fire our projectile.
    public void FireProjectile()
    {
        if (projectilePrefab != null)
        {
            AudioController.Shoot();
            ProjectileController projectile = 
                Instantiate(projectilePrefab,
                childToTilt.position + childToTilt.forward * 0.5f,
                childToTilt.rotation).GetComponent<ProjectileController>();

            projectile.SetSpeed(projectileSpeed);
            projectile.SetDamage(projectileDamage);
        }
    }
}
