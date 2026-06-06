
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : IInteractable
{
    int questGiverID;
    string questGiverName;

    public int QuestGiverID => questGiverID;
    public string QuestGiverName => questGiverName;

    Dictionary<int,Quest> quests = new Dictionary<int, Quest>();
    public event Action<IQuester, int> QuestAcceptedEvent;

    public QuestGiver(int id,string name,Dictionary<int,Quest> quests) { 
        this.questGiverID = id;
        questGiverName = name;
        this.quests = quests;
    }


    public void OnQuestAccepted(IQuester accepter,int quest) {
        if (quests.TryGetValue(quest, out var acceptedQuest))
        {
            QuestAcceptedEvent?.Invoke(accepter, quest);
        }
        else {
            Debug.Log("Invalid Quest");
        }
    }

    public void OnInteract() {
        Debug.Log("I have been interacted with!");
    }


}
