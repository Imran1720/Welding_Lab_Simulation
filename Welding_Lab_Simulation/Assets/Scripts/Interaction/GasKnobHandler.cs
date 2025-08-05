using System;
using UnityEngine;

public class GasKnobHandler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationduration;
    [SerializeField] private BlinkHandlerData blinkHandlerData;

    private bool isGasOn;
    private int delta = 1;
    private float rotationtimer;

    private ObjectBlinkerHandler blinker;
    private ToggleState currentToggleState;
    private EventService eventService;

    private void Start()
    {
        rotationtimer = 0;
        blinker = new ObjectBlinkerHandler(blinkHandlerData);
    }

    private void Update()
    {
        blinker?.Update();
        UpdateKnobRotation();
    }

    private void UpdateKnobRotation()
    {
        if (rotationtimer > 0f)
        {
            rotationtimer -= Time.deltaTime;
            RotateKnob();
        }
    }

    private void RotateKnob()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * delta, Space.Self);
    }

    public void ToggleGas()
    {
        isGasOn = !isGasOn;
        delta = isGasOn ? 1 : -1;
        rotationtimer = rotationduration;
        currentToggleState = isGasOn ? ToggleState.ON : ToggleState.OFF;

        eventService.GasOnTrigger.InvokeEvent(isGasOn);
    }

    public void InitializeEvents(EventService eventService) => this.eventService = eventService;
    public void StartBlinking() => blinker.StartBlinking();
    public ToggleState GetToggleState() => currentToggleState;
}