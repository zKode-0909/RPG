using System.Collections.Generic;
using UnityEngine;

public static class QuestActionHandler
{
    public static void HandleActions(IReadOnlyList<QuestAction> actions) {
        if (actions != null) {
            foreach (var action in actions)
            {

                action.Execute();
            }
        }
        
    }
}
