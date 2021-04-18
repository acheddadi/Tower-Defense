// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used rotate sprites so that they always
// directly face the camera's forward vector. This gives
// the impression of a 2.5D game.
// --------------------------------------------------------
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Align our sprite's forward vector to be parallel to the camera's forward vector (This is how the 2.5D magic happens.)
        transform.LookAt(transform.position - Camera.main.transform.forward);
    }
}