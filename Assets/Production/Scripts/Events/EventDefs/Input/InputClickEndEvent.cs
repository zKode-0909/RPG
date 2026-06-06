using UnityEngine;


public struct InputClickEndEvent : IEvent
{
    public Vector2 clickLocation;

    public InputClickEndEvent(Vector2 location)
    {
        this.clickLocation = location;
    }
}
