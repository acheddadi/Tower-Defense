using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform foregroundBar;
    private Image image;

    private void Awake()
    {
        if (foregroundBar != null) image = foregroundBar.GetComponent<Image>();
        else Debug.Log("Reference to Foreground Bar is not set!");
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (foregroundBar != null)
        {
            foregroundBar.localScale = new Vector3(health / maxHealth, 1.0f, 1.0f);
            if (health / maxHealth < 0.34f) image.color = Color.red;
            else image.color = Color.green;
        }
    }
}
