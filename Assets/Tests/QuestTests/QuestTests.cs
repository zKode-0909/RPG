

using NUnit.Framework;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// tests to make
/*  
 * Quest Complete
 * Quest stage complete
 * transition stage
 * increment objective
 * quest accepted
 */

public class QuestTests
{
    [Test]
    public void TransitionQuestTest() {

        var questReward = new QuestReward(500,50);

        var stageDict = new Dictionary<string, QuestStageDetails>();

        var stage1NextStages = new List<QuestStageDetails>();

        var stage1Reqs = new Dictionary<string, ObjectiveStageRequirement>();
        stage1Reqs.Add("testObjective1",new ObjectiveStageRequirement(3,0,"testObjective1"));

        var stage1Actions = new List<QuestAction>();
        stage1Actions.Add(new SayQuestAction("Nigga yo"));

        var stage1PreReqs = new List<string>();
        var stage1 = new QuestStageDetails("stage1",stage1NextStages,stage1Reqs,stage1Actions,stage1PreReqs);


        var stage2NextStages = new List<QuestStageDetails>();

        var stage2Reqs = new Dictionary<string, ObjectiveStageRequirement>();
        stage2Reqs.Add("testObjective2",new ObjectiveStageRequirement(2,0,"testObjective2"));
        stage2Reqs.Add("testObjective3", new ObjectiveStageRequirement(5, 0, "testObjective3"));

        var stage2Actions = new List<QuestAction>();
        stage2Actions.Add(new SayQuestAction("Completed stage 2 fam"));

        var stage2PreReqs = new List<string>();
        stage2PreReqs.Add("testObjective1");

        var stage2 = new QuestStageDetails("stage2", stage2NextStages, stage2Reqs, stage2Actions, stage2PreReqs);

        stage1NextStages.Add(stage2);

        stageDict.Add("stage1", stage1);
        stageDict.Add("stage2",stage2);

        var stages = new QuestStages(stageDict,stage1);

        var testQuest = new Quest("Test Quest", 1, stages,questReward);

        testQuest.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        testQuest.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        testQuest.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));


        testQuest.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));


        Assert.AreEqual(testQuest.Stages.CurrentStage.StageID, stage2.StageID);


    }

    [Test]
    public void TestQuestDB() {
        var questDB = AssetDatabase.LoadAssetAtPath<QuestDB>(
            "Assets/Production/Scripts/QuestScripts/Quests/Data/QuestDB.asset"
        );

        

        Assert.IsNotNull(questDB);

        questDB.BuildQuestDB();

        Assert.IsTrue(questDB.TryGetQuestDef(1, out var quest));
        Assert.IsNotNull(quest);
        Assert.AreEqual(1, quest.QuestID);

    }

    [Test]
    public void TestQuestFactory() {

        var questDB = AssetDatabase.LoadAssetAtPath<QuestDB>(
            "Assets/Production/Scripts/QuestScripts/Quests/Data/QuestDB.asset"
        );

        questDB.BuildQuestDB();

        var questFactory = new QuestFactory(questDB);

        var quest = questFactory.TryCreateQuest(1, out var q);

        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));

        q.HandleIncrement(new QuestIncrementEvent("testObjective2", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective2", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective2", 1, QuestObjectiveType.Kill));

        Assert.AreEqual(q.Stages.CurrentStage.StageID, "<TestQuest>.<Stage2>");

        q.HandleIncrement(new QuestIncrementEvent("testObjective3", 4, QuestObjectiveType.Kill));

        Assert.AreEqual(q.Complete, true);
    }

    [Test]
    public void InteractWithQuestGiver() {
        var questDB = AssetDatabase.LoadAssetAtPath<QuestDB>(
            "Assets/Production/Scripts/QuestScripts/Quests/Data/QuestDB.asset"
        );

        questDB.BuildQuestDB();

        var questFactory = new QuestFactory(questDB);

        Quest quest = null;

        if (questFactory.TryCreateQuest(1, out var q)) {
            quest = q;
        }

        Assert.IsNotNull(quest);

        var questList = new Dictionary<int,Quest>();

        questList.Add(quest.questID,quest);


        var giver = new QuestGiver(1, "Test Giver", questList);

        Assert.IsNotNull(giver);

        giver.OnInteract();
    }

    [Test]
    public void AcceptQuestTest() {
        var questDB = AssetDatabase.LoadAssetAtPath<QuestDB>(
            "Assets/Production/Scripts/QuestScripts/Quests/Data/QuestDB.asset"
        );

        questDB.BuildQuestDB();

        var questFactory = new QuestFactory(questDB);
        var questLogRegistry = new QuestLogRegistry();
        var questLogFactory = new QuestLogFactory(questLogRegistry);

        Quest quest = null;

        if (questFactory.TryCreateQuest(1, out var q))
        {
            quest = q;
        }

        Assert.IsNotNull(quest);

        QuestLog questLog = null;

        if (questLogFactory.TryCreateQuestLog(3, out var log)) {
            questLog = log;
        }

        Assert.IsNotNull(questLog);

        questLog.TryAddQuest(quest);

        Assert.IsTrue(questLog.Quests.ContainsKey(quest.questID));
    }

    [Test]
    public void QuestTurnIn() {
        var questDB = AssetDatabase.LoadAssetAtPath<QuestDB>(
            "Assets/Production/Scripts/QuestScripts/Quests/Data/QuestDB.asset"
        );

        questDB.BuildQuestDB();

        var questFactory = new QuestFactory(questDB);

        var quest = questFactory.TryCreateQuest(1, out var q);

        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective1", 1, QuestObjectiveType.Kill));

        q.HandleIncrement(new QuestIncrementEvent("testObjective2", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective2", 1, QuestObjectiveType.Kill));
        q.HandleIncrement(new QuestIncrementEvent("testObjective2", 1, QuestObjectiveType.Kill));

        Assert.AreEqual(q.Stages.CurrentStage.StageID, "<TestQuest>.<Stage2>");

        q.HandleIncrement(new QuestIncrementEvent("testObjective3", 4, QuestObjectiveType.Kill));

        Assert.AreEqual(q.Complete, true);

        QuestReward questReward = null;

        if (q.TryCompleteQuest(out var reward)) {
            questReward = reward;
        }

        Assert.IsNotNull(questReward);
    }
}
