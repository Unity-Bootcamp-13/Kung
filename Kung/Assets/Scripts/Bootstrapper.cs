using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private InventoryServiceLocatorSO _inventoryServiceLocator;

    void Start()
    {
        _inventoryServiceLocator.Bootstrap();        
    }
}
