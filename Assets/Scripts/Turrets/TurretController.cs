using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float lookAtSpeed = 2.0f;
    [SerializeField] private Transform childToTilt;

    private LinkedList<Collider> inboundEnemy = new LinkedList<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inboundEnemy.First != null)
        {
            Vector3 direction = inboundEnemy.First.Value.transform.position - transform.position;
            Quaternion swivelRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, Vector3.up).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, swivelRotation, lookAtSpeed * Time.deltaTime);

            if (childToTilt != null)
            {
                direction = inboundEnemy.First.Value.transform.position - childToTilt.position;
                Quaternion tiltRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(direction, childToTilt.right).normalized);
                childToTilt.rotation = Quaternion.Slerp(childToTilt.rotation, tiltRotation, lookAtSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();   // Real behaviour
        //PlayerController enemy = other.GetComponent<PlayerController>();    // Testing purposes

        if (enemy != null)
            inboundEnemy.AddLast(other);
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();   // Real behaviour
        //PlayerController enemy = other.GetComponent<PlayerController>();    // Testing purposes

        if (enemy != null)
        {
            try
            {
                inboundEnemy.Remove(other);
            } catch (Exception e)
            {
                Debug.Log("Undocumented enemy just exited my trigger!\n" + e);
            }
        }
    }
}
