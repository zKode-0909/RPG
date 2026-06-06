
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestGiverSettings", menuName = "Quests/QuestGiverSettings")]
public class QuestGiverSettings : ScriptableObject
{
    [SerializeField] int giverID;
    [SerializeField] string giverName;
    [SerializeField] List<QuestSettings> quests;

    public int GiverID => giverID;
    public string GiverName => giverName;
    public List<QuestSettings> Quests => quests;


}
