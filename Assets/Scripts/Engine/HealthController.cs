// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to give a gameobject health points,
// and allow it to take damage, recover health or die.
// --------------------------------------------------------
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

    // Helper method to remove health from our health controller.
    public void LoseHealth(float health)
    {
        health -=  health * (1.0f - (10.0f / (10.0f + defensePoints)));
        Debug.Log(name + " lost " + health + " hp.");
        this.health -= health;
        this.health = Mathf.Max(this.health, 0.0f);

        if (healthBar != null) healthBar.SetMainValue(this.health, maxHealth);
        if (this.health == 0.0f) Die();
    }

    // Helper method to add health to our health controller.
    public void GainHealth(float health)
    {
        Debug.Log(name + " gained " + health + " hp.");
        this.health += health;
        this.health = Mathf.Min(this.health, maxHealth);

        if (healthBar != null) healthBar.SetMainValue(this.health, maxHealth);
    }

    // Helper method to destroy the player object.
    private void Die()
    {
        AudioController.Hurt();
        Debug.Log(name + " has fallen.");
        if (destroyPrefab != null)
            Instantiate(destroyPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    // Getter for health.
    public float GetHealth()
    {
        return health;
    }

    // Getter to Max health.
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
