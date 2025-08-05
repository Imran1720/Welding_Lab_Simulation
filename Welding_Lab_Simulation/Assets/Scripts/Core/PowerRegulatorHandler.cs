using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerRegulatorHandler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] BlinkHandlerData blinkHandlerData;


    private int rotationDelta = 1;
    private PowerLevel currentPowerLevel;
    private ObjectBlinkerHandler blinkerHandler;

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

        switch (currentPowerLevel)
        {
            case PowerLevel.LOW:
            default:
                RotateRegulatorTo(80);
                break;
            case PowerLevel.MEDIUM:
                RotateRegulatorTo(150);
                break;
            case PowerLevel.HIGH:
                RotateRegulatorTo(250);
                break;
        }
    }

    private void RotateRegulatorTo(float targetRotation)
    {
        if (!WeldSimulationService.Instance.GetGasKnobHandler().IsGasOn())
        {
            powerText.text = "" + 0;
            return;
        }
        if (Mathf.Abs(transform.localEulerAngles.y - targetRotation) <= 1f)
        {
            return;
        }
        rotationDelta = transform.localEulerAngles.y - targetRotation <= 0 ? 1 : -1;
        transform.Rotate(0, rotationSpeed * Time.deltaTime * rotationDelta, 0, Space.Self);
        powerText.text = "" + (int)transform.localEulerAngles.y;
    }

    public void StopRegulatorBlinking()
    {
        blinkerHandler.StopBlinking();
    }

    public void ChangePowerLevel()
    {
        if (!WeldSimulationService.Instance.GetGasKnobHandler().IsGasOn())
        {
            WeldSimulationService.Instance.GetGasKnobHandler().StartBlinking();
            return;
        }
        currentPowerLevel++;
        if ((int)currentPowerLevel > 3)
        {
            currentPowerLevel = 0;
        }
    }

    public void BlinkGasKnob()
    {
        GasKnobHandler gasKnobHandler = WeldSimulationService.Instance.GetGasKnobHandler();
        if (!gasKnobHandler.IsGasOn())
        {
            gasKnobHandler.StartBlinking();
        }
    }
}
