// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to control an enemy object. It sends
// position data to a Nav Mesh agent, which in turn moves
// the enemy towards its destination. If a target becomes
// in range, this behaviour is overridden and the enemy
// begins to follow its target. Once a target is close
// enough, the enemy will begin attacking its target.
// --------------------------------------------------------
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
    }

    private void Update()
    {
        // If Crystal was destroyed, have all enemies stop all movement.
        if (CrystalController.GetInstance() == null)
        {
            if (navMeshAgent.velocity.magnitude > 0.0f) DelayReaction();
            return;
        }

        // If no target detected, move towards the Crystal.
        if (attackTarget == null)
        {
            if (movementTimer > movementDelay)
            {
                navMeshAgent.SetDestination(CrystalController.GetInstance().transform.position);
                movementTimer = 0.0f;
            }
        }

        // Target is detected.
        else
        {
            // If close enough to attack target.
            if (Vector3.Distance(attackTarget.transform.position, transform.position) < attackRange)
            {
                navMeshAgent.velocity = Vector3.zero;
                navMeshAgent.ResetPath();

                // Start attacking after delay has been reached.
                if (attackTimer > attackDelay)
                {
                    // Align sprite to always face target.
                    spriteDirection = Vector3.ProjectOnPlane(attackTarget.transform.position - transform.position, Camera.main.transform.forward).normalized;
                    if (spriteAnimator != null) spriteAnimator.Attack();

                    // Player sound and remove health from target.
                    AudioController.Attack();
                    attackTarget.LoseHealth(attackStrength);
                    attackTimer = 0.0f;
                }

                movementTimer = 0.0f;
                attackTimer += Time.deltaTime;
            }

            // Not close enough to attack, keep chasing target.
            else if (movementTimer > movementDelay)
            {
                navMeshAgent.SetDestination(attackTarget.transform.position);
                movementTimer = 0.0f;
            }
        }

        movementTimer += Time.deltaTime;
    }

    // Helper method to stop enemy in its path.
    private void DelayReaction()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.ResetPath();
        movementTimer = 0.0f;
        attackTimer = 0.0f;
    }

    // Physics update.
    private void FixedUpdate()
    {
        // If we animator controlled was assigned.
        if (spriteAnimator != null)
        {
            // Make sure that we're moving if we want to rotate the sprite (looks better than way.)
            if (navMeshAgent.velocity.magnitude > 0.0f)
                spriteDirection = Vector3.ProjectOnPlane(navMeshAgent.velocity, Camera.main.transform.forward).normalized;

            // Look at target.
            spriteAnimator.SetDirection(spriteDirection);
            spriteAnimator.SetMoving(navMeshAgent.velocity.magnitude > 0.0f);
        }

        // If we are too far from the target, give up the chase.
        if (attackTarget != null && Vector3.Distance(transform.position, attackTarget.transform.position) > giveUpChaseRange)
        {
            DelayReaction();
            attackTarget = null;
        }
    }

    // Target acquired (beep beep boop, fire missiles!)
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            HealthController target = other.GetComponent<HealthController>();

            if (target != null && other.tag != "Enemy" && attackTarget != target)
            {
                DelayReaction();
                attackTarget = target;
            }
        }
    }
}
