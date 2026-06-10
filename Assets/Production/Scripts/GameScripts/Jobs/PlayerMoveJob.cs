using Codice.CM.Common;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct PlayerMoveJob : IJobParallelFor
{
    [ReadOnly]
    public NativeList<MovementData> playersMoving;
    [WriteOnly]
    public NativeQueue<int>.ParallelWriter playersInRange;
    [ReadOnly]
    public Vector2 humanPosition;
    [ReadOnly]
    public float deltaTime;

    public void Execute(int index)
    {
        var data = playersMoving[index];

        var targetPos = data.targetLocation;
        var currentPos = data.currentLocation;

        bool newRender = false;

        
        Vector2 distanceFromHuman = humanPosition - currentPos;

        if (distanceFromHuman.magnitude <= 50f) {
            playersInRange.Enqueue(data.entityID);
            newRender = true;
        }

        if (!playersMoving[index].hasTarget)
        {
            playersMoving[index] = new MovementData(data.entityID, data.speed, data.targetLocation, data.targetLocation, false, newRender);
            return;
        }

        Vector2 distanceToTarget = targetPos - currentPos;

        var distance = distanceToTarget.magnitude; 

        if (distance <= 0.001f)
        {
            playersMoving[index] = new MovementData(data.entityID,data.speed,data.targetLocation,data.targetLocation,false,newRender);
            return;
        }

        Vector2 direction = distanceToTarget / distance;

        float moveAmount = data.speed * deltaTime;

        if (moveAmount >= distance)
        {
            playersMoving[index] = new MovementData(data.entityID, data.speed, data.targetLocation, data.targetLocation, false, newRender);
            return;
        }
        else
        {
            playersMoving[index] = new MovementData(data.entityID,data.speed,data.currentLocation += direction * moveAmount,data.targetLocation,true, newRender);
            return;
        }
    }
}
