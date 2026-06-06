using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questName { get; private set; }
    public int questID { get; private set; }

    QuestStages stages;

    bool complete;

    QuestReward reward;

    public QuestStages Stages => stages;
    public bool Complete => complete;

    public event Action<string, int> OnIncrementUI;
    public event Action<IReadOnlyList<QuestAction>> OnTurnIn;
    public event Action OnStagesComplete;
    public event Action<IReadOnlyList<QuestAction>> OnQuestAction;


    List<QuestAction> turninActions;
    List<QuestAction> stagesCompleteActions;
    

    public Quest(string name,int id,QuestStages stages,QuestReward reward){
        this.questName = name;
        this.reward = reward;
        questID = id;
        this.stages = stages;
        complete = false;
        stages.StagesComplete += HandleStagesComplete;
        stages.SetStage += HandleStageChange;
    }

    public void HandleIncrement(QuestIncrementEvent evt) {
        Debug.Log($"Attempting to incremnt objective {evt.ObjectiveID}");
        if (stages.TryIncrement(evt.ObjectiveID, evt.Qty)) { 
            OnIncrementUI?.Invoke(evt.ObjectiveID, evt.Qty);
        }
    }

    void HandleStageChange(QuestStageDetails prevStage,QuestStageDetails newStage) {
        if (prevStage != null) {
            prevStage.StageCompleteAction -= HandleQuestAction;
        }

        if (newStage != null) {
            newStage.StageCompleteAction += HandleQuestAction;
        }
        
        
    }

    void HandleStagesComplete() {
        Debug.Log($"Quest {questName}: Complete!");
        complete = true;
        //OnStagesComplete?.Invoke();
        HandleQuestAction(stagesCompleteActions);
    }

    public void HandleQuestAction(IReadOnlyList<QuestAction> actions) {
        QuestActionHandler.HandleActions(actions);
    }

    public bool TryCompleteQuest(out QuestReward reward) {
        if (complete)
        {
            HandleQuestAction(turninActions);
            reward = this.reward;
            return true;
        }
        else {
            reward = null;
            return false;
        }

    }

    

   
    /*
    public string entityOwnerStableID { get; private set; }
    
    public string questGiverID { get; private set; }
    
    public int questRuntimeID { get; private set; }
    public int questLevel { get; private set; }
    public event Action<string, string> allObjectiveCompleteEvent;
    QuestStages questStages;
    private Action<IReadOnlyList<QuestAction>> actionSink;

    Dictionary<string, int> progressByTargetId;

    //Inventory currentPlayerInventory;

    public Quest(string name, string questGiverID, string questID, int runtimeID, int questLevel, QuestStages stages, string entityID*//*, Inventory inventory*//*)
    {
        this.questName = name;
        this.questGiverID = questGiverID;
        this.questID = questID;
        this.questRuntimeID = runtimeID;
        this.questLevel = questLevel;
        this.entityOwnerStableID = entityID;
        this.questStages = stages;

        //currentPlayerInventory = inventory;

       // questStages.SetInventory(inventory);

        progressByTargetId = new Dictionary<string, int>();

        questStages.QuestObjectiveEvent += HandleAction;
        questStages.ObjectivesFinishedEvent += HandleObjectivesComplete;
        questStages.SetTargetIDDict(progressByTargetId);
        questStages.SetQuest(this);






    }*/
    /*
    public void InitializeFresh()
    {
        foreach (var item in currentPlayerInventory.GetItems())
        {
            foreach (var requirement in questStages.GetCurrentStage().GetStageRequirements())
            {
                if (item != null)
                {
                    Debug.Log($"checking item: {item.StableID} vs objective {requirement.Value.GetQuestObjectiveStableID()}");
                    if (requirement.Value.GetQuestObjectiveStableID() == item.StableID)
                    {
                        OnObjectiveEvent(item.StableID);
                    }
                }


            }
        }
    }*/
    /*
    public void InitializeFromData(string stageID, List<QuestStageRequirementContext> reqs, List<string> completedStageIDs)
    {
        questStages.SetStage(stageID, reqs, completedStageIDs);
    }



    public void BindActions(QuestActionRunner runner)
    {
        actionSink += runner.HandleActions;
    }

    private void EmitActions(IReadOnlyList<QuestAction> actions)
    {

        foreach (QuestAction action in actions)
        {
            Debug.Log($"emitting action {action}");
            action.Execute();
        }
        // actionSink?.Invoke(actions);
    }

    void HandleObjectivesComplete()
    {
        allObjectiveCompleteEvent?.Invoke(questID, entityOwnerStableID);
    }



    public void HandleAction(IReadOnlyList<QuestAction> actions)
    {
        EmitActions(actions);
        //allObjectiveCompleteEvent?.Invoke(entityOwnerRuntimeID, questID);
    }

    public bool GetQuestCompletionStatus()
    {
        return questStages.GetQuestCompletionStatus();
    }
    

    public void OnObjectiveEvent(string objectiveThingId)
    {
        if (!progressByTargetId.TryGetValue(objectiveThingId, out var progress))
        {
            progressByTargetId.Add(objectiveThingId, 0);
        }
        questStages.RequestIncrementObjective(objectiveThingId, 1);
    }

    public QuestStages GetQuestStages()
    {
        return questStages;
    }*/
}
