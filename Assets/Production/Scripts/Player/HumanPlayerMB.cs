using UnityEngine;

public class HumanPlayerMB : MonoBehaviour
{
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = new Player(0,"Humy",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
