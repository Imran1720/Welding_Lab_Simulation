using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public EventController gasKnobToggled;
    public EventController<bool> InteractingWIthKnob;

    public EventService()
    {
        gasKnobToggled = new EventController();
        InteractingWIthKnob = new EventController<bool>();
    }
}
