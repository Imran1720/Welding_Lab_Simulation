using UnityEngine;

public class WeldSimulationService : MonoBehaviour
{
    public static WeldSimulationService Instance { get; private set; }

    [SerializeField] private GasKnobHandler gasKnobHandler;

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
    }

    public EventService GetEventService() => eventService;
}
