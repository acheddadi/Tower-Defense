// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script implements a singleton pattern so that other
// objects may refer to it more easily.
// --------------------------------------------------------
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class CrystalController : MonoBehaviour
{
    // Singleton pattern just to easily refer to our Crystal (Not actually that useful.)
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
