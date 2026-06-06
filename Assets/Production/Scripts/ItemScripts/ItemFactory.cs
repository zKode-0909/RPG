using UnityEngine;

public class ItemFactory
{
    ItemDB itemDatabase;

    public ItemFactory(ItemDB itemDatabase)
    {
        this.itemDatabase = itemDatabase;
    }

    public bool TryCreateItemFromID(int id,out RuntimeItem runtimeItem) {
        if (itemDatabase.TryGetItemDef(id, out var itemSettings)) {
            runtimeItem = new RuntimeItem(itemSettings.ItemID, itemSettings.name, itemSettings.MaxStack, itemSettings.ItemIcon, itemSettings.ItemWeight);
            return true;
        }

        runtimeItem = null;
        return false;
    }
}
