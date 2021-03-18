using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemController : MonoBehaviour
{
    private enum ItemType {  RESOURCE, HEALTH };
    [SerializeField] ItemType itemType = ItemType.RESOURCE;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == "Player")
        {
            switch (itemType)
            {
                case ItemType.RESOURCE:
                    other.GetComponent<PlayerController>().AddResource();
                    break;
                case ItemType.HEALTH:
                    other.GetComponent<HealthController>().GainHealth(10.0f);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
