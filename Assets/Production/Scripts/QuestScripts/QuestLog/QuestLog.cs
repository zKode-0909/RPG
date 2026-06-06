using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestLog
{
    private Dictionary<int, Quest> quests;
    public Dictionary<int, Quest> Quests => quests;
    public HashSet<int> completedQuests;
    public int capacity;



    public QuestLog(int capacity)
    {
        this.capacity = capacity;
        quests = new Dictionary<int, Quest>();
        completedQuests = new HashSet<int>();
    }

    public void SetCurrentAndCompletedQuests(List<Quest> questsToAdd, List<int> completed)
    {
        foreach (Quest quest in questsToAdd)
        {
            quests.Add(quest.questID,quest);
        }

        foreach (int completedQuest in completed)
        {
            completedQuests.Add(completedQuest);
        }
    }


    /*
    public Quest GetQuestByObjective() { 
        return 
    }*/

    public Dictionary<int, Quest> GetQuests()
    {

        return quests;
    }

    public List<int> GetCompletedQuests()
    {
        return completedQuests.ToList();
    }



    public bool TryAddQuest(Quest quest)
    {
        if (quests.Count >= capacity - 1)
        {
            Debug.Log("log full");
            return false;
        }



        if (quest == null)
        {
            return false;

        }


        var added = quests.TryAdd(quest.questID, quest);
        if (!added)
        {
            return false; // already exists
        }


        Debug.Log($"successfully added {quest.questName} to log");

        return true;
    }

    /*
    public void TryIncrementQuestObjective(string objectiveID)
    {
        foreach (KeyValuePair<string, Quest> quest in quests)
        {
            quest.Value.OnObjectiveEvent(objectiveID);
        }
    }
    */




    public void RemoveQuest(Quest quest) { quests.Remove(quest.questID); }
    public void RemoveQuest(int questID) { quests.Remove(questID); }
}
