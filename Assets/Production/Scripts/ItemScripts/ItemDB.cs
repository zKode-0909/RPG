using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName = "Items/ItemDB")]
public class ItemDB : ScriptableObject
{
    [SerializeField] List<ItemSettings> Items = new();

    Dictionary<int, ItemSettings> ItemsByID;

    public void BuildItemDB()
    {

        ItemsByID = new Dictionary<int, ItemSettings>(Items.Count);

        foreach (var Item in Items)
        {
            if (Item == null) continue;

            if (ItemsByID.ContainsKey(Item.ItemID))
                Debug.LogError($"Duplicate ItemId: {Item.ItemID}");
            else
                ItemsByID.Add(Item.ItemID, Item);
        }
    }

    public bool TryGetItemDef(int itemID, out ItemSettings item)
    {
        if (ItemsByID == null)
        {
            BuildItemDB();
        }

        if (ItemsByID.TryGetValue(itemID, out var i))
        {
            item = i;
            return true;
        }
        else
        {
            item = null;
            return false;
        }
    }
}
