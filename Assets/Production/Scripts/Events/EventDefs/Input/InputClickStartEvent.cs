using UnityEngine;

public struct InputClickStartEvent : IEvent
{
    public Vector2 clickLocation;

    public InputClickStartEvent(Vector2 location) {
        this.clickLocation = location;
    }
}
