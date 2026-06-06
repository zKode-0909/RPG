using UnityEngine;

public class SimulationBootStrapper
{
    PlayerFactory playerFactory;
    PlayerRegistry playerRegistry;

    public void BootStrap(PlayerFactory playerFactory,PlayerRegistry playerRegistry) { 
        this.playerFactory = playerFactory;
        this.playerRegistry = playerRegistry;
    }


}
