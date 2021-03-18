using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ProjectileController : MonoBehaviour
{
    private float speed = 1.0f;
    private float damage = 1.0f;

    private Animator animator;
    private bool exploded = false;

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError("No animator componenet found in children.");
    }

    private void Update()
    {
        if (timer > 5.0f) DestroyProjectile();

        if (!exploded) transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (other.tag == "Enemy")
                other.GetComponent<HealthController>().LoseHealth(damage);

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
