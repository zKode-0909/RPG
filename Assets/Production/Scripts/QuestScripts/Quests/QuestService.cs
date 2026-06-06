using UnityEngine;

public class QuestService
{
    QuestLogRegistry questLogRegistry;
    QuestFactory questFactory;


    public QuestService(QuestLogRegistry questLogRegistry, QuestFactory questFactory)
    {
        this.questLogRegistry = questLogRegistry;
        this.questFactory = questFactory;
    }

    void HandleQuestAccept(IQuester acceptor,int questID) {
        if (questLogRegistry.TryGet(acceptor.EntityID, out var log)) {
            if (questFactory.TryCreateQuest(questID, out var quest)) {
                acceptor.QuestIncrementEvent += quest.HandleIncrement;
                log.TryAddQuest(quest);
            }
        }
    }



    public void SubscribeToQuestGiver(QuestGiver giver) {
        giver.QuestAcceptedEvent += HandleQuestAccept;
    }
}
