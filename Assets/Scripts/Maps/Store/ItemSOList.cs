using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSOList")]
public class ItemSOList : ScriptableObject
{
    public List<ItemSO> _list = new();
}