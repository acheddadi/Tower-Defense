using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform mainBar;
    [SerializeField] private RectTransform secondaryBar;

    private Image mainImage;
    private Image secondaryImage;

    private void Start()
    {
        if (mainBar != null) mainImage = mainBar.GetComponent<Image>();
        else Debug.Log("Main bar not set in object: " + name);

        if (secondaryBar != null) secondaryImage = secondaryBar.GetComponent<Image>();
    }

    public void SetMainValue(float health, float maxHealth)
    {
        if (mainBar == null) return;

        if (maxHealth == 0.0f)
        {
            mainBar.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            return;
        }

        mainBar.localScale = new Vector3(health / maxHealth, 1.0f, 1.0f);

        if (health / maxHealth < 0.34f) mainImage.color = Color.red;
        else mainImage.color = Color.green;
    }

    public void SetSecondaryValue(float health, float maxHealth, Color normal, Color low)
    {
        if (secondaryBar == null) return;

        if (maxHealth == 0.0f)
        {
            secondaryBar.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            return;
        }

        secondaryBar.localScale = new Vector3(health / maxHealth, 1.0f, 1.0f);

        if (health / maxHealth < 0.34f) secondaryImage.color = low;
        else secondaryImage.color = normal;
    }
}
