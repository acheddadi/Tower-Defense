// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used as the main timeline which determines
// which event to trigger next based on the current state of
// the game.
// --------------------------------------------------------
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

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

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWindow = CreateWindow(welcomeMessage);
        DisableInput();
    }

    // Update is called once per frame
    void Update()
    {
        // Cycle through each game state.
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

                else if (enemySpawnerController.Victorious())
                {
                    AudioController.Win();
                    currentWindow = CreateWindow(winMessage, 0.5f);
                    DisableInput();
                    currentState++;
                }

                else if(player == null || CrystalController.GetInstance() == null)
                {
                    AudioController.Lose();
                    currentWindow = CreateWindow(loseMessage, 0.5f);
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

            // Credit Screen
            case 3:     
                if (currentWindow == null)
                    Quit();
                break;
        }
    }

    // Helper method to create our popup window.
    private GameObject CreateWindow(string message)
    {
        GameObject window = Instantiate(windowPrefab);
        window.GetComponent<WindowController>().SetMessage(message);
        return window;
    }

    private GameObject CreateWindow(string message, float delay)
    {
        GameObject window = Instantiate(windowPrefab);
        window.GetComponent<WindowController>().SetDelay(delay);
        window.GetComponent<WindowController>().SetMessage(message);
        return window;
    }

    // Helper method to disable user input.
    private void DisableInput()
    {
        inputController.SetState(InputController.State.MENU);
    }

    // Helper method to enable user input.
    private void EnableInput()
    {
        inputController.SetState(InputController.State.GAMEPLAY);
    }

    // Helper method to quit either the standalone game or the game inside the editor.
    private void Quit()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public static  bool GameOver()
    {
        if (instance != null) return instance.currentState > 1;

        throw new System.Exception("No instance of GameOver exists.");
    }
}
