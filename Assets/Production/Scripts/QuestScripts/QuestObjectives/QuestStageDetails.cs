using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class QuestStageDetails {



   
    string stageID;
    List<QuestStageDetails> potentialNextStages = new List<QuestStageDetails>();
    Dictionary<string,ObjectiveStageRequirement> stageRequirements = new Dictionary<string, ObjectiveStageRequirement>(); 

    public string StageID => stageID;
    public List<QuestStageDetails> PotentialNextStages => potentialNextStages;

    IReadOnlyList<QuestAction> stageCompleteActions = new List<QuestAction>();
    public event Action<IReadOnlyList<QuestAction>> StageCompleteAction;

    List<string> stagePreReqs = new List<string>();
    HashSet<string> completeObjectives = new HashSet<string>();

    public List<string> PreReqs => stagePreReqs;


   

    public QuestStageDetails(string id,List<QuestStageDetails> potentialNextStages,Dictionary<string,ObjectiveStageRequirement> objectives
        ,IReadOnlyList<QuestAction> stageCompleteActions,List<string> preReqs) {
        this.stageID = id;
        this.potentialNextStages = potentialNextStages;
        this.stageRequirements = objectives;
        this.stageCompleteActions = stageCompleteActions;
        this.stagePreReqs = preReqs;
        
    }

    public bool TryIncrementObjective(string objectiveID,int qty) {
        bool incremented = false;
        if (stageRequirements.TryGetValue(objectiveID,out var objective)) {

            if (objective.TryIncrementProgress(objectiveID, qty)) { 

                incremented = true;
                if (objective.CheckCompletion()) { 
                    completeObjectives.Add(objectiveID);
                }
                
            }
        }
        
        return incremented;
    }

    public bool CheckStageCompletion(out string nextStage) {
        bool complete = true;
        foreach (KeyValuePair<string, ObjectiveStageRequirement> pair in stageRequirements) {
            if (!pair.Value.CheckCompletion()) { 
                complete = false; break;
            }
        }

        if (complete) {
          
            nextStage = GetNextStage();
            return complete;
        }

        nextStage = null;   

        return complete;
    }

    public void CompleteStage() {
        StageCompleteAction?.Invoke(stageCompleteActions);
    }

    public string GetNextStage() {
        string nextStage = null;
        
        if (potentialNextStages.Count == 0) {
            return "Complete";
        }

        bool foundStage = true;    
        foreach (var stage in potentialNextStages) {
                foreach (var preReq in stage.PreReqs) {
                    Debug.Log($"next stages count: {potentialNextStages.Count}   complete objectives count: {completeObjectives.Count}    pre req count: {preReq}");

                    if (!completeObjectives.Contains(preReq)) {
                        foundStage = false;
                        break;
                    }
                }
                if (foundStage == true)
                {
                    nextStage = stage.stageID;
                    return nextStage;
                }
                else { 
                    foundStage = true;
                }
           
        }

        return nextStage;
    }
    /*
    IReadOnlyList<ObjectiveStageRequirement> preRequisites;
    IReadOnlyList<ObjectiveStageRequirement> requirements;
    IReadOnlyList<IReadOnlyList<ObjectiveStageRequirement>> stageCompletionConditions;
    public bool complete = false;
    List<QuestStageDetails> possibleNextStages;


    public event Action<IReadOnlyList<QuestAction>> StageCompleteEvent;
    public event Action<IReadOnlyList<QuestAction>> StageStartedEvent;
    public event Action<IReadOnlyList<QuestAction>> RequirementCompleteEvent;
    public event Action<IReadOnlyList<QuestAction>> RequirementIncrementedEvent;

    IReadOnlyList<QuestAction> incrementActions;
    IReadOnlyList<QuestAction> finishedRequirementActions;
    IReadOnlyList<QuestAction> stageEndActions;
    IReadOnlyList<QuestAction> stageStartActions;

    Dictionary<string,ObjectiveStageRequirement> reqById;

    List<QuestAction> actionSubsetHolder;

    string QuestStageID;

    public string GetStageID() {
        return QuestStageID;
    }

    public QuestStageDetails(IReadOnlyList<ObjectiveStageRequirement> preReqs,IReadOnlyList<ObjectiveStageRequirement> reqs,
        IReadOnlyList<IReadOnlyList<ObjectiveStageRequirement>> conditions,
        IReadOnlyList<QuestAction> incrementActions,IReadOnlyList<QuestAction> finishedRequirementActions,
        IReadOnlyList<QuestAction> stageEndActions,IReadOnlyList<QuestAction> stageStartActions,string id) 
    {
        this.preRequisites = preReqs;
        this.requirements = reqs;
        this.stageCompletionConditions = conditions;
        this.incrementActions = incrementActions;
        this.finishedRequirementActions = finishedRequirementActions;
        this.stageEndActions = stageEndActions;
        this.stageStartActions = stageStartActions;
        this.QuestStageID = id;

        reqById = new Dictionary<string,ObjectiveStageRequirement>();

        foreach (var req in requirements)
            reqById[req.GetQuestObjectiveStableID()] = req;

    }

    public Dictionary<string, ObjectiveStageRequirement> GetStageRequirements() { 
        return reqById;
    }

    public void SetNextStages(List<QuestStageDetails> nextStages) { 
        this.possibleNextStages = nextStages;
    }

    public bool TryTransition(Dictionary<string,int> progressDict,out QuestStageDetails nextStage) 
    {
        if (possibleNextStages.Count == 0)
        {
            Debug.Log($"stage has ended, I am going to fire {stageEndActions.Count}");
            StageCompleteEvent?.Invoke(stageEndActions);
            nextStage = null;
            return false;
        }
        else {
            foreach (var stage in possibleNextStages)
            {
                if (stage.CheckPreReqs(progressDict))
                {
                    Debug.Log($"stage has ended, I am going to fire {stageEndActions.Count}");
                    StageCompleteEvent?.Invoke(stageEndActions);
                    Debug.Log("transitioning stage");
                    nextStage = stage;
                    return true;
                }
            }
            // cant transition -- no event
            nextStage = null;
            return false;
        }
    }



    public void EnterStage() {
        Debug.Log("I have entered this stage");
        StageStartedEvent?.Invoke(stageStartActions);
    }

    public void RequestIncrementProgress(string id,int count,Dictionary<string,int> progressDict) {

        

        
        if (!complete) {
            foreach (var requirement in requirements)
            {
                if (progressDict.TryGetValue(id,out var progress) && requirement.GetQuestObjectiveStableID() == id)
                {
                    if (progress < requirement.GetMaxProgressCount()) {
                        progressDict[id] += count;
                        reqById[id].TryIncrementProgress(id,count);
                        
                        RequirementIncrementedEvent?.Invoke(incrementActions);
                        Debug.Log($"incremented requirement: {id}");
                        if (requirement.CheckCompletion(progressDict))
                        {
                            Debug.Log($"completed requirement: {id}");
                            RequirementCompleteEvent?.Invoke(finishedRequirementActions);
                        }
                    }
                }
            }
        }
        
    }

    public bool CheckPreReqs(Dictionary<string,int> progressDict) {
        foreach (var preRequisite in preRequisites) {
            if (!preRequisite.CheckCompletion(progressDict)) { 
                return false;
            }
        }
        return true;
    }

    public bool CheckStageCompletion(Dictionary<string, int> progressDict) {
        if (complete == true) return true;

        

        foreach (var conditionGroup in stageCompletionConditions)
        {
            bool groupSatisfied = true;

            foreach (var condReq in conditionGroup)
            {
                var id = condReq.GetQuestObjectiveStableID();

                // must exist and be complete
                if (!reqById.TryGetValue(id, out var runtimeReq) || !runtimeReq.CheckCompletion(progressDict))
                {
                    groupSatisfied = false;
                    break; // fail this group; try next group
                }
            }

            if (groupSatisfied)
            {
                complete = true;
                return true;
            }
        }
        return false;

    */

        /*

        foreach (var requirement in requirements) {
            if (!requirement.CheckCompletion(progressDict)) {
                return false;
            }
        }
        complete = true;
        return true;*/
    //}



}
