using UnityEngine;

public class SayQuestAction : QuestAction
{
    string wordsToSay;

    public SayQuestAction(string wordsToSay)
    {
        this.wordsToSay = wordsToSay;
    }

    public override void Execute()
    {
        Debug.Log(wordsToSay);
    }
}
