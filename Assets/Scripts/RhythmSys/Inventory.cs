using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CurseList
{
    MadiSeal,
    PerfectSeal,
}

public enum Blessing
{
    none,
    unblindness,
}

// player.inven --> ReturnItemRule() == ItemSO 물음표 보이는 아이템


public class Inventory : MonoBehaviour
{
    [Header("장착 아이템")]
    [SerializeField] private ItemSO _equipItem;

    [Header("저주")] private List<CurseList> _curse = new();
    
    private Dictionary<ItemSO, int> HitInvoke = new();
    private Dictionary<ItemSO, int> NodeInvoke = new();

    public ItemSO ReturnItemRule()
    {
        if(_equipItem)
            return _equipItem;

        return null;
    }

    public void AddCurse(CurseList en)
    {
        _curse.Add(en);
    }

    public void ResetCurse()
    {
        _curse.Clear();
    }

    public bool FindCurse(CurseList en)
    {
        return _curse.Contains(en);
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
        Debug.Log($"들어옴 {_so}");
        if (_so==null || _so._itemtype == ItemType.Equipment)
        {
            if (_equipItem != null)
            {
                PlayerStat.MaxHP -= _equipItem.AddStat.AddHP;
                //objStat.HP -= _equipItem.AddStat.RegenHP;
                objStat.Size -= _equipItem.AddStat.AddplayerSize;
                objStat.ATK -= _equipItem.AddStat.AddATK;
                objStat.SPEED -= _equipItem.AddStat.AddSpeed;
                objStat.AttackRange -= _equipItem.AddStat.AddRange;

                switch (_equipItem._passive)
                {
                    case Blessing.none:
                        break;
                    case Blessing.unblindness:
                        GameManager.Instance.maps[GameManager.Instance.curStage].HindSight();
                        break;
                }
                
            }
            
            Debug.Log(_so);
            
            _equipItem = _so;

            if (_equipItem != null)
            {
                _equipItem.InitEquiped();
            
                PlayerStat.MaxHP += _equipItem.AddStat.AddHP;
                PlayerStat.HP += _equipItem.AddStat.AddHP;
                objStat.Size += _equipItem.AddStat.AddplayerSize;
                objStat.ATK += _equipItem.AddStat.AddATK;
                objStat.SPEED += _equipItem.AddStat.AddSpeed;
                objStat.AttackRange += _equipItem.AddStat.AddRange;
                
                switch (_equipItem._passive)
                {
                    case Blessing.none:
                        break;
                    case Blessing.unblindness:
                        GameManager.Instance.maps[GameManager.Instance.curStage].ClearSight();
                        break;
                }
                
            }
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
        
        if(_so != null && _so._type != ACTIONType.none)
        {
            switch (_so._type)
            {
                case ACTIONType.Node:
                    NodeInvoke.Add(_so, _so.RhythmPassive);
                    break;
                case ACTIONType.Hit:
                    HitInvoke.Add(_so, _so.RhythmPassive);
                    break;
            }
        }
        
        GameManager.Instance.player.RefreshStat();
    }

    public void HitInvoking()
    {
        List<ItemSO> a = new();
        
        foreach (var VARIABLE in HitInvoke)
        {
            a.Add(VARIABLE.Key);
        }

        if (a.Count > 0)
        {
            for (int i = 0; i < a.Count; i++)
            {
                a[i].ItemInvoke();
                NodeInvoke[a[i]]--;
                if (HitInvoke[a[i]] <= 0)
                {
                    if (a[i] == _equipItem)
                    {
                        GameManager.Instance.player.GetItem(null);
                    }
                
                    HitInvoke.Remove(a[i]);
                }
            }
        }
        

    }

    public void NodeInvoking()
    {
        List<ItemSO> a = new();
        
        foreach (var VARIABLE in NodeInvoke)
        {
            a.Add(VARIABLE.Key);
        }

        if (a.Count > 0)
        {
            for (int i = 0; i < a.Count; i++)  
            {
                a[i].ItemInvoke();
                NodeInvoke[a[i]]--;
                if (NodeInvoke[a[i]] <= 0)
                {
                    if (a[i] == _equipItem)
                    {
                        GameManager.Instance.player.GetItem(null);
                        Debug.Log($"{a[i]} 발동");
                    }
                
                    NodeInvoke.Remove(a[i]);
                }
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
