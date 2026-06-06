using UnityEngine;

[System.Serializable]
public class QuestStageRequirementContext
{
    [SerializeField] string objectiveID;
    [SerializeField] int progress;

    public string ObjectiveID => objectiveID;
    public int Progress => progress;

    public QuestStageRequirementContext(string id, int prog)
    {
        this.objectiveID = id;
        this.progress = prog;
    }
}
