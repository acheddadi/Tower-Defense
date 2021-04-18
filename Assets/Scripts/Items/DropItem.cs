// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to drop an item when the game object
// in question is destroyed.
// --------------------------------------------------------
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private float dropRadius = 0.5f;

    private bool applicationQuit = false;

    // Make sure the object isn't being destroyed because the user quit the game, other forget spawning new objects.
    private void OnApplicationQuit()
    {
        applicationQuit = true;
    }

    private void OnDestroy()
    {
        if (!applicationQuit) Drop();
    }

    // Helper method to drop an item when object is destroyed.
    private void Drop()
    {
        if (itemPrefabs != null)
        {
            Vector2 dropCircle = Random.insideUnitCircle.normalized * dropRadius;
            Vector3 spawnPoint = new Vector3(transform.position.x + dropCircle.x, transform.position.y, transform.position.z + dropCircle.y);
            Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], spawnPoint, Quaternion.identity);
        }
    }
}
