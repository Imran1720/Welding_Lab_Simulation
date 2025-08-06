using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public EventController<bool> GasOnTrigger;

    public EventService()
    {
        GasOnTrigger = new EventController<bool>();
    }
}
