using UnityEngine;

public class QuestLogFactory
{

    QuestLogRegistry registry;


    public QuestLogFactory(QuestLogRegistry registry) {
        this.registry = registry;
    }

    public bool TryCreateQuestLog(int owner, out QuestLog log,int capacity = 25) {
        log = new QuestLog(capacity);

        if (registry.Register(log, owner))
        {
            return true;
        }
        else { 
            log = null;
            return false;
        }



    }
}
