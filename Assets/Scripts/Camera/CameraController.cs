using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float cameraSmoothing = 0.3F;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (cameraTarget == null) return;
        transform.position = Vector3.SmoothDamp(transform.position, cameraTarget.position + cameraOffset, ref velocity, cameraSmoothing);
    }
}
