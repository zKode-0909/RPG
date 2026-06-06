using UnityEngine;

public class PlayerFactory
{
    PlayerRegistry playerRegistry;
    PlayerSpawner spawner;
    QuestLogFactory questLogFactory;
    PlayerMB playerMBTemplate;

    public PlayerFactory(PlayerRegistry playerRegistry,QuestLogFactory questLogFactory,PlayerMB template,PlayerSpawner spawner) {
        this.playerRegistry = playerRegistry;
        this.questLogFactory = questLogFactory;
        this.playerMBTemplate = template;
        this.spawner = spawner;
    }

    public bool TryCreatePlayer(int id,string name,bool human,Vector2 spawnPosition,out Player player) {
        if (questLogFactory.TryCreateQuestLog(id, out var log)) {
            player = new Player(id,name,human);
            if (playerRegistry.Register(player, id)) {
                spawner.SpawnPlayerIntoWorld(player, spawnPosition, new Vector2(25, 25), false);
                return true;
            }
        }
        Debug.Log("Failed creating player");
        player = null;
        return false;
    }

}
