// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// --------------------------------------------------------
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
        // Auto destroy object after a while.
        if (timer > lifeTime)
        {
            // If sprite is referenced, then fade it out before destroying it.
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
