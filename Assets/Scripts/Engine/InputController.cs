using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private enum State { GAMEPLAY, MENU };
    private State currentState = State.GAMEPLAY;

    private Vector2 direction = Vector3.zero;

    private bool actionButton = false;
    private bool actionButton_D = false;
    private bool actionButton_U = false;

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        actionButton = Input.GetKey(KeyCode.Space);
        actionButton_D = Input.GetKeyDown(KeyCode.Space);
        actionButton_U = Input.GetKeyUp(KeyCode.Space);


        switch (currentState)
        {
            case State.GAMEPLAY:
                GameplayState();
                break;
            case State.MENU:
                break;
        }
    }

    private void GameplayState()
    {
        if (direction.magnitude > 0.0f) player.Move(direction);
        if (actionButton_D) player.HoldTurretPlacement();
        if (actionButton_U) player.ReleaseTurretPlacement();
    }
}
