using UnityEngine;

public class BatmanSignalController : MonoBehaviour
{
    public Light signalLight;
    public float rotationSpeed = 10f;

    private bool isOn = false;

    void Start()
    {
        if (signalLight != null)
            signalLight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOn = !isOn;

            if (signalLight != null)
                signalLight.enabled = isOn;
        }
        if (isOn)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
