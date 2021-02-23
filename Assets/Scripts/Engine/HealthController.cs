using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float health = 100.0f;

    public void LoseHealth(float health)
    {
        Debug.Log(name + " lost " + health + " hp.");
        this.health -= health;
        this.health = Mathf.Max(this.health, 0.0f);

        if (this.health == 0.0f) Die();
    }

    public void GainHealth(float health)
    {
        Debug.Log(name + " gained " + health + " hp.");
        this.health += health;
        this.health = Mathf.Min(this.health, 100.0f);
    }

    private void Die()
    {
        Debug.Log(name + " has fallen.");
        Destroy(gameObject);
    }
}
