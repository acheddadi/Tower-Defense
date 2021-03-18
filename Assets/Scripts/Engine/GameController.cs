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
    [SerializeField] private PlayerController player;
    [SerializeField] private EnemySpawnerController enemySpawnerController;
    [SerializeField] private GameObject windowPrefab;

    private GameObject currentWindow;
    private int currentState = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWindow = CreateWindow(welcomeMessage);
        DisableInput();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            // Welcome Message
            case 0:     
                if (currentWindow == null)
                {
                    EnableInput();
                    currentState++;
                }
                break;

            // Enemy Spawner
            case 1:     
                if (!enemySpawnerController.Started())
                    enemySpawnerController.StartEnemySpawner();

                else if (enemySpawnerController.Wiped())
                {
                    currentWindow = CreateWindow(winMessage);
                    DisableInput();
                    currentState++;
                }

                else if(player == null)
                {
                    currentWindow = CreateWindow(loseMessage);
                    DisableInput();
                    currentState++;
                }
                break;

            // Win/Lose Message
            case 2:
                if (currentWindow == null)
                {
                    currentWindow = CreateWindow(creditsMessage);
                    currentState++;
                }
                break;

            case 3:     // Credit Screen
                if (currentWindow == null)
                    Quit();
                break;
        }
    }

    private GameObject CreateWindow(string message)
    {
        GameObject window = Instantiate(windowPrefab);
        window.GetComponent<WindowController>().SetMessage(message);
        return window;
    }

    private void DisableInput()
    {
        inputController.SetState(InputController.State.MENU);
    }

    private void EnableInput()
    {
        inputController.SetState(InputController.State.GAMEPLAY);
    }

    private void Quit()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
