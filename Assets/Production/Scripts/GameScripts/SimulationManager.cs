using Codice.CM.Common;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class SimulationManager : MonoBehaviour 
{
    PlayerFactory playerFactory;
    PlayerRegistry playerRegistry;
    PlayerSpawner playerSpawner;
    PlayerLocationManager playerLocationManager;
    HumanPlayerMB playerHuman;
    bool jobScheduled;
    JobHandle playerMovementJobHandle;
    public void Initialize(PlayerFactory playerFactory, PlayerRegistry playerRegistry,PlayerSpawner playerSpawner,HumanPlayerMB humanPlayer,PlayerLocationManager playerLocationManager) {
        this.playerFactory = playerFactory;
        this.playerRegistry = playerRegistry;

        this.playerSpawner = playerSpawner;
        this.playerHuman = humanPlayer;

        this.playerLocationManager = playerLocationManager;

        playerFactory.TryCreatePlayer(1, "Testy",false,new Vector2(0,0), out var player1);
        playerFactory.TryCreatePlayer(2, "Testy2",false,new Vector2(25,0), out var player2);

        
        
        
            
        
    }

    private void Update()
    {
        if (jobScheduled)
        {
            playerMovementJobHandle.Complete();
        }
 

        playerMovementJobHandle = new PlayerMoveJob()
        {
            playersMoving = playerLocationManager.PlayersMoving,
            playersInRange = playerLocationManager.PlayersToRender.AsParallelWriter(),
            humanPosition = new Vector2(playerHuman.transform.position.x, playerHuman.transform.position.z),
            deltaTime = Time.deltaTime,


        }.Schedule(playerLocationManager.PlayersMoving.Length, 32);

        jobScheduled = true;
    }


    private void LateUpdate()
    {
        playerMovementJobHandle.Complete();
        jobScheduled = false;
        Debug.Log($"Reading players in moving list");
        foreach (var player in playerLocationManager.PlayersMoving) {
            Debug.Log($"{player.entityID} is at location: {player.currentLocation.x},{player.currentLocation.y}");
        }
        
    }

    float CheckDistanceFromHuman(Vector2 SimPosition,Vector2 HumanPosition) {
        Vector2 toTarget = HumanPosition - SimPosition;

        return toTarget.magnitude;

        

       
    }


}
