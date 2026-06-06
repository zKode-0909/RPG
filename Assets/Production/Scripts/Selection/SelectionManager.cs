using UnityEngine;

public class SelectionManager
{
    EventBinding<InputClickStartEvent> ClickStartBinding;
    EventBinding<InputClickEndEvent> ClickEndBinding;

    float startTime = 0;
    float endTime = 0;

    Vector2 mouseLocation = Vector2.zero;

    public void Initialize() {
        ClickStartBinding = new EventBinding<InputClickStartEvent>(HandleClickStart);
        EventBus<InputClickStartEvent>.Register(ClickStartBinding);

        ClickEndBinding = new EventBinding<InputClickEndEvent>(HandleClickEnd);
        EventBus<InputClickEndEvent>.Register(ClickEndBinding);
    }

    public void Dispose() {
        EventBus<InputClickStartEvent>.Deregister(ClickStartBinding);
        EventBus<InputClickEndEvent>.Deregister(ClickEndBinding);
    }


    void HandleClickStart(InputClickStartEvent evt) {
        startTime = Time.time;

        mouseLocation = evt.clickLocation;
    }

    void HandleClickEnd(InputClickEndEvent evt) {
        endTime = Time.time;

        var endLocation = evt.clickLocation;

        Debug.Log($"{CheckTimePassed()} time has passed");

    }

    float CheckTimePassed() { 
        var passedTime =  endTime - startTime;
        startTime = 0;
        endTime = 0;

        return passedTime;  
        
    }
}
