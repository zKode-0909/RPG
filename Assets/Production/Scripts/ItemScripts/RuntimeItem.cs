using UnityEngine;

public class RuntimeItem
{
    int StableID { get; }
    public string ItemName { get; private set; }
    int maxStack { get; }
    Sprite Icon { get; }
    float weight { get; }

    public RuntimeItem(int id,string name,int maxStack,Sprite icon,float weight) { 
        this.StableID = id;
        this.ItemName = name;
        this.maxStack = maxStack;
        this.Icon = icon;
        this.weight = weight;
    }


    public void HandleLeftClick() {
        Debug.Log($"Left clicked item: {ItemName}");
    }
    public void HandleRightClick() {
        Debug.Log($"Right clicked item: {ItemName}");
    }


}
