using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementDelay = 1.0f;
    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackStrength = 10.0f;
    [SerializeField] private float giveUpChaseRange = 5.0f;
    [SerializeField] private SpriteAnimator spriteAnimator;

    private float movementTimer = 0.0f;
    private float attackTimer = 0.0f;

    private HealthController attackTarget;
    private NavMeshAgent navMeshAgent;
    private Vector3 spriteDirection;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (CrystalController.GetInstance() == null) Debug.LogError("No instance of crystal was found.");
    }

    private void Update()
    {
        if (attackTarget == null)
        {
            if (CrystalController.GetInstance() != null && movementTimer > movementDelay)
            {
                navMeshAgent.SetDestination(CrystalController.GetInstance().transform.position);
                movementTimer = 0.0f;
            }

            else return;
        }

        else
        {
            if (Vector3.Distance(attackTarget.transform.position, transform.position) < attackRange)
            {
                navMeshAgent.velocity = Vector3.zero;
                navMeshAgent.ResetPath();

                if (attackTimer > attackDelay)
                {
                    spriteDirection = Vector3.ProjectOnPlane(attackTarget.transform.position - transform.position, Camera.main.transform.forward).normalized;
                    if (spriteAnimator != null) spriteAnimator.Attack();
                    attackTarget.LoseHealth(attackStrength);
                    attackTimer = 0.0f;
                }

                attackTimer += Time.deltaTime;
            }

            else if (movementTimer > movementDelay)
            {
                navMeshAgent.SetDestination(attackTarget.transform.position);
                movementTimer = 0.0f;
            }
        }

        movementTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (spriteAnimator != null)
        {
            if (navMeshAgent.velocity.magnitude > 0.0f)
                spriteDirection = Vector3.ProjectOnPlane(navMeshAgent.velocity, Camera.main.transform.forward).normalized;

            spriteAnimator.SetDirection(spriteDirection);
            spriteAnimator.SetMoving(navMeshAgent.velocity.magnitude > 0.0f);
        }

        if (attackTarget != null && Vector3.Distance(transform.position, attackTarget.transform.position) > giveUpChaseRange)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.ResetPath();
            attackTarget = null;
            movementTimer = 0.0f;
            attackTimer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            HealthController target = other.GetComponent<HealthController>();

            if (target != null && other.tag != "Enemy")
                attackTarget = target;
        }
    }
}
