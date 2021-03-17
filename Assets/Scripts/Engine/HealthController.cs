using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private GameObject destroyPrefab;
    [SerializeField] [Range(0.0f, 10.0f)] private float defensePoints = 0.0f;
    [SerializeField] [Range(0.0f, 100.0f)] private float healthPoints = 100.0f;

    public void LoseHealth(float health)
    {
        health -=  health * (1.0f - (10.0f / (10.0f + defensePoints)));
        Debug.Log(name + " lost " + health + " hp.");
        this.healthPoints -= health;
        this.healthPoints = Mathf.Max(this.healthPoints, 0.0f);

        if (this.healthPoints == 0.0f) Die();
    }

    public void GainHealth(float health)
    {
        Debug.Log(name + " gained " + health + " hp.");
        this.healthPoints += health;
        this.healthPoints = Mathf.Min(this.healthPoints, 100.0f);
    }

    private void Die()
    {
        Debug.Log(name + " has fallen.");
        if (destroyPrefab != null)
            Instantiate(destroyPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
