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
            RaycastHit raycastHit = new RaycastHit();
            Vector3 dropCircle = new Vector3();
            do
            {
                dropCircle = Random.insideUnitCircle.normalized * dropRadius;

            } while (!Physics.Raycast(
                transform.position + Vector3.up * 3.0f + dropCircle, 
                Vector3.down, out raycastHit, Mathf.Infinity, 
                LayerMask.GetMask("Level")
                )
            );
            Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], raycastHit.point, Quaternion.identity);
        }
    }
}
