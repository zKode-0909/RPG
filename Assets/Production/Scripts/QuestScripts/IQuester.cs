using System;
using UnityEngine;

public interface IQuester
{
    int EntityID { get; }

    public event Action<QuestIncrementEvent> QuestIncrementEvent;
}
