using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private GameObject destroyPrefab;
    [SerializeField] [Range(0.0f, 10.0f)] private float defensePoints = 0.0f;
    [SerializeField] [Range(0.0f, 100.0f)] private float maxHealth = 100.0f;
    [SerializeField] private HealthBar healthBar;

    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void LoseHealth(float health)
    {
        health -=  health * (1.0f - (10.0f / (10.0f + defensePoints)));
        Debug.Log(name + " lost " + health + " hp.");
        this.health -= health;
        this.health = Mathf.Max(this.health, 0.0f);

        if (healthBar != null) healthBar.SetMainValue(this.health, maxHealth);
        if (this.health == 0.0f) Die();
    }

    public void GainHealth(float health)
    {
        Debug.Log(name + " gained " + health + " hp.");
        this.health += health;
        this.health = Mathf.Min(this.health, maxHealth);

        if (healthBar != null) healthBar.SetMainValue(this.health, maxHealth);
    }

    private void Die()
    {
        AudioController.Hurt();
        Debug.Log(name + " has fallen.");
        if (destroyPrefab != null)
            Instantiate(destroyPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
