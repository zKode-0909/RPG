using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestGiverDB", menuName = "Quests/QuestGiverDB")]
public class QuestGiverDB : ScriptableObject
{
    [SerializeField] List<QuestGiverSettings> QuestGivers = new();

    public Dictionary<int, QuestGiverSettings> QuestGiversByID { get; private set; }

    public void BuildQuestGiverDB()
    {

        QuestGiversByID = new Dictionary<int, QuestGiverSettings>(QuestGivers.Count);

        foreach (var QuestGiver in QuestGivers)
        {
            if (QuestGiver == null) continue;

            if (QuestGiversByID.ContainsKey(QuestGiver.GiverID))
                Debug.LogError($"Duplicate QuestId: {QuestGiver.GiverID}");
            else
                QuestGiversByID.Add(QuestGiver.GiverID, QuestGiver);
        }
    }

    public bool TryGetQuestGiverDef(int giverID, out QuestGiverSettings questGiver)
    {
        if (QuestGiversByID == null)
        {
            BuildQuestGiverDB();
        }
        if (QuestGiversByID.TryGetValue(giverID, out var q))
        {
            questGiver = q;
            return true;
        }
        else
        {
            questGiver = null;
            return false;
        }
    }
}
