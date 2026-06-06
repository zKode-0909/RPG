using System.Collections.Generic;
using UnityEngine;

public class QuestGiverFactory
{
    QuestGiverDB database;
    QuestFactory questFactory;

    public QuestGiverFactory(QuestGiverDB database,QuestFactory questFactory)
    {
        this.database = database;
        this.questFactory = questFactory;
    }

    public bool TryCreateQuestGiver(int id,out QuestGiver questGiver) {
        if (database.TryGetQuestGiverDef(id,out var giver)) { 
            var quests = giver.Quests;
            Dictionary<int, Quest> questDict = new Dictionary<int, Quest>();
            foreach (var quest in quests) {
                if (questFactory.TryCreateQuest(quest.QuestID, out var validQuest)) {
                    questDict.TryAdd(quest.QuestID, validQuest);
                }
                
            }
            
            var newGiver = new QuestGiver(giver.GiverID,giver.GiverName,questDict);
            questGiver = newGiver;
            return true;
            
        }
        questGiver = null;
        return false;
    }
}
