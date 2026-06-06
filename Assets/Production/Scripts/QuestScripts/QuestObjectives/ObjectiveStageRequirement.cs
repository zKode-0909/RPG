using System.Collections.Generic;
using UnityEngine;


public class ObjectiveStageRequirement
{
    int maxProgessCount;
    int currentProgress;
    string questObjectiveStableID;
    //IReadOnlyList<QuestAction> objectiveCompleteActions;
    //IReadOnlyList<QuestAction> incrementActions;
    public string ObjectiveStableID => questObjectiveStableID;

    public ObjectiveStageRequirement(int maxProgessCount, int currProgress, string questObjectiveStableID/*,
        IReadOnlyList<QuestAction> objectiveCompleteActions, IReadOnlyList<QuestAction> incrementActions*/)
    {
        this.maxProgessCount = maxProgessCount;
        this.currentProgress = 0;
        this.questObjectiveStableID = questObjectiveStableID;
        /*this.objectiveCompleteActions = objectiveCompleteActions;
        this.incrementActions = incrementActions;*/
    }

    public void SetCurrentProgress(int progress) { 
        currentProgress = progress;
    }

    public string GetQuestObjectiveStableID() {
        return questObjectiveStableID;
    }

    public int GetMaxProgressCount() { 
        return maxProgessCount;
    }

    public int GetCurrentProgress() {
        return currentProgress;
    }

    public bool CheckCompletion() {
        Debug.Log($"Completion check is {currentProgress >= maxProgessCount}");
        return currentProgress >= maxProgessCount;
    }

    public bool TryIncrementProgress(string id,int count) {
        if (currentProgress < maxProgessCount && questObjectiveStableID == id) {
            currentProgress += count;

            return true;
        } 

        return false;

    }

}
