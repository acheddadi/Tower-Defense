using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothSpawn : MonoBehaviour
{
    [SerializeField] float spawnSpeed = 6.0f;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale != initialScale) transform.localScale = Vector3.MoveTowards(transform.localScale, initialScale, spawnSpeed * Time.deltaTime);
    }
}
