// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to slowly scale up objects to their
// initial size in order to give the impression of a smooth
// spawning animation.
// --------------------------------------------------------
using UnityEngine;

public class SmoothSpawn : MonoBehaviour
{
    [SerializeField] float spawnSpeed = 6.0f;

    private Vector3 initialScale;

    private void Awake()
    {
        // Make our object tiny when spawning.
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly bring it back to its original size.
        if (transform.localScale != initialScale) transform.localScale = Vector3.MoveTowards(transform.localScale, initialScale, spawnSpeed * Time.deltaTime);
    }
}
