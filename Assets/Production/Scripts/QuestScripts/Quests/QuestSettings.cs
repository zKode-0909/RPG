using UnityEngine;

[CreateAssetMenu(fileName = "QuestSettings", menuName = "Quests/QuestSettings")]
public class QuestSettings : ScriptableObject
{
    [SerializeField] int questID;
    [SerializeField] string questName;
    [SerializeField] string questDesc;
    [SerializeField] QuestStageSettings stages;
    [SerializeField] QuestRewardSettings rewardSettings;



    public int QuestID => questID;
    public string QuestName => questName;
    public string QuestDesc => questDesc;

    public Quest BuildRuntimeQuest() {
        return new Quest(questName, questID, stages.BuildStages(),rewardSettings.BuildRuntimeQuestReward());
    }
}
