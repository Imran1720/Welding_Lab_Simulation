using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public EventController gasKnobToggled;
    public EventController<bool> InteractingWithKnob;

    public EventService()
    {
        gasKnobToggled = new EventController();
        InteractingWithKnob = new EventController<bool>();
    }
}
