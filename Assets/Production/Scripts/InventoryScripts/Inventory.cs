using UnityEngine;

public class Inventory
{
    int capacity;
    RuntimeItem[] items;
    public string ownerStableID;
    public Inventory(int capacity, string ownerStableID)
    {
        this.capacity = capacity;
        items = new RuntimeItem[capacity];
        this.ownerStableID = ownerStableID;
    }

    public bool TryAddItemToInventory(RuntimeItem item)
    {
        int idx = 0;
        foreach (var i in items)
        {
            if (i == null)
            {
                items[idx] = item;
                if (item != null)
                {
                    Debug.Log($"added {item.ItemName} to inventory");
                }
                else
                {
                    Debug.Log("Added null to inventory");
                }

                return true;
            }
            idx++;
        }

        return false;
    }

    public void SetItemAtIndex(int index, RuntimeItem item)
    {
        items[index] = item;
    }

    public RuntimeItem[] GetItems()
    {
        return items;
    }

    public void SwapItems(int idx1, int idx2)
    {

        var item1 = items[idx1];
        var item2 = items[idx2];
        items[idx1] = item2;
        items[idx2] = item1;


    }
}
