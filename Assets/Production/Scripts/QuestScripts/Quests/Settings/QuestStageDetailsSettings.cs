
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStageSettings", menuName = "Quests/QuestStageDetailsSettings")]
public class QuestStageDetailsSettings : ScriptableObject
{
    [SerializeField] string stageID;
    [SerializeField] List<QuestStageDetailsSettings> potentialNextStages;
    [SerializeField] List<QuestObjectiveSettings> questObjectives;
    [SerializeField] List<string> stagePreReqs;
    [SerializeField] List<QuestAction> stageCompleteActions;

    public string StageID => stageID;
    public List<QuestStageDetailsSettings> PotentialNextStages => potentialNextStages;  
    public List<QuestObjectiveSettings> QuestObjectives => questObjectives;
    public List<string> QuestStagePreReqs => stagePreReqs;
    public List<QuestAction> StageCompleteActions => stageCompleteActions;

    public QuestStageDetails BuildRuntimeQuestStageDetails(Dictionary<string,QuestStageDetails> stages) {
        if (stages.TryGetValue(stageID, out var stage)) {
            return stage;
        }

        var nextRuntimeStages = new List<QuestStageDetails>();

        if (potentialNextStages != null) {
            foreach (var potentialStage in potentialNextStages) {
                nextRuntimeStages.Add(potentialStage.BuildRuntimeQuestStageDetails(stages));
            }
        }

        var runtime = new QuestStageDetails(stageID,
            nextRuntimeStages,
            BuildObjectiveDict(questObjectives),
            stageCompleteActions,
            stagePreReqs
        );

        stages.TryAdd(stageID, runtime );

        return runtime;



    }

    public Dictionary<string, ObjectiveStageRequirement> BuildObjectiveDict(List<QuestObjectiveSettings> objectives) {
        var objectiveDict = new Dictionary<string, ObjectiveStageRequirement>();
        foreach (var objective in objectives) {
            objectiveDict.TryAdd(objective.ObjectiveID,new ObjectiveStageRequirement(objective.MaxProgress, 0, objective.ObjectiveID));
        }

        return objectiveDict;
    }
}
