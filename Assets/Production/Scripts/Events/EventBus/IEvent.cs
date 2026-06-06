using JetBrains.Annotations;
using System;
using UnityEngine;

public interface IEvent
{
    
}
// make structs over classes since structs are allocatged on the stack putting less pressure on the garbae collector
public struct TestEvent : IEvent { }

public struct PlayerEvent : IEvent {
    public int xp;
}
