using UnityEngine;

public class TestQuestAction : QuestAction
{
    string wordsToSay;
    public TestQuestAction(string words) {
        wordsToSay = words;
    }

    public override void Execute() {
        Debug.Log(wordsToSay);
    }


}
