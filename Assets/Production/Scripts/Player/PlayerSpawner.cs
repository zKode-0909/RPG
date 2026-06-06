using UnityEngine;

public class PlayerSpawner
{
    PlayerLocationManager playerLocationManager;
    PlayerMB playerMBTemplate;
    public PlayerSpawner(PlayerLocationManager playerLocationManager)
    {
        this.playerLocationManager = playerLocationManager;
    }

    public bool TrySpawnPlayerMB(Player player,Vector3 spawnPosition,HumanPlayerMB humanPlayer,out PlayerMB playerMB) {
        playerMB = Object.Instantiate(playerMBTemplate);
        
        if (playerMB != null)
        {
            playerMB.gameObject.SetActive(false);
            playerMB.Initialize(player, humanPlayer);

            return true;
        }

        return false;
    }

    public void SpawnPlayerIntoWorld(Player playerToSpawn,Vector2 spawnPosition,Vector2 targetPosition,bool hasTarget) {
        playerLocationManager.TryRegisterMovingPlayer(new MovementData(playerToSpawn.EntityID, playerToSpawn.Speed, spawnPosition, targetPosition, hasTarget,false));
    }
}
