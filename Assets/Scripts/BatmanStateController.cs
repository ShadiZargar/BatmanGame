using UnityEngine;
using System.Collections;

public enum BatmanState
{
    Normal,
    Stealth,
    Alert
}

public class BatmanStateController : MonoBehaviour
{
    [Header("Current State")]
    public BatmanState currentState = BatmanState.Normal;

    [Header("Main Light Settings")]
    public Light mainLight;
    public float normalIntensity = 1f;
    public float stealthIntensity = 0.2f;
    public float alertIntensity = 1.4f;

    [Header("Alert Flashing Lights")]
    public Light alertLight1;
    public Light alertLight2;
    public float flashInterval = 0.2f;

    private bool flashing = false;
    private Coroutine flashRoutine = null;

    [Header("Alert Sound")]
    public AudioSource alertSound;

    void Start()
    {
        // ensure alert lights are off at start
        if (alertLight1) alertLight1.enabled = false;
        if (alertLight2) alertLight2.enabled = false;
    }

    void Update()
    {
        HandleStateInput();
    }

    private void HandleStateInput()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SetState(BatmanState.Normal);

        if (Input.GetKeyDown(KeyCode.C))
            SetState(BatmanState.Stealth);

        if (Input.GetKeyDown(KeyCode.Space))
            SetState(BatmanState.Alert);
    }

    public void SetState(BatmanState newState)
    {
        currentState = newState;
        Debug.Log("==> STATE CHANGED TO: " + newState);

        StopFlashing(); // stop old coroutine

        switch (newState)
        {
            case BatmanState.Normal:
                mainLight.intensity = normalIntensity;
                TurnOffAlertLights();
                if (alertSound) alertSound.Stop();
                break;

            case BatmanState.Stealth:
                mainLight.intensity = stealthIntensity;
                TurnOffAlertLights();
                if (alertSound) alertSound.Stop();
                break;

            case BatmanState.Alert:
                mainLight.intensity = alertIntensity;
                TurnOffAlertLights();
                StartFlashing();
                if (alertSound) alertSound.Play();
                break;
        }
    }

    private void TurnOffAlertLights()
    {
        if (alertLight1) alertLight1.enabled = false;
        if (alertLight2) alertLight2.enabled = false;
    }

    private void StartFlashing()
    {
        flashing = true;
        flashRoutine = StartCoroutine(FlashLights());
    }

    private void StopFlashing()
    {
        flashing = false;

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        TurnOffAlertLights();
    }

    private IEnumerator FlashLights()
{
    bool toggle = false;

    while (flashing)
    {
        toggle = !toggle;

        if (alertLight1) alertLight1.enabled = toggle;     
        if (alertLight2) alertLight2.enabled = !toggle;   

        yield return new WaitForSeconds(flashInterval);
    }
}

}
