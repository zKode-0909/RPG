using System.Collections.Generic;

public sealed class QuestBuildContext
{
    public readonly Dictionary<QuestStageDetailsSettings, QuestStageDetails> StageCache = new();
    public readonly HashSet<QuestStageDetailsSettings> Visiting = new(); // cycle detection
}