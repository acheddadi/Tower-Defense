// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to easily sent data which is used
// to change the sprites used for bot the player and enemies.
// The data used is a direction vector, as well as a boolean flag.
// --------------------------------------------------------
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpriteAnimator : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Helper method to change the blend tree parameters.
    public void SetDirection(Vector2 direction)
    {
        animator.SetFloat("XAxis", direction.x);
        animator.SetFloat("YAxis", direction.y);
    }

    // Helper method to enable the walk cycle.
    public void SetMoving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    // Helper method to play the attack animation.
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    // Helper method to play the hurt animation (not used yet.)
    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }
}
