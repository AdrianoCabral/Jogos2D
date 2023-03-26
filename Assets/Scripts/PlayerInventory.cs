using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public List<AllItems> _inventoryItems = new List<AllItems>(); //Items que o player pegou

    private void Awake()
    {
        Instance = this;
    }

    public void addItem(AllItems item)
    {
        if(!_inventoryItems.Contains(item))
        {
            _inventoryItems.Add(item);
        }
    }

    public void removeItems(AllItems item)
    {
        if (_inventoryItems.Contains(item))
        {
            _inventoryItems.Remove(item);
        }
    }

    public enum AllItems //Todos os items disponiveis 
    {
        Hammer,
        Potion,
        Key
    }
}
