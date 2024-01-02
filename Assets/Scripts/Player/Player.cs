using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Stat NormalStat;
    [SerializeField] private Stat AddStat;
    [SerializeField] private Stat CurseStat;

    [Header("Rhythm")] 
    [SerializeField] private int RhythmCount = 0;

    
    private int _gold = 0;

    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
        }
    }

    public void GetGold(int value)
    {
        _gold += value;
    }

    public bool isUseGold(int value)
    {
        if (_gold - value >= 0)
        {
            return true;
        }

        return false;
    }

    public void UseingGold(int value)
    {
        _gold -= value;
    }
    
    private Inventory _inven;

    public Inventory Inven
    {
        get
        {
            if (_inven == null)
                _inven = GetComponent<Inventory>();
            
            Debug.LogWarning(_inven);
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
    
    private PlayerAttack _plATK;

    public PlayerAttack PlayerAttack
    {
        get
        {
            if (_plATK == null)
                _plATK = GetComponent<PlayerAttack>();
            return _plATK;
        }
    }
    
    private void InitSetting()
    {
        //Inven.ReturnValue(ref AddStat);
    }

    public void GetItem(ItemSO _so)
    {
        Inven.UseItem(ref NormalStat, ref AddStat,  _so);
    }
    
    public void RefreshStat()
	{
        playerCtrl.SetStat(PlayerStat);
	}

    public Stat PlayerStat
    {
        get
        {
            return NormalStat + AddStat + CurseStat;
        }
    }

    public void CurseStatAdd(Stat cs)
    {
        CurseStat += cs;
    }

    public void ModifyHPPlus(int value)
    {
        NormalStat.HP += value;
    }

    public void ModifyMaxHPPlus(int value)
    {
        NormalStat.MaxHP += value;
        if (value > 0)
        {
            NormalStat.HP += value;
        }
    }
    
    public void ModifyATKPlus(int value)
    {
        NormalStat.ATK += value;
    }

    public void ModifySpeedPlus(int value)
    {
        NormalStat.SPEED += value;
    }
    
}
