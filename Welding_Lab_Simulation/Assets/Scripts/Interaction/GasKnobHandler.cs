using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GasKnobHandler : MonoBehaviour
{
    private bool isGasOn;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationduration;

    private int delta = 1;

    private float rotationtimer;

    private void Start()
    {
        rotationtimer = 0;
    }

    private void Update()
    {
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
    }

    public void StopRotation() => rotationtimer = 0;
}
