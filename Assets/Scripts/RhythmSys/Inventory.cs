using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSO _items;


    public void SetItem(ItemSO _so)
    {
        _items = _so;
    }
    
    
}
