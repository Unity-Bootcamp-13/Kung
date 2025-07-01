using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

// Unity �ν����Ϳ��� �� ����ü�� ������ �� �ֵ��� [Serializable] ��Ʈ����Ʈ �߰�
[Serializable]
public class ShopItem
{
    public string Id;
    public string ItemName; // ������ �̸� (��: "���޻���")
    public int Price;       // ������ ���� (��: 1000)
    public string Discription;
}
[Serializable]
public class ShopItemList
{
    public ShopItem[] items;
}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string wrappedJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}



public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button[] _shopButtons;
    [SerializeField] private GameObject TextPanel;
    [SerializeField] private ShopText _shop;
    private ShopItem[] _shopItems;
    private void Awake()
    {

        // Resources �������� JSON �ҷ�����
        TextAsset jsonFile = Resources.Load<TextAsset>("ShopItems");
        if (jsonFile == null)
        {
            Debug.LogError("ShopItems_RootArray.json ������ Resources ������ �����ϴ�.");
            return;
        }

        _shopItems = JsonHelper.FromJson<ShopItem>(jsonFile.text);

        for (int i = 0; i < _shopButtons.Length; i++)
        {
            int itemIndex = i;
            _shopButtons[i].onClick.AddListener(() => OnClickShopItem(itemIndex));
        }
    }

    private void OnClickShopItem(int index)
    {
        if (_shopItems == null || index >= _shopItems.Length)
        {
            Debug.LogWarning("������ ������ �����ϴ�.");
            return;
        }
        ShopItem item = _shopItems[index];
        _shop.SetText(item.ItemName, item.Price, item.Discription);
        Debug.Log($"������ �̸� : {item.ItemName}, ������ ���� : {item.Price}");
        
    }
}