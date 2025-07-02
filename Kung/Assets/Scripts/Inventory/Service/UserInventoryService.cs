using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public interface IUserInventroyService
{
    void AcquireItem(int itemId);

    IReadOnlyList<UserInventoryItemDto> Items { get; }

    bool IsFull();

}


public class UserInventoryService : IUserInventroyService
{
    private readonly Inventory _inventory;
    private readonly ItemService _itemService;

    public UserInventoryService()
    {
        _inventory = new Inventory();
        _itemService = new ItemService();

    }

    public IReadOnlyList<UserInventoryItemDto> Items
    {
        get
        {
            return _inventory.Items
                .Select(item => new UserInventoryItemDto(item))
                .ToList();
        }
    }

    public void AcquireItem(int itemId)
    {
        if (_inventory.IsFull)
        {
            return;
        }

        if (Items.Count >= 25)
        {
            Debug.Log("°¹¼ö ÃÊ°ú");

            return;
        }

        bool isMineral = _itemService.CheakIsMineral(itemId);
        if (isMineral)
        {
            UserInventoryItem? userInventoryItem = _inventory.Items
                .FirstOrDefault(item => item.CanStack(itemId));

            if (userInventoryItem == null)
            {
                UserInventoryItem newItem = UserInventoryItem.Acquire(itemId, isMineral);
                _inventory.AddItem(newItem);
            }
            else
            {
                userInventoryItem.AddQuantity();
            }
        }
    }

    public bool IsFull()
    {
        throw new System.NotImplementedException();
    }
}
