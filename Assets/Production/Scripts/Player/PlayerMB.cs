using UnityEngine;

public class PlayerMB : MonoBehaviour
{
    Player player;
    PlayerInputReader input;
    HumanPlayerMB humanPlayer;
    

    public void Initialize(Player player,HumanPlayerMB humanPlayer,PlayerInputReader input = null)
    {
        this.player = player;
        this.input = input;
        this.humanPlayer = humanPlayer;
       
    }

    private void Update()
    {/*
        if (CheckDistanceFromHuman(player.CurrentPosition, new Vector2(humanPlayer.transform.position.x, humanPlayer.transform.position.z)) >= 55f) { 
            //player.UnMount();
            Destroy(gameObject);
        }*/
    }

    float CheckDistanceFromHuman(Vector2 SimPosition, Vector2 HumanPosition)
    {
        Vector2 toTarget = HumanPosition - SimPosition;

        return toTarget.magnitude;




    }
}
