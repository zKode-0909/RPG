using Codice.Client.Commands.WkTree;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSettings", menuName = "Scriptable Objects/ItemSettings")]
public class ItemSettings : ScriptableObject
{
    [SerializeField] int itemID;
    [SerializeField] string itemName;
    [SerializeField] int maxStack;
    [SerializeField] Sprite itemIcon;
    [SerializeField] float itemWeight;

    public int ItemID => itemID;
    public string ItemName => itemName;
    public int MaxStack => maxStack;
    public Sprite ItemIcon => itemIcon;
    public float ItemWeight => itemWeight;

}
