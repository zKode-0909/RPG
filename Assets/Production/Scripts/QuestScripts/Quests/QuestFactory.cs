using UnityEngine;

public class QuestFactory
{
    QuestDB database;

    

    public QuestFactory(QuestDB database) { 
        this.database = database;
    }

    public bool TryCreateQuest(int ID,out Quest quest) {
        if (database.TryGetQuestDef(ID, out var settings))
        {
            quest = settings.BuildRuntimeQuest();//quest = new Quest(settings.QuestName, settings.QuestID);
            return true;
        }
        else { 
            quest = null;
            return false;
        }
    }
}
