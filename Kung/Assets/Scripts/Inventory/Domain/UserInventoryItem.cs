using System;
using UnityEngine;
public class UserInventoryItem
{
    public long SerialNumber { get; }
    public int ItemId { get; }
    public int Quantity { get; private set; }
    public bool IsStackable { get; }
    public bool IsFull => IsStackable && Quantity >= 5;

    public static UserInventoryItem Acquire(int itemId, bool isMineral)
    {
        // TODO: 광물인 경우 다르게 처리해야 함
        if (isMineral)
        {

            return new UserInventoryItem(
                serialNumber: DateTime.UtcNow.Ticks,
                itemId: itemId,
                quantity: 1,
                isStackable: true
            );
        }
        else
        {
            return new UserInventoryItem(
                serialNumber: DateTime.UtcNow.Ticks,
                itemId: itemId,
                quantity: 1,
                isStackable: false
            );
        }
            
    }

    private UserInventoryItem(long serialNumber, int itemId, int quantity, bool isStackable)
    {
        Debug.Log("광물 생성됨");
        SerialNumber = serialNumber;
        ItemId = itemId;
        Quantity = quantity;
        IsStackable = isStackable;
    }

    public void AddQuantity()
    {
        Quantity += 1;
    }

    public bool CanStack(int itemId) => itemId == ItemId && !IsFull;
}
