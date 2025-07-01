using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Unity �ν����Ϳ��� �� ����ü�� ������ �� �ֵ��� [Serializable] ��Ʈ����Ʈ �߰�
[Serializable]
public class ShopItem
{
    public string Id;
    public string ItemName; // ������ �̸� (��: "���޻���")
    public int Price;       // ������ ���� (��: 1000)
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

        var item = _shopItems[index];
        Debug.Log($"������ �̸� : {item.ItemName}, ������ ���� : {item.Price}");
    }
}