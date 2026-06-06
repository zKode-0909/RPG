using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerLocationManager
{
    NativeQueue<int> playersToRender;
    NativeList<MovementData> playersMoving;
    Dictionary<int, int> playerMovingIdxToPlayerID;
    public Dictionary<int, int> PlayerMovingIdxToPlayerID => playerMovingIdxToPlayerID;
    public NativeList<MovementData> PlayersMoving => playersMoving;
    public NativeQueue<int> PlayersToRender => playersToRender;

    int currentMovingIdx = 0;

    public void Initialize()
    {
        playersMoving = new NativeList<MovementData>(Allocator.Persistent);
        playersToRender = new NativeQueue<int>(Allocator.Persistent);
        playerMovingIdxToPlayerID = new Dictionary<int, int>();
    }

    public void Dispose()
    {
        playersMoving.Dispose();
        playersToRender.Dispose();

    }

    public bool TryRegisterMovingPlayer(MovementData data)
    {
        if (!playersMoving.IsCreated)
        {
            Debug.Log($"Playersmoving not created");
            return false;
        }

        if (currentMovingIdx > playersMoving.Length)
        {
            Debug.Log($"invalid index");
            return false;
        }
        if (playerMovingIdxToPlayerID.TryAdd(data.entityID, currentMovingIdx)) {
            playersMoving.Add(data);
            currentMovingIdx += 1;

            return true;
        }

        Debug.Log("Failed");
        return false;
        
    }

    public Vector2 GetPosition(int id) { 
        var idx = playerMovingIdxToPlayerID[id];
        return playersMoving[idx].currentLocation;
    }


    public bool TryUpdatePlayerMovementData(int playerID, MovementData newData)
    {
        
        if (playerMovingIdxToPlayerID.TryGetValue(playerID,out var idxToUpdate)) {
            if (idxToUpdate < playersMoving.Length) {
                playersMoving[idxToUpdate] = newData;
                return true;
            }
            return false;
        }
        return false;
        
    }
}
