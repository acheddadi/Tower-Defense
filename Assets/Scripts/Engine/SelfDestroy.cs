using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3.0f;
    [SerializeField] SpriteRenderer spriteRenderer;

    private Color fadeColor = Color.white;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (timer > lifeTime)
        {
            if (spriteRenderer != null && spriteRenderer.color.a > 0.0f)
            {
                fadeColor.a -= Time.deltaTime;
                spriteRenderer.color = fadeColor;
            }

            else Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
