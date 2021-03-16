using System.Collections;
using System.Collections.Generic;
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

    public void SetDirection(Vector2 direction)
    {
        animator.SetFloat("XAxis", direction.x);
        animator.SetFloat("YAxis", direction.y);
    }

    public void SetMoving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }
}
