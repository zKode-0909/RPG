using Castle.Components.DictionaryAdapter.Xml;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestRewardSettings", menuName = "Quests/QuestRewardSettings")]
public class QuestRewardSettings : ScriptableObject
{
    [SerializeField] int xpRewarded;
    [SerializeField] int copperRewarded;

    public int XP => xpRewarded;
    public int Copper => copperRewarded;

    public QuestReward BuildRuntimeQuestReward() {
        return new QuestReward(xpRewarded, copperRewarded);
    }
}
