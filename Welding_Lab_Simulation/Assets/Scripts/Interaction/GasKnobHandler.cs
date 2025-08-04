using System;
using UnityEngine;

public class GasKnobHandler : MonoBehaviour
{
    private bool isGasOn;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationduration;
    [SerializeField] private BlinkHandlerData blinkHandlerData;

    private int delta = 1;
    private float rotationtimer;
    private ToggleState currentToggleState;
    private ObjectBlinkerHandler blinker;
    private void Start()
    {
        rotationtimer = 0;
        blinker = new ObjectBlinkerHandler(blinkHandlerData);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            StartBlinking();
        }
        if (blinker != null)
        {
            blinker.Update();
        }
        if (rotationtimer > 0.1f)
        {
            rotationtimer -= Time.deltaTime;
            RotateKnob();
        }
    }
    private void RotateKnob()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * delta);
    }

    public void ToggleGas()
    {
        isGasOn = !isGasOn;
        delta = isGasOn ? 1 : -1;
        rotationtimer = rotationduration;
        currentToggleState = isGasOn ? ToggleState.ON : ToggleState.OFF;
    }

    public void StopRotation() => rotationtimer = 0;
    public ToggleState GetToggleState() => currentToggleState;

    public void StartBlinking() => blinker.StartBlinking();
}

[Serializable]
public class BlinkHandlerData
{
    public MeshRenderer renderer;
    public Material highlightMaterial;
    public float duration;
    public int blinkCount;
}
