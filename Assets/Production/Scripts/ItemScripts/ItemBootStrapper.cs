using UnityEngine;

public class ItemBootStrapper
{
    ItemFactory factory;
    ItemDB dB;

    public void BootStrap(ItemDB dB) { 
        dB.BuildItemDB();

        factory = new ItemFactory(dB);
    }
}
