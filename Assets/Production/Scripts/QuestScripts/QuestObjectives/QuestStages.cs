using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestStages
{
    Dictionary<string,QuestStageDetails> allStages = new Dictionary<string,QuestStageDetails>();
    QuestStageDetails currentStage;
    public event Action StagesComplete;
    public event Action<QuestStageDetails,QuestStageDetails> SetStage;



    public QuestStageDetails CurrentStage => currentStage;

    public QuestStages(Dictionary<string, QuestStageDetails> stages,QuestStageDetails initialStage) { 
        this.allStages = stages;
        this.currentStage = initialStage;


        
    }

    void TransitionStage(string nextStage) {
        Debug.Log($"New stage is {nextStage}");
        if (nextStage == "Complete") {
            SetStage?.Invoke(currentStage,null);
           // currentStage.StageCompleteAction -= quest.HandleQuestAction;
            StagesComplete?.Invoke();
            currentStage = null;
            return;
        }
        currentStage.CompleteStage();
        //currentStage.StageCompleteAction -= quest.HandleQuestAction;
        
        var newStage = allStages[nextStage];
        SetStage?.Invoke(currentStage, newStage);

        currentStage = newStage;
        //currentStage.StageCompleteAction += quest.HandleQuestAction;


    }

    

    public bool TryIncrement(string id,int qty) {
        bool incremented = false;
        if (currentStage.TryIncrementObjective(id,qty)) {
            incremented = true;
            if (currentStage.CheckStageCompletion(out var nextStage)) {
               
                TransitionStage(nextStage);
            }
        }

        return incremented;
    } 

    
    /*
    bool stagesComplete = false;
    QuestStageDetails currentStage;
    List<QuestStageDetails> allStages;
    public List<string> completedStages;    
    IReadOnlyList<QuestAction> allObjectivesFinishedActions;

    public event Action ObjectivesFinishedEvent;

    public event Action<IReadOnlyList<QuestAction>> QuestObjectiveEvent;

    Dictionary<string, int> progressByTargetId;

    //Inventory currentPlayerInventory;

    Quest currentQuest;

    public QuestStages(QuestStageDetails initialStage,IReadOnlyList<QuestAction> objectivesFinishedActions,List<QuestStageDetails> allStages) { 
        currentStage = initialStage;
        this.allStages = allStages;
        completedStages = new List<string>();
  
        allObjectivesFinishedActions = objectivesFinishedActions;
        currentStage.StageStartedEvent += SendActions;
        currentStage.RequirementCompleteEvent += SendActions;
        currentStage.StageCompleteEvent += SendActions;
        currentStage.RequirementIncrementedEvent += SendActions;
    }

    public void SetCompletedStages(List<string> completed) {
        this.completedStages = completed;
    }*/
    /*
    public void SetInventory(Inventory inventory) {
        this.currentPlayerInventory = inventory;
    }
    */
    /*
    public void SetQuest(Quest quest) { 
        this.currentQuest = quest;
    }

    public void SetStage(string id,List<QuestStageRequirementContext> stageCtx,List<string> completedIds) {
        foreach (var stage in allStages) {

            if (completedIds.Contains(stage.GetStageID())) {
                foreach (var requirement in stage.GetStageRequirements()) {
                    progressByTargetId.Add(requirement.Key, requirement.Value.GetMaxProgressCount());
                }
            }

            if (stage.GetStageID() == id) { 
                currentStage = stage;
              
            }
        }

        foreach (var ctx in stageCtx) {
          
            
            foreach (var requirement in currentStage.GetStageRequirements())
            {
                if (requirement.Value.GetQuestObjectiveStableID() == ctx.ObjectiveID) {
                    requirement.Value.SetCurrentProgress(ctx.Progress);
                    progressByTargetId.Add(requirement.Key, requirement.Value.GetCurrentProgress());
                }
            }
        }

        TryTransition();
        
    }

    public QuestStageDetails GetCurrentStage() { 
        return currentStage;
    }

    public void SetStageProgress(List<QuestStageRequirementContext> stageCtx) { 
        
    }


    void SendActions(IReadOnlyList<QuestAction> actions) {
        QuestObjectiveEvent?.Invoke(actions);
    }

    bool TryTransition() {
        if (currentStage.CheckStageCompletion(progressByTargetId)) {
            if (currentStage.TryTransition(progressByTargetId,out var nextStage))
            {
                completedStages.Add(currentStage.GetStageID());
                currentStage = nextStage;
                nextStage.EnterStage();*/
                /*
                foreach (var item in currentPlayerInventory.GetItems()) {
                    foreach (var requirement in nextStage.GetStageRequirements()) {
                        
                        if (item != null && requirement.Value.GetQuestObjectiveStableID() == item.StableID) {
                            currentQuest.OnObjectiveEvent(item.StableID);
                        }
                    }
                }*/
                /*
                return true;
            }
            else {
                
                stagesComplete = true;
                Debug.Log($"QUEST OBJECTIVE COMPLETE!");
                ObjectivesFinishedEvent?.Invoke();
                SendActions(allObjectivesFinishedActions);
                return false;
            }
        }
        return false;
    }

    public void RequestIncrementObjective(string id, int count) {
        if (stagesComplete == false) {
            currentStage.RequestIncrementProgress(id, count,progressByTargetId);
            TryTransition();
        }
        
    }

    public void SetTargetIDDict(Dictionary<string, int> dict) { 
        progressByTargetId = dict;
    }

    public bool GetQuestCompletionStatus() => stagesComplete;*/

}
