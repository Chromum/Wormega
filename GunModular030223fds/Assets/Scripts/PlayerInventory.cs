using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<Item,StoredItem> Items = new Dictionary<Item,StoredItem>();
    public PlayerStatsManager statsManager;
    public Gun Gun;
    public void AddNewItem(Item i) 
    {
        if(Items.ContainsKey(i))
        {
            Items[i].Quantity += 1;
        }
        else
        {
            Items.Add(i, new StoredItem { Item = i, Quantity = 1 });
        }
        statsManager.AddStats(i.ItemStats);
    }
    public void RemoveItem(Item i) 
    {
        if (Items.ContainsKey(i))
        {
            Items[i].Quantity -= 1;
        }
        statsManager.RemoveStats(i.ItemStats);
    }
}
[System.Serializable]
public class StoredItem 
{
    public Item Item;
    public int Quantity = 1;
}


