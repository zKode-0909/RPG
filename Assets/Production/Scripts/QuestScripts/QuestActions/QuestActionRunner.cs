using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestActionRunner
{

    public void HandleActions(IReadOnlyList<QuestAction> actions)
    {
        //Debug.Log("running action");
        Debug.Log($"{actions.Count}");
        foreach (var action in actions)
        {
            action.Execute();
        }
    }
}
