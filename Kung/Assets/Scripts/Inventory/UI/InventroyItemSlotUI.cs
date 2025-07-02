using System;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityText;
   
    public void SetData(InventoryItemSlotUIData data)
    {
        Debug.Log("½ÇÇàµÊ?");
        Debug.Log(data.Quantity.ToString());
        _icon.sprite = data.IconSprite;
        _quantityText.text = data.Quantity.ToString();
    }    
}
