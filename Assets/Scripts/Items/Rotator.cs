using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20.0f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis * rotateSpeed * Time.deltaTime, Space.World);
    }
}
