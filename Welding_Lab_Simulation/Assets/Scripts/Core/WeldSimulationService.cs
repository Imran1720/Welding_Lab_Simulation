using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldSimulationService : MonoBehaviour
{
    public static WeldSimulationService Instance { get; private set; }

    private EventService eventService;

    [SerializeField] private GasKnobHandler gasKnobHandler;


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
