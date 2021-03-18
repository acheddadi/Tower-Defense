using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private float dropRadius = 0.5f;

    private bool applicationQuit = false;

    private void OnApplicationQuit()
    {
        applicationQuit = true;
    }

    private void OnDestroy()
    {
        if (!applicationQuit) Drop();
    }

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
