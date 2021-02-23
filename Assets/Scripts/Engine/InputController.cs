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
        if (Input.anyKey)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            actionButton = Input.GetKey(KeyCode.Space);
            actionButton_D = Input.GetKeyDown(KeyCode.Space);
            actionButton_U = Input.GetKeyUp(KeyCode.Space);


            switch (currentState)
            {
                case State.GAMEPLAY:
                    player.Move(direction);
                    if (actionButton) player.PlaceTurret();
                    break;
                case State.MENU:
                    break;
            }
        }
    }
}
