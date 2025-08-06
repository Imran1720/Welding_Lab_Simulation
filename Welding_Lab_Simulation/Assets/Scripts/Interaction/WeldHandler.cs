using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeldHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private BlinkHandlerData blinkHandlerData;
    [SerializeField] private InputActionProperty primaryButtonAction;

    // State
    private PowerLevel currentPowerLevel;
    private bool canWeld;
    private bool isWelding;
    private bool isGasOn;

    // References
    private Transform weldDragPoint;
    private EventService eventService;
    private ParticleSystem weldParticles;
    private WeldZoneHandler weldZoneHandler;
    private ObjectBlinkerHandler blinkerHandler;

    private void Start()
    {
        blinkerHandler = new ObjectBlinkerHandler(blinkHandlerData);
        eventService = WeldSimulationService.Instance.GetEventService();

        eventService.GasOnTrigger.AddEventListener(GasOnTriggered);
        eventService.OnPowerLevelChanged.AddEventListener(OnPowerLevelChanged);
    }

    private void OnEnable()
    {
        primaryButtonAction.action.canceled += OnPrimaryActionReleased;
        primaryButtonAction.action.performed += OnPrimaryActionTriggered;
    }

    private void OnDisable()
    {
        primaryButtonAction.action.canceled -= OnPrimaryActionReleased;
        primaryButtonAction.action.performed -= OnPrimaryActionTriggered;
    }

    private void Update()
    {
        blinkerHandler.Update();

        if (ShouldPerformWelding())
        {
            AlignWeldDragPoint();
        }
        else
        {
            StopWeldParticles();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WeldZoneHandler>(out weldZoneHandler) && isGasOn)
        {
            canWeld = true;

            weldZoneHandler.EnableWeld(currentPowerLevel);
            weldDragPoint = weldZoneHandler.GetDragPoint();
            weldParticles = weldDragPoint.GetComponent<ParticleSystem>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<WeldZoneHandler>(out weldZoneHandler))
        {
            StopWeldParticles();
            weldDragPoint = null;
            weldParticles = null;

            canWeld = false;
        }
    }

    private bool ShouldPerformWelding()
    {
        return canWeld && weldDragPoint != null && isWelding && isGasOn;
    }

    private void AlignWeldDragPoint()
    {
        Vector3 position = weldDragPoint.position;
        weldDragPoint.position = new Vector3(transform.position.x, position.y, position.z);
    }

    private void PlayWeldParticles()
    {
        if (weldParticles != null && !weldParticles.isPlaying)
        {
            weldParticles.Play();
        }
    }

    private void StopWeldParticles()
    {
        if (weldParticles != null)
        {
            weldParticles.Stop();
            weldParticles.Clear();
        }
    }

    private void OnPrimaryActionTriggered(InputAction.CallbackContext callbackContext)
    {
        isWelding = true;
        if (isGasOn && canWeld)
        {
            PlayWeldParticles();
        }
    }

    private void OnPrimaryActionReleased(InputAction.CallbackContext callbackContext) => isWelding = false;

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
