using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [Header("장착 아이템")]
    [SerializeField] private ItemSO _equipItem;

    private Dictionary<Action, int> HitInvoke = new();
    private Dictionary<Action, int> NodeInvoke = new();

    public ItemSO ReturnItemRule()
    {
        return _equipItem;
    }

    //public void ReturnValue(ref Stat objStat)
    //{
    //    objStat.Size = 0;
    //    objStat.MaxHP = 0;
    //    objStat.HP = 0;
    //    objStat.ATK = 0;
    //    objStat.SPEED = 0;
    //    
    //    for (int i = 0; i < _items.Count; i++)
    //    {
    //        AddStat add = _items[i].AddStat;
    //        //objStat.HP += add.AddHP;
    //        //objStat.HP += add.RegenHP;
    //        objStat.MaxHP += add.AddHP;
    //        objStat.Size += add.AddplayerSize;
    //        objStat.ATK += add.AddATK;
    //        objStat.SPEED += add.AddSpeed;
    //    }
    //}

    public void UseItem(ref Stat PlayerStat, ref Stat objStat,  ItemSO _so)
    {
        if (_so._itemtype == ItemType.Equipment)
        {
            if (_equipItem != null)
            {
                objStat.MaxHP -= _equipItem.AddStat.AddHP;
                //objStat.HP -= _equipItem.AddStat.RegenHP;
                objStat.Size -= _equipItem.AddStat.AddplayerSize;
                objStat.ATK -= _equipItem.AddStat.AddATK;
                objStat.SPEED -= _equipItem.AddStat.AddSpeed;
                objStat.AttackRange -= _equipItem.AddStat.AddRange;
            }
            
            _equipItem = _so;
            
            objStat.MaxHP += _equipItem.AddStat.AddHP;
            PlayerStat.HP += _equipItem.AddStat.AddHP;
            objStat.Size += _equipItem.AddStat.AddplayerSize;
            objStat.ATK += _equipItem.AddStat.AddATK;
            objStat.SPEED += _equipItem.AddStat.AddSpeed;
            objStat.AttackRange += _equipItem.AddStat.AddRange;
        }
        else if(_so._itemtype == ItemType.Infinity)
        {
            PlayerStat.MaxHP += _so.AddStat.AddHP;
            PlayerStat.HP += _so.AddStat.AddHP;
            PlayerStat.Size += _so.AddStat.AddplayerSize;
            PlayerStat.ATK += _so.AddStat.AddATK;
            PlayerStat.SPEED += _so.AddStat.AddSpeed;
            PlayerStat.AttackRange += _so.AddStat.AddRange;
        }
        else
        {
            switch (_so._type)
            {
                case ACTIONType.Node:
                    NodeInvoke.Add(_so.ItemInvoke, _so.RhythmPassive);
                    break;
                case ACTIONType.Hit:
                    HitInvoke.Add(_so.ItemInvoke, _so.RhythmPassive);
                    break;
            }
        }
    }

    public void HitInvoking()
    {
        List<Action> a = new();
        
        foreach (var VARIABLE in HitInvoke)
        {
            VARIABLE.Key.Invoke();
            HitInvoke[VARIABLE.Key]--;
            a.Add(VARIABLE.Key);
        }

        for (int i = 0; i < a.Count; i++)
        {
            if (HitInvoke[a[i]] <= 0)
            {
                HitInvoke.Remove(a[i]);
            }
        }
    }

    public void NodeInvoking()
    {
        List<Action> a = new();
        
        foreach (var VARIABLE in NodeInvoke)
        {
            VARIABLE.Key.Invoke();
            NodeInvoke[VARIABLE.Key]--;
            a.Add(VARIABLE.Key);
        }

        for (int i = 0; i < a.Count; i++)
        {
            if (NodeInvoke[a[i]] <= 0)
            {
                NodeInvoke.Remove(a[i]);
            }
        }
    }

    //public void RemoveItem(ref Stat objStat, ItemSO _so)
    //{
    //    if (_items.Contains(_so))
    //    {
    //        objStat.MaxHP -= _so.AddStat.AddHP; // 어차피 HP 제거
    //        objStat.Size -= _so.AddStat.AddplayerSize;
    //        objStat.ATK -= _so.AddStat.AddATK;
    //        objStat.SPEED -= _so.AddStat.AddSpeed; 
    //    }
    //}
}
