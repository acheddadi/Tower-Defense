// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// --------------------------------------------------------
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ProjectileController : MonoBehaviour
{
    private float speed = 1.0f;
    private float damage = 1.0f;

    private Animator animator;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError("No animator componenet found in children.");
    }

    private void Update()
    {
        // Go in a straight line if no impacts detected.
        if (!exploded) transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Impact detected, go boom.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.tag == "Enemy")
                other.GetComponent<HealthController>().LoseHealth(damage);

            AudioController.Explode();
            animator.SetTrigger("Explode");
            exploded = true;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
