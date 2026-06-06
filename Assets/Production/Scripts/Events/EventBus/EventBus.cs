using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EventBus<T> where T : IEvent 
{
    static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

    public static void Register(IEventBinding<T> binding) => bindings.Add(binding);
    public static void Deregister(IEventBinding<T> binding) => bindings.Remove(binding);

    public static void Raise(T evt) {

        foreach (var binding in bindings.ToArray())
        {
            binding.OnEvent?.Invoke(evt);
            binding.OnEventNoArgs?.Invoke();
        }
    }

    static void Clear() {
       // Debug.Log($"Clearing {typeof(T).Name} bindings");
        bindings.Clear();
    }
}
