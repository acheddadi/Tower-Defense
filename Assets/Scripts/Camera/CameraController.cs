// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// --------------------------------------------------------
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float cameraSmoothing = 0.3F;

    private Vector3 velocity = Vector3.zero;

    // Follow player around while applying some smoothing.
    void Update()
    {
        if (cameraTarget == null) return;
        transform.position = Vector3.SmoothDamp(transform.position, cameraTarget.position + cameraOffset, ref velocity, cameraSmoothing);
    }
}
