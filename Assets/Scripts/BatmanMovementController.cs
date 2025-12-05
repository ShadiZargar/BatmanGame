using UnityEngine;

public class BatmanMovementController : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float boostSpeed = 10f;
    public float rotationSpeed = 100f;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        HandleSpeed();
        HandleMovement();
    }

    private void HandleSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            currentSpeed = boostSpeed;
        else
            currentSpeed = normalSpeed;
    }

    private void HandleMovement()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * move * currentSpeed * Time.deltaTime);
        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);
    }
}
