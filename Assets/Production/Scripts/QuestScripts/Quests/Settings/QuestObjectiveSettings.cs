using UnityEngine;

[CreateAssetMenu(fileName = "QuestObjectiveSettings", menuName = "Quests/QuestObjectiveSettings")]
public class QuestObjectiveSettings : ScriptableObject
{
    [SerializeField] string objectiveID;
    [SerializeField] int maxProgress;

    public string ObjectiveID => objectiveID;
    public int MaxProgress => maxProgress;
}
