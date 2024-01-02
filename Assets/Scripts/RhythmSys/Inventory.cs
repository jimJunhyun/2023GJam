using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [FormerlySerializedAs("_items")] [SerializeField] private ItemSO _itemRule;

    public List<ItemSO> _items = new();
    
    public void SetRuleItem(ItemSO _so)
    {
        _itemRule = _so;
        //objStat.BPM += _so.AddBPM;
    }

    public ItemSO ReturnItemRule()
    {
        return _itemRule;
    }

    public void ReturnValue(ref Stat objStat)
    {
        objStat.Size = 0;
        objStat.MaxHP = 0;
        objStat.HP = 0;
        objStat.ATK = 0;
        objStat.SPEED = 0;
        
        for (int i = 0; i < _items.Count; i++)
        {
            AddStat add = _items[i].AddStat;
            //objStat.HP += add.AddHP;
            //objStat.HP += add.RegenHP;
            objStat.MaxHP += add.AddHP;
            objStat.Size += add.AddplayerSize;
            objStat.ATK += add.AddATK;
            objStat.SPEED += add.AddSpeed;
        }
    }

    public void UseItem(ref Stat objStat, ItemSO _so)
    {
        objStat.MaxHP += _so.AddStat.AddHP;
        objStat.HP += _so.AddStat.RegenHP;
        objStat.Size += _so.AddStat.AddplayerSize;
        objStat.ATK += _so.AddStat.AddATK;
        objStat.SPEED += _so.AddStat.AddSpeed;
    }

    public void RemoveItem(ref Stat objStat, ItemSO _so)
    {
        if (_items.Contains(_so))
        {
            objStat.MaxHP -= _so.AddStat.AddHP; // 어차피 HP 제거
            objStat.Size -= _so.AddStat.AddplayerSize;
            objStat.ATK -= _so.AddStat.AddATK;
            objStat.SPEED -= _so.AddStat.AddSpeed; 
        }
    }
}
