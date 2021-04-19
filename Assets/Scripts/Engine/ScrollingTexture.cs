using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    private enum ScrollAxis { X_AXIS, Y_AXIS, XY_AXIS };
    [SerializeField] private ScrollAxis scrollDirection = ScrollAxis.XY_AXIS;
    [SerializeField] private float scrollSpeed = 1.0f;
    private Material material;

    private Vector2 vectorOffset = Vector2.zero;
    private float offset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        if (offset > 1.0f) offset -= 1.0f;

        switch (scrollDirection)
        {
            case ScrollAxis.XY_AXIS:
                vectorOffset.x = vectorOffset.y = offset;
                break;
            case ScrollAxis.X_AXIS:
                vectorOffset.x = offset;
                vectorOffset.y = 0.0f;
                break;
            case ScrollAxis.Y_AXIS:
                vectorOffset.y = offset;
                vectorOffset.x = 0.0f;
                break;
        }

        material.SetTextureOffset("_MainTex", vectorOffset);
    }
}
