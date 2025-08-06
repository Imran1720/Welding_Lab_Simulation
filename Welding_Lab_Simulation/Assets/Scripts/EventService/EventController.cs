using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    public Action currentEvent;

    public void InvokeEvent() => currentEvent?.Invoke();
    public void AddEventListener(Action listener) => currentEvent += listener;
    public void RemoveEventListener(Action listener) => currentEvent -= listener;
}

public class EventController<T>
{
    public Action<T> currentEvent;
    public void InvokeEvent(T type) => currentEvent?.Invoke(type);
    public void AddEventListener(Action<T> listener) => currentEvent += listener;
    public void RemoveEventListener(Action<T> listener) => currentEvent -= listener;
}
