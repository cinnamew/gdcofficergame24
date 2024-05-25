using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInventory : MonoBehaviour
{
    public List<UpgradeItem> upgradeItems = new List<UpgradeItem>();

    // Add item to the inventory
    public void AddItem(UpgradeItem item)
    {
        upgradeItems.Add(item);
    }

    // Remove item from the inventory (most likely wont be used)
    public void RemoveItem(UpgradeItem item)
    {
        upgradeItems.Remove(item);
    }

    // Check if item is in the inventory
    public bool HasItem(UpgradeItem item)
    {
        return upgradeItems.Contains(item);
    }

    public bool HasItemWithName(string name){
        for (int i = 0; i < upgradeItems.Count; i++){
            if (upgradeItems[i].itemName == name){
                return true;
            }
        }
        return false;
    }
}