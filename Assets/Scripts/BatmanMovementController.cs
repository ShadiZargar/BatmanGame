using UnityEngine;

public class BatmanMovementController : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float slowSpeed = 2f;
    public float alertSpeed = 10f;
    public float rotationSpeed = 100f;

    private float currentSpeed;

    private BatmanStateController stateController;

    private void Start()
    {
        stateController = GetComponent<BatmanStateController>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        UpdateSpeedByState();
        HandleMovement();
    }

    private void UpdateSpeedByState()
    {
        switch (stateController.currentState)
        {
            case BatmanState.Normal:
                currentSpeed = normalSpeed;

                // BOOST with SHIFT
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    currentSpeed = normalSpeed * 1.8f;  // یا یک مقدار ثابت مثل 9

                break;

            case BatmanState.Stealth:
                currentSpeed = slowSpeed;
                break;

            case BatmanState.Alert:
                currentSpeed = alertSpeed;
                break;
        }
    }

    private void HandleMovement()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * move * currentSpeed * Time.deltaTime);
        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);
    }
}
