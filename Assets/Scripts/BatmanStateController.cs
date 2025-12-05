using UnityEngine;
using System.Collections;

/// <summary>
/// Normal  : default mode with normal speed and lighting.
/// Stealth : sneaking mode with lower speed and dimmer lights.
/// Alert   : danger mode with flashing lights and alarm sound.
/// </summary>
public enum BatmanState
{
    Normal,
    Stealth,
    Alert
}

/// <summary>
/// Controls Batman's current state (Normal / Stealth / Alert)
/// and applies the visual/audio effects for each state:
/// - changes main light intensity
/// - turns alert lights on/off and makes them flash
/// - plays/stops alert siren sound
/// </summary>
public class BatmanStateController : MonoBehaviour
{
    [Header("Current State")]
    // The active state of Batman that other scripts (like movement) will also read.
    public BatmanState currentState = BatmanState.Normal;

    [Header("Main Light Settings")]
    // Main scene light that will become brighter/dimmer depending on the state.
    public Light mainLight;
    public float normalIntensity = 2.0f;
    public float stealthIntensity = 0.2f;
    public float alertIntensity = 1.6f;

    [Header("Alert Flashing Lights")]
    // Extra lights used only in Alert mode
    public Light alertLight1;
    public Light alertLight2;
    public float flashInterval = 0.2f; // Time between each flash toggle.

    // Internal flag that tells the flashing coroutine to keep running or stop.
    private bool flashing = false;
    // Reference to the running coroutine so we can stop it safely when state changes.
    private Coroutine flashRoutine = null;

    [Header("Alert Sound")]
    // AudioSource that plays the alert siren when Batman is in Alert state.
    public AudioSource alertSound;

    void Start()
    {
        // Ensure the game always starts in Normal state with correct light settings.
        SetState(BatmanState.Normal);

        // Make sure alert lights are off at the beginning of the game.
        if (alertLight1) alertLight1.enabled = false;
        if (alertLight2) alertLight2.enabled = false;
    }

    void Update()
    {
        // Listen for keyboard input every frame and change the state accordingly.
        HandleStateInput();
    }

    /// <summary>
    /// Handles keyboard shortcuts for switching between states:
    /// N      -> Normal
    /// C      -> Stealth
    /// Space  -> Alert
    /// </summary>
    private void HandleStateInput()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SetState(BatmanState.Normal);

        if (Input.GetKeyDown(KeyCode.C))
            SetState(BatmanState.Stealth);

        if (Input.GetKeyDown(KeyCode.Space))
            SetState(BatmanState.Alert);
    }

    /// <summary>
    /// Central method for changing Batman's state.
    /// - updates the currentState variable
    /// - applies light intensity for that state
    /// - starts/stops flashing lights
    /// - starts/stops alert sound
    /// </summary>
    public void SetState(BatmanState newState)
    {
        currentState = newState;
        Debug.Log("==> STATE CHANGED TO: " + newState);

        // Make sure any previous flashing routine is stopped
        // before applying behavior of the new state.
        StopFlashing();

        switch (newState)
        {
            case BatmanState.Normal:
                // Normal brightness, no alert lights, no siren.
                mainLight.intensity = normalIntensity;
                TurnOffAlertLights();
                if (alertSound) alertSound.Stop();
                break;

            case BatmanState.Stealth:
                // Stealth mode: dim main light, no alert lights, no siren.
                mainLight.intensity = stealthIntensity;
                TurnOffAlertLights(); 
                if (alertSound) alertSound.Stop();
                break;

            case BatmanState.Alert:
                // Alert mode: brighter main light + flashing lights + siren.
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

    /// <summary>
    /// Starts the coroutine that alternates the alert lights
    /// to create a police-style flashing effect in Alert mode.
    /// </summary>
    private void StartFlashing()
    {
        flashing = true;
        flashRoutine = StartCoroutine(FlashLights());
    }

    /// <summary>
    /// Stops the flashing coroutine and ensures alert lights are turned off.
    /// </summary>
    private void StopFlashing()
    {
        flashing = false;

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        TurnOffAlertLights();
    }

    /// <summary>
    /// Coroutine that toggles alert lights on and off in opposite patterns
    /// This runs in a loop while "flashing" is true.
    /// </summary>
    private IEnumerator FlashLights()
    {
        bool toggle = false;

        while (flashing)
        {
            toggle = !toggle;

            if (alertLight1) alertLight1.enabled = toggle;    
            if (alertLight2) alertLight2.enabled = !toggle;   

            // Wait for the configured interval before next toggle.
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
