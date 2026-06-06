using UnityEngine;

public class QuestHandInEvent : QuestAction
{
    Quest quest;
    public QuestHandInEvent(Quest quest) { 
        this.quest = quest;
    }
    public override void Execute()
    {
        Debug.Log($"Turning in quest {quest.questName}");
    }
}
