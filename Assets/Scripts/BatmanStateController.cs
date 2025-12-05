using UnityEngine;

public enum BatmanState
{
    Normal,
    Stealth,
    Alert
}

public class BatmanStateController : MonoBehaviour
{
    public BatmanState currentState = BatmanState.Normal;

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
        Debug.Log("Batman state changed to: " + newState);
    }
}
