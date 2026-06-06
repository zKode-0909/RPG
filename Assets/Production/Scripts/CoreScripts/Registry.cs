using System.Collections.Generic;
using UnityEngine;

public abstract class Registry<TKey,TValue>
{
    protected readonly Dictionary<TKey, TValue> registered = new();

    public IEnumerable<KeyValuePair<TKey, TValue>> Registered => registered;

    public bool TryGet(TKey id, out TValue value) => registered.TryGetValue(id, out value);


    public bool Register(TValue item, TKey ownerId)
    {
        if (registered.TryGetValue(ownerId, out var value)) return false;
        return registered.TryAdd(ownerId, item);

    }

    public bool Remove(TKey id) => registered.Remove(id);
}
