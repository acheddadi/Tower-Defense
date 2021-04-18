// -------------------------------------------------------
// ASSIGNMENT#3 - MEDIUM FIDELITY PROTOTYPE
// Written by: Ali Cheddadi
// Date: MARCH 18, 2021
// For COSC 2636 - WINTER 2021
// This script is used to control the player object and
// give logic to typical player interactions. It takes care
// of movement, animation, turret placement and resource
// management. It is also used to update certain GUI elements.
// --------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float gravity = 4.0f;
    [SerializeField] private SpriteAnimator spriteAnimator;
    [SerializeField] private TurretPlacement turretPlacement;
    [SerializeField] private float turretPlacementDistance = 1.0f;
    [SerializeField] private float turretPlacementSpeed = 10.0f;
    [SerializeField] private int availableResources = 2;
    [SerializeField] private Text availableResourcesText;


    private CharacterController characterController;
    private Vector3 forwardDirection;
    private Vector3 rightDirection;

    private Vector3 moveDirection;
    private bool isPlacingTurret = false;

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
        // Move our turret's cursor instantly if we turn 180 degrees.
        if (Vector3.Dot(transform.position - turretPlacement.transform.position, transform.position - turretPlacementTarget) < 0)
            turretPlacement.transform.position = turretPlacementTarget;

        // Otherwise smoothly move our turret's cursor towards where the player is facing.
        else turretPlacement.transform.position =
            Vector3.Lerp(turretPlacement.transform.position, turretPlacementTarget, Time.deltaTime * turretPlacementSpeed);

        // Update resources text on our UI.
        if (availableResourcesText != null) availableResourcesText.text = string.Format("x{0:00}", availableResources);
    }

    private void FixedUpdate()
    {
        // Apply basic gravity.
        characterController.Move(Vector3.down * gravity * Time.deltaTime);

        // The turret's cursor uses a target to follow, move that target to always face the player.
        turretPlacementTarget = transform.position + moveDirection * turretPlacementDistance;

        // Snap the turret's cursor's target to the ground.
        if (isPlacingTurret && Physics.Raycast(turretPlacementTarget + Vector3.up * 10.0f, Vector3.down, out raycastHit, Mathf.Infinity, LayerMask.GetMask("Level")))
            turretPlacementTarget = raycastHit.point;
    }

    private void LateUpdate()
    {
        // This probably is not the best way to animate the player, but we're making sure to disable the running cycle at the end of the frame.
        if (spriteAnimator != null) spriteAnimator.SetMoving(false);
    }

    // Helper method to move the player in a given direction (and animate him.)
    public void Move(Vector2 direction)
    {
        if (spriteAnimator != null)
        {
            spriteAnimator.SetMoving(true);
            spriteAnimator.SetDirection(direction.normalized);
        }

        moveDirection = Vector3.zero;
        moveDirection -= rightDirection * direction.x;
        moveDirection += forwardDirection * direction.y;
        moveDirection = moveDirection.normalized;

        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
    }

    // Helper method used by our input manager when holding space bar, places an overlay of the turret.
    public void HoldTurretPlacement()
    {
        if (availableResources == 0) return;

        isPlacingTurret = true;
        turretPlacement.PlaceTurretOverlay();
    }

    // Helper method used by our input manager when releasing space bar, spawns a turret is possible.
    public void ReleaseTurretPlacement()
    {
        if (availableResources == 0) return;

        isPlacingTurret = false;
        if (turretPlacement.RemoveTurretOverlay()) availableResources--;
    }

    // Helper method used when picking up resources.
    public void AddResource()
    {
        availableResources++;
    }
}
