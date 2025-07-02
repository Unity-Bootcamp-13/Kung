using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopText : MonoBehaviour
{
    [Header("�޽��� �ڽ����� ����� ���� UI ����")]
    [SerializeField] Text _itemName;
    [SerializeField] Text _itemDiscription;
    [SerializeField] TextMeshProUGUI _itemPrice;

    [Header("�� �г� ����")]
    [SerializeField] GameObject _shopTextPanel;
    [SerializeField] GameObject _shopNotEnoughTextPanel;
    private Gold playerGold;
    private int _price;
    private void Start()
    {
        playerGold = Gold.New(10000);

    }
    public void SetText(string name, int price, string dis)
    {
        _price = price;
        _itemName.text = name;
        _itemPrice.text = price.ToString();
        _itemDiscription.text = dis;
        _shopTextPanel.SetActive(true);
    }

    public void OnClickYesButton()
    {
        if (playerGold.IsEnough(_price))
        {
            playerGold = playerGold.RemoveGold(_price);


        }
        else
        {
            SetNotEnoughText();

        }
    }

    public void SetNotEnoughText()
    {
        _shopNotEnoughTextPanel.SetActive(true);

    }

    
}
