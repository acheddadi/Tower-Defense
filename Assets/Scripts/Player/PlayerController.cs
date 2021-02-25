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
    [SerializeField] private float turretPlacementDistance = 1.0f;
    [SerializeField] private float turretPlacementSpeed = 10.0f;


    private CharacterController characterController;
    private Vector3 forwardDirection;
    private Vector3 rightDirection;

    private Vector3 moveDirection;
    private bool isPlacingTurret = false;
    private bool isWalking = false;

    private RaycastHit raycastHit;
    private Vector3 turretPlacementTarget;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        forwardDirection = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        rightDirection = Vector3.Cross(forwardDirection, Vector3.up);
        moveDirection = -forwardDirection;
    }

    void Update()
    {
        playerAnimator.SetMoving(isWalking);
        turretPlacement.transform.position =
            Vector3.Lerp(turretPlacement.transform.position, turretPlacementTarget, Time.deltaTime * turretPlacementSpeed);
    }

    private void FixedUpdate()
    {
        characterController.Move(Vector3.down * gravity * Time.deltaTime);
        turretPlacementTarget = transform.position + moveDirection * turretPlacementDistance;

        if (isPlacingTurret && Physics.Raycast(turretPlacementTarget + Vector3.up * 10.0f, Vector3.down, out raycastHit, Mathf.Infinity, LayerMask.GetMask("Level")))
            turretPlacementTarget = raycastHit.point;
    }

    private void LateUpdate()
    {
        isWalking = false;
    }

    public void Move(Vector2 direction)
    {
        if (playerAnimator != null)
            playerAnimator.SetBlend(direction.normalized);

        moveDirection = Vector3.zero;
        moveDirection -= rightDirection * direction.x;
        moveDirection += forwardDirection * direction.y;
        moveDirection = moveDirection.normalized;

        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
        isWalking = true;
    }

    public void HoldTurretPlacement()
    {
        isPlacingTurret = true;
        turretPlacement.PlaceTurretOverlay();
    }

    public void ReleaseTurretPlacement()
    {
        isPlacingTurret = false;
        turretPlacement.RemoveTurretOverlay();
    }
}
