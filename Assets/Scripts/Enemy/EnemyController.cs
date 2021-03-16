using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float maxChaseDistance = 5.0f;
    [SerializeField] private float chaseDelay = 1.0f;
    [SerializeField] private SpriteAnimator spriteAnimator;

    private float chaseTimer = 0.0f;

    private Vector3 mainTarget;
    private HealthController attackTarget;
    private NavMeshAgent navMeshAgent;
    private Vector3 spriteDirection;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        CrystalController crystal = CrystalController.GetInstance();
        if (crystal != null)
            mainTarget = crystal.transform.position;
        else Debug.LogError("No instance of crystal was found.");
    }

    private void Update()
    {
        if (chaseTimer > chaseDelay)
        {
            if (attackTarget != null)
            {
                float distance = Vector3.Distance(attackTarget.transform.position, transform.position);
                if (distance > maxChaseDistance)
                {
                    attackTarget = null;
                    navMeshAgent.SetDestination(mainTarget);
                }

                else if (distance < attackRange)
                {
                    Debug.Log("Attacking, distance of " + distance);
                    navMeshAgent.ResetPath();
                    spriteDirection = Vector3.ProjectOnPlane(attackTarget.transform.position - transform.position, Camera.main.transform.forward).normalized;
                }

                else navMeshAgent.SetDestination(attackTarget.transform.position);

            }

            else if (mainTarget != null) navMeshAgent.SetDestination(mainTarget);


            chaseTimer = 0.0f;
        }

        chaseTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (spriteAnimator != null)
        {
            if (navMeshAgent.remainingDistance > 0.1f)
                spriteDirection = Vector3.ProjectOnPlane(navMeshAgent.velocity, Camera.main.transform.forward).normalized;

            spriteAnimator.SetDirection(spriteDirection);
            spriteAnimator.SetMoving(navMeshAgent.velocity.magnitude > 0.0f);
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
