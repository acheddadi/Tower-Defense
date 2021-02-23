using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class CrystalController : MonoBehaviour
{
    private static CrystalController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static CrystalController GetInstance()
    {
        return instance;
    }
}
