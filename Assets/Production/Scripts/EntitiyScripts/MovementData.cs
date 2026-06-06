using UnityEngine;

public struct MovementData
{
    public int entityID;
    public float speed;
    public Vector2 currentLocation;
    public Vector2 targetLocation;
    public bool hasTarget;
    public bool rendered;

    public MovementData(int id,float speed,Vector2 currentLoc,Vector2 targetLoc,bool hasTarget,bool rendered) { 
        this.entityID = id;
        this.speed = speed;
        this.currentLocation = currentLoc;
        this.targetLocation = targetLoc;
        this.hasTarget = hasTarget;
        this.rendered = rendered;
    }

}
