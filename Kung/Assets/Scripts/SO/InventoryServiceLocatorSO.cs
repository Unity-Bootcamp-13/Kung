using UnityEngine;

[CreateAssetMenu(fileName = "InventoryServiceLocator", menuName = "Scriptable Objects/InventoryServiceLocator")]
public class InventoryServiceLocatorSO : ScriptableObject
{
    public UserInventoryService Service { get; private set; }

    public void Bootstrap()
    {
        Service = new UserInventoryService();
    }
}
