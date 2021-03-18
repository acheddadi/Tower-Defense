using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private string welcomeMessage;
    [SerializeField] private string winMessage;
    [SerializeField] private string loseMessage;
    [SerializeField] private string creditsMessage;
    [SerializeField] private InputController inputController;
    [SerializeField] private EnemySpawnerController enemySpawnerController;
    [SerializeField] private GameObject windowPrefab;

    private GameObject currentWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
