using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerRegulatorHandler : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 50f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI powerText;

    [Header("Blink Settings")]
    [SerializeField] private BlinkHandlerData blinkHandlerData;

    private EventService eventService;

    private ObjectBlinkerHandler blinkerHandler;
    private PowerLevel currentPowerLevel = PowerLevel.LOW;
    private int rotationDelta = 1;
    private bool isGasOn;

    private Dictionary<PowerLevel, int> powerLevelAngles = new Dictionary<PowerLevel, int>()
    {
        { PowerLevel.LOW,80},
        { PowerLevel.MEDIUM,150},
        { PowerLevel.HIGH,250}
    };

    public void AddEvents(EventService eventService)
    {
        eventService.GasOnTrigger.AddEventListener(GasOnTriggered);
    }

    public void RemoveEvents(EventService eventService)
    {
        eventService.GasOnTrigger.RemoveEventListener(GasOnTriggered);
    }

    private void Start()
    {
        blinkerHandler = new ObjectBlinkerHandler(blinkHandlerData);
        blinkerHandler.StartBlinking();
    }

    private void Update()
    {
        blinkerHandler?.Update();
        UpdatePowerRegulator();
    }

    private void UpdatePowerRegulator()
    {
        if (!isGasOn)
        {
            SetPowerUIText(0);
            return;
        }

        RotateRegulatorTo(powerLevelAngles[currentPowerLevel]);
    }

    private void RotateRegulatorTo(float targetRotation)
    {
        if (Mathf.Abs(transform.localEulerAngles.y - targetRotation) <= 1f)
        {
            SetPowerUIText(powerLevelAngles[currentPowerLevel]);
            return;
        }
        rotationDelta = transform.localEulerAngles.y - targetRotation <= 0 ? 1 : -1;
        transform.Rotate(0, rotationSpeed * Time.deltaTime * rotationDelta, 0, Space.Self);
        SetPowerUIText((int)transform.localEulerAngles.y);
    }

    public void ChangePowerLevel()
    {
        if (!isGasOn)
        {
            WeldSimulationService.Instance.BlinkGasKnob();
            return;
        }
        currentPowerLevel++;
        if ((int)currentPowerLevel > 2)
        {
            currentPowerLevel = 0;
        }
    }

    private void GasOnTriggered(bool isGasOn) => this.isGasOn = isGasOn;
    private void SetPowerUIText(int value) => powerText.text = "" + value;

    public void StopRegulatorBlinking() => blinkerHandler.StopBlinking();
    public void BlinkGasKnob() => WeldSimulationService.Instance.BlinkGasKnob();

}
