using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurretController : MonoBehaviour
{
    [SerializeField] private float lookAtSpeed = 2.0f;
    [SerializeField] private Transform childToTilt;
    [SerializeField] private float firingRate = 2.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1.0f;
    [SerializeField] private float projectileDamage = 10.0f;

    private Animator animator;

    private LinkedList<Collider> inboundEnemy = new LinkedList<Collider>();
    private RaycastHit raycastHit;
    private float firingTimer = 0.0f;
    private bool firing = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inboundEnemy.First != null)
        {
            if (inboundEnemy.First.Value == null)
            {
                inboundEnemy.RemoveFirst();
                return;
            }

            Vector3 direction = inboundEnemy.First.Value.transform.position - transform.position;
            Quaternion swivelRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, Vector3.up).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, swivelRotation, lookAtSpeed * Time.deltaTime);

            direction = inboundEnemy.First.Value.transform.position - childToTilt.position;
            Quaternion tiltRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, childToTilt.right).normalized);
            childToTilt.rotation = Quaternion.Slerp(childToTilt.rotation, tiltRotation, lookAtSpeed * Time.deltaTime);

            if (firingTimer > firingRate)
            {
                Physics.Raycast(childToTilt.transform.position, childToTilt.transform.forward, out raycastHit, Mathf.Infinity);
                if (raycastHit.collider.tag == "Enemy") Fire();
            }

            firingTimer += Time.deltaTime;
        }

        else if (!firing)
        {
            if (transform.rotation != Quaternion.identity)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, (lookAtSpeed / 2.0f) * Time.deltaTime);

            if (childToTilt.rotation != Quaternion.identity)
                childToTilt.rotation = Quaternion.Slerp(childToTilt.rotation, Quaternion.identity, (lookAtSpeed / 2.0f) * Time.deltaTime);

        }

    }

    private void Fire()
    {
        animator.SetTrigger("Fire");
        firingTimer = 0.0f;
        firing = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !other.isTrigger)
            inboundEnemy.AddLast(other);
    }

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
    public void FireProjectile()
    {
        if (projectilePrefab != null)
        {
            ProjectileController projectile = 
                Instantiate(projectilePrefab,
                childToTilt.position + childToTilt.forward * 0.5f,
                childToTilt.rotation).GetComponent<ProjectileController>();

            projectile.SetSpeed(projectileSpeed);
            projectile.SetDamage(projectileDamage);
        }
        firing = false;
    }
}
