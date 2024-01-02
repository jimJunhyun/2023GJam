using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Stat NormalStat;
    [SerializeField] private Stat AddStat;

    private Inventory _inven;

    public Inventory Inven
    {
        get
        {
            if (_inven == null)
                _inven = GetComponent<Inventory>();
            return _inven;
        }
    }

    private PlayerCtrl _plCtr;

    public PlayerCtrl playerCtrl
    {
        get
        {
            if (_plCtr == null)
                _plCtr = GetComponent<PlayerCtrl>();
            return _plCtr;
        }
    }
    
    private void InitSetting()
    {
        Inven.ReturnValue(ref AddStat);
    }

    public void GetItem(ItemSO _so)
    {
        Inven.UseItem(ref AddStat, _so);
    }

    public void GetRuleItem(ItemSO _so)
    {
        Inven.SetRuleItem(_so);
    }

    public Stat PlayerStat
    {
        get
        {
            return NormalStat + AddStat;
        }
    }
    
    
}
