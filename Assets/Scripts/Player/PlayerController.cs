using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float gravity = 4.0f;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private TurretPlacement turretPlacement;

    private CharacterController characterController;
    private Vector3 forwardDirection;
    private Vector3 rightDirection;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        forwardDirection = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        rightDirection = Vector3.Cross(forwardDirection, Vector3.up);
    }

    void Update()
    {
        characterController.Move(Vector3.down * gravity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerAnimator.SetMoving(characterController.velocity.magnitude > 0.1f);
    }

    public void Move(Vector2 direction)
    {
        if (playerAnimator != null)
            playerAnimator.SetBlend(direction.normalized);

        Vector3 moveDirection = Vector3.zero;
        moveDirection -= rightDirection * direction.x;
        moveDirection += forwardDirection * direction.y;
        moveDirection = moveDirection.normalized;

        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
    }

    public void PlaceTurret()
    {
        if (turretPlacement != null)
            turretPlacement.PlaceTurret();
    }
}
