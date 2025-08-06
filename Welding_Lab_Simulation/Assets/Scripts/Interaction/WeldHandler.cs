using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class WeldHandler : MonoBehaviour
{
    private Transform weldDragpoint;
    [SerializeField] private BlinkHandlerData blinkHandlerData;
    [SerializeField] private InputActionProperty primaryButtonAction;
    private PowerLevel currentPowerLevel;
    private WeldZoneHandler weldZoneHandler;

    private bool canWeld;
    private bool isWelding = false;
    private bool isGasOn = false;
    private ObjectBlinkerHandler blinkerHandler;

    private void Start()
    {
        blinkerHandler = new ObjectBlinkerHandler(blinkHandlerData);

        WeldSimulationService.Instance.GetEventService().OnPowerLevelChanged.AddEventListener(OnPowerLevelChanged);
        WeldSimulationService.Instance.GetEventService().GasOnTrigger.AddEventListener(GasOnTriggered);
    }

    private void OnEnable()
    {
        primaryButtonAction.action.performed += OnPrimaryActionTriggered;
        primaryButtonAction.action.canceled += OnPrimaryActionLeft;
    }

    private void OnDisable()
    {
        primaryButtonAction.action.performed -= OnPrimaryActionTriggered;
        primaryButtonAction.action.canceled -= OnPrimaryActionLeft;
    }

    private void Update()
    {
        blinkerHandler.Update();
        if (canWeld && weldDragpoint != null && isWelding && isGasOn)
        {
            Vector3 newPosition = new Vector3(transform.position.x, weldDragpoint.position.y, weldDragpoint.position.z);
            weldDragpoint.position = newPosition;
        }
        else if (weldDragpoint != null)
        {
            weldDragpoint.GetComponent<ParticleSystem>().Stop();
            weldDragpoint.GetComponent<ParticleSystem>().Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WeldZoneHandler>(out weldZoneHandler) && isGasOn)
        {
            weldZoneHandler.EnableWeld(currentPowerLevel);
            weldDragpoint = weldZoneHandler.GetDragPoint();
            canWeld = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<WeldZoneHandler>(out weldZoneHandler))
        {
            weldDragpoint?.GetComponent<ParticleSystem>().Stop();
            weldDragpoint?.GetComponent<ParticleSystem>().Clear();
            weldDragpoint = null;
            canWeld = false;
        }
    }

    private void OnPrimaryActionTriggered(InputAction.CallbackContext callbackContext)
    {
        isWelding = true;
        if (isGasOn && canWeld)
        {
            weldDragpoint.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnPrimaryActionLeft(InputAction.CallbackContext callbackContext)
    {
        isWelding = false;
    }

    private void OnPowerLevelChanged(PowerLevel powerLevel)
    {
        currentPowerLevel = powerLevel;
        weldZoneHandler?.DisableWeld();
    }

    private void GasOnTriggered(bool value)
    {
        isGasOn = value;
        blinkerHandler.StartBlinking();
    }
}
