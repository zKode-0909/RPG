using UnityEngine;

public class QuestBootstrapper
{
    QuestFactory questFactory;
    QuestGiverFactory questGiverFactory;

    QuestLogFactory questLogFactory;
    QuestLogRegistry questLogRegistry;

    QuestGiverDB questGiverDB;
    QuestDB questDB;

    bool bootstrapped;

    public void BootStrap(QuestLogFactory questLogFactory,QuestLogRegistry questLogRegistry,QuestDB questDB,QuestGiverDB questGiverDB) {
        if (bootstrapped) {
            Debug.Log("I am already bootstrapped");
            return;
        }

        bootstrapped = true;

        this.questGiverDB = questGiverDB;
        this.questDB = questDB;

        questDB.BuildQuestDB();
        questGiverDB.BuildQuestGiverDB();

        questFactory = new QuestFactory(questDB);
        questGiverFactory = new QuestGiverFactory(questGiverDB,questFactory);


        this.questLogFactory = questLogFactory;
        this.questLogRegistry = questLogRegistry;

        InitializeQuestGivers();



    }

    void InitializeQuestGivers() {
        foreach (var giver in questGiverDB.QuestGiversByID) {
            questGiverFactory.TryCreateQuestGiver(giver.Key, out var questGiver); 
                
            
        }
    }



}
