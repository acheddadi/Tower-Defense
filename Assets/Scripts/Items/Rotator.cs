// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to rotate an object over time
// around any given axis.
// --------------------------------------------------------
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20.0f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        // Rotate an object (this is my lazy way of animating an object.)
        transform.Rotate(rotationAxis * rotateSpeed * Time.deltaTime, Space.World);
    }
}
