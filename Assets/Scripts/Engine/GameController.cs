using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] [TextArea] private string welcomeMessage;
    [SerializeField] [TextArea] private string winMessage;
    [SerializeField] [TextArea] private string loseMessage;
    [SerializeField] [TextArea] private string creditsMessage;
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
