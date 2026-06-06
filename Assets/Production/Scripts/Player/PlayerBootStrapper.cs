using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;

public class PlayerBootStrapper
{
    PlayerRegistry playerRegistry;
    PlayerFactory playerFactory;

    

    public void Bootstrap(PlayerFactory playerFactory,PlayerRegistry playerRegistry) {
        this.playerRegistry = playerRegistry;
        this.playerFactory = playerFactory;    

       

        


    }
}
