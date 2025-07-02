using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemService
{
    public string GetIconPath(int itemId);
}

class ItemService : IItemService
{
    private readonly string path = "Textures/";

    //10001 ~ 10009 = 광물
    //1000n 이면 광물
    public bool CheakIsMineral(int itemId)
    {
        if (itemId < 1000 || itemId > 1009)
        {
            return false;
        }
        return true;
    }

    public string GetIconPath(int itemId)
    {
        return (path + itemId.ToString());

    }
}
