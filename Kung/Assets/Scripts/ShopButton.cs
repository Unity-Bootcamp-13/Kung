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
    [SerializeField] private GameObject _shopButtonPrefab;
    [SerializeField] private Image _buttonIcon;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private TextMeshPro _priceText;
    [SerializeField] private RectTransform buttonPar;
    private ShopItem[] _shopItems;
    const string path = "Items/";
    private void Awake()
    {

        // Resources �������� JSON �ҷ�����
        TextAsset jsonFile = Resources.Load<TextAsset>("ShopItems");

        if (jsonFile == null)
        {
            throw new Exception("json����");
        }

        _shopItems = JsonHelper.FromJson<ShopItem>(jsonFile.text);

        for (int i = 0; i < _shopItems.Length; i++)
        {
            int itemIndex = i;
            GameObject go = Instantiate(_shopButtonPrefab, buttonPar);

            go.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(path + _shopItems[i].Id.ToString());
            
            TextMeshProUGUI[] texts = go.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = _shopItems[i].ItemName;
            texts[1].text = _shopItems[i].Price.ToString();
            
            go.GetComponent<Button>().onClick.AddListener(() => OnClickShopItem(itemIndex));
        }
    }

    private void OnClickShopItem(int index)
    {
        if (_shopItems == null || index >= _shopItems.Length)
        {
            throw new IndexOutOfRangeException("�ε��� �ʰ�");

        }
        ShopItem item = _shopItems[index];
        _shop.SetText(item.ItemName, item.Price, item.Discription);
        Debug.Log($"������ �̸� : {item.ItemName}, ������ ���� : {item.Price}");
        
    }
}