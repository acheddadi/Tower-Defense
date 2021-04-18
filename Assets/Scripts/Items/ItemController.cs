// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to set the type of resource of an item,
// along with setting its behaviour.
// --------------------------------------------------------
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemController : MonoBehaviour
{
    private enum ItemType {  RESOURCE, HEALTH };
    [SerializeField] ItemType itemType = ItemType.RESOURCE;

    // If the player enters item's trigger, give him stuff and self destroy.
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
            AudioController.PickUp();
            Destroy(gameObject);
        }
    }
}
