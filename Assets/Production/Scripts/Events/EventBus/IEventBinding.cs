using System;
using UnityEngine;

//This interface says: “an event binding for T exposes two delegate properties.”
public interface IEventBinding<T>
{
    public Action<T> OnEvent { get; }
    public Action OnEventNoArgs { get; }
}

public class EventBinding<T> : IEventBinding<T> where T : IEvent {
    // Those defaults are “do nothing” handlers, so whoever raises the event can safely invoke them without null checks.
    Action<T> onEvent = _ => { };
    Action onEventNoArgs = () => { };

    Action<T> IEventBinding<T>.OnEvent {
        get => onEvent;
      
    }

    Action IEventBinding<T>.OnEventNoArgs { 
        get => onEventNoArgs;
      
    }

    public EventBinding(Action<T> onEvent) => this.onEvent = onEvent ?? (_ => { });
    public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs ?? (() => { });

    public void Add(Action handler) => onEventNoArgs += handler;
    public void Remove(Action handler) => onEventNoArgs -= handler;

    public void Add(Action<T> handler) => onEvent += handler;
    public void Remove(Action<T> handler) => onEvent -= handler;

}
