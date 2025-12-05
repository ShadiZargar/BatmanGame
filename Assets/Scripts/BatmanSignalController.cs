using UnityEngine;

/// <summary>
/// Controls the Bat-Signal spotlight:
/// - Toggle on/off with key B
/// - Slowly rotates while turned on, to simulate scanning the sky.
/// </summary>
public class BatmanSignalController : MonoBehaviour
{
    // The spotlight representing the Bat-Signal beam.
    public Light signalLight;

    // Rotation speed of the Bat-Signal around the Y axis.
    public float rotationSpeed = 10f;

    // Internal flag storing whether the signal is currently on or off.
    private bool isOn = false;

    void Start()
    {
        // At the beginning of the game, make sure the signal is turned off.
        if (signalLight != null)
            signalLight.enabled = false;
    }

    void Update()
    {
        // Toggle Bat-Signal with key B.
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOn = !isOn;

            // Enable/disable the actual light component based on isOn flag.
            if (signalLight != null)
                signalLight.enabled = isOn;
        }

        // While the signal is on, continuously rotate the object
        if (isOn)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
