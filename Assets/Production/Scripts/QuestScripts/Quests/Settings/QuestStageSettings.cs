
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStageSettings", menuName = "Quests/QuestStageSettings")]
public class QuestStageSettings : ScriptableObject
{
    [SerializeField] List<QuestStageDetailsSettings> stages;
    [SerializeField] QuestStageDetailsSettings initialStage;

    public List<QuestStageDetailsSettings> StageDetails => stages;
    public QuestStageDetailsSettings InitialStage => initialStage;

    public QuestStages BuildStages() {
        var stageDetails = new Dictionary<string,QuestStageDetails>();

        var root = initialStage.BuildRuntimeQuestStageDetails(stageDetails);

        return new QuestStages(stageDetails,root);

        
    }
    


}
