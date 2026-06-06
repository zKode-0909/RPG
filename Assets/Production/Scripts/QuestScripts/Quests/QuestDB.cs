
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDB", menuName = "Quests/QuestDB")]
public class QuestDB : ScriptableObject
{
    [SerializeField] List<QuestSettings> Quests = new();

    Dictionary<int, QuestSettings> QuestsByID;

    public void BuildQuestDB()
    {

        QuestsByID = new Dictionary<int, QuestSettings>(Quests.Count);

        foreach (var Quest in Quests)
        {
            if (Quest == null) continue;

            if (QuestsByID.ContainsKey(Quest.QuestID))
                Debug.LogError($"Duplicate QuestId: {Quest.QuestID}");
            else
                QuestsByID.Add(Quest.QuestID, Quest);
        }
    }

    public bool TryGetQuestDef(int questID, out QuestSettings quest)
    {
        if (QuestsByID == null)
        {
            BuildQuestDB();
        }

        if (QuestsByID.TryGetValue(questID, out var q))
        {
            quest = q;
            return true;
        }
        else
        {
            quest = null;
            return false;
        }
    }
}
