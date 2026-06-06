using System.Security;
using UnityEngine;

public struct QuestIncrementEvent
{
    public string ObjectiveID { get; private set; }
    public int Qty { get; private set; }
    public QuestObjectiveType Type { get; private set; }

    public QuestIncrementEvent(string id, int qty, QuestObjectiveType type) { 
        ObjectiveID = id;
        Qty = qty;
        Type = type;
    }

}
