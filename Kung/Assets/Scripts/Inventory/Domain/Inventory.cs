using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Inventory
{
    List<UserInventoryItem> items = new List<UserInventoryItem>();

    public bool IsFull => items.Count >= 25;

    public IReadOnlyList<UserInventoryItem> Items
    {
        get { return items.AsReadOnly(); }
    }

    public void AddItem(UserInventoryItem item)
    {
        Debug.Log("addæ∆¿Ã≈€");
        items.Add(item);
        foreach (var item1 in items)
        {
            Debug.Log(item1.ItemId);
        }
    }
}
