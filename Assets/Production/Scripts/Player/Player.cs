using Codice.CM.Common;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : IQuester
{
    int entityID;
    string playerName;

    bool active;
    public bool Active => active;

    PlayerMB playerMB;

    bool human;
    public bool Human => human;

    bool hasTarget = false;

    float speed = 5f;
    public float Speed => speed;

    public int EntityID => entityID;
    public string PlayerName => playerName;

    public event Action<string> KilledEnemyEvent;
    public event Action<QuestIncrementEvent> QuestIncrementEvent;

    

    public Player(int entityID,string playerName,bool human) { 
        this.entityID = entityID;
        this.playerName = playerName;
        this.human = human;
    }

    public void Kill(string killedID) {
        KilledEnemyEvent?.Invoke(killedID);
        QuestIncrementEvent?.Invoke(new QuestIncrementEvent(killedID, 1, QuestObjectiveType.Kill));
    }



   

  

  





}
