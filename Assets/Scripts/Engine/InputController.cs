// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used as a state machine to handle user 
// input appropriately based on which state the game is in.
// --------------------------------------------------------
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    public enum State { GAMEPLAY, MENU };
    private State currentState = State.GAMEPLAY;

    private Vector2 direction = Vector3.zero;

    private bool actionButton = false;
    private bool actionButton_D = false;
    private bool actionButton_U = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Layer of abstraction for our input, just incase we want to add gamepad support later.
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        actionButton = Input.GetKey(KeyCode.Space);
        actionButton_D = Input.GetKeyDown(KeyCode.Space);
        actionButton_U = Input.GetKeyUp(KeyCode.Space);

        // Switch between game states to disable user input.
        switch (currentState)
        {
            case State.GAMEPLAY:
                GameplayState();
                break;
            case State.MENU:
                break;
        }
    }

    // Send user inputs to player using this method.
    private void GameplayState()
    {
        if (player == null)
        {
            currentState = State.MENU;
            return;
        }

        if (direction.magnitude > 0.0f) player.Move(direction);
        if (actionButton_D) player.HoldTurretPlacement();
        if (actionButton_U) player.ReleaseTurretPlacement();
    }

    // Setter for our input state.
    public void SetState(State state)
    {
        currentState = state;
    }
}
