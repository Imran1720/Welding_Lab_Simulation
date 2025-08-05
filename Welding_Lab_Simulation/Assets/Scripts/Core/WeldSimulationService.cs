using UnityEngine;

public class WeldSimulationService : MonoBehaviour
{
    public static WeldSimulationService Instance { get; private set; }

    [SerializeField] private GasKnobHandler gasKnobHandler;
    [SerializeField] private PowerRegulatorHandler powerRegulatorHandler;

    private EventService eventService;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        eventService = new EventService();

        gasKnobHandler.InitializeEvents(eventService);
        powerRegulatorHandler.AddEvents(eventService);
    }

    private void OnDisable()
    {
        powerRegulatorHandler.RemoveEvents(eventService);
    }
    public void BlinkGasKnob() => gasKnobHandler.StartBlinking();

    public EventService GetEventService() => eventService;

    public GasKnobHandler GetGasKnobHandler() => gasKnobHandler;
}
