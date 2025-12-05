using UnityEngine;

/// <summary>
/// Handles Batman's movement based on keyboard input
/// and adapts movement speed according to the current BatmanState:
/// - Normal  : normalSpeed (with optional SHIFT boost)
/// - Stealth : slowSpeed
/// - Alert   : alertSpeed
/// </summary>
public class BatmanMovementController : MonoBehaviour
{
    // Base speeds for each state.
    public float normalSpeed = 5f;
    public float slowSpeed = 2f;
    public float alertSpeed = 10f;

    // Rotation speed around Y axis when player presses left/right.
    public float rotationSpeed = 100f;

    // Runtime speed that is actually used for this frame (depends on state).
    private float currentSpeed;

    // Reference to BatmanStateController to read the active state.
    private BatmanStateController stateController;

    private void Start()
    {
        // Get reference to the state controller on the same GameObject.
        stateController = GetComponent<BatmanStateController>();

        // Start with normal speed by default.
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        // Every frame:
        // 1) update speed based on the current state (and SHIFT boost)
        // 2) apply movement & rotation based on player input.
        UpdateSpeedByState();
        HandleMovement();
    }


    private void UpdateSpeedByState()
    {
        switch (stateController.currentState)
        {
            case BatmanState.Normal:
                currentSpeed = normalSpeed;

                // BOOST speed when either Shift key is held down.
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    currentSpeed = normalSpeed * 1.8f; 

                break;

            case BatmanState.Stealth:
                // Slower movement in stealth mode.
                currentSpeed = slowSpeed;
                break;

            case BatmanState.Alert:
                // Faster movement in alert mode.
                currentSpeed = alertSpeed;
                break;
        }
    }

    /// <summary>
    /// Reads horizontal/vertical axes and moves/rotates Batman accordingly:
    /// - Vertical axis (W/S or Up/Down) moves forward/backward.
    /// - Horizontal axis (A/D or Left/Right) rotates left/right.
    /// </summary>
    private void HandleMovement()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Move forward/backward in local Z direction based on currentSpeed.
        transform.Translate(Vector3.forward * move * currentSpeed * Time.deltaTime);

        // Rotate around Y axis based on horizontal input.
        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);
    }
}
