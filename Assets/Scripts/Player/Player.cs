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

    [Header("LifeModule")] [SerializeField]
    private Player_LifeObject _life;

    public Player_LifeObject LifeModule
    {
        get
        {
            if (_life == null)
                _life = GetComponent<Player_LifeObject>();
            
            return _life;
            
        }
    }

    private void Awake()
    {
        AddStat.Reset();
        CurseStat.Reset();
        RefreshStat();
    }

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

    private PlayerStatUI _playerStatUI;

    public PlayerStatUI PlayerUI
    {
        get
        {
            if (_playerStatUI == null)
            {
                _playerStatUI = FindObjectOfType<PlayerStatUI>();
            }

            return _playerStatUI;
        }
    }
    
    private void InitSetting()
    {
        //Inven.ReturnValue(ref AddStat);
    }

    public void GetItem(ItemSO _so)
    {
        Inven.UseItem(ref NormalStat, ref AddStat,  _so);
        RefreshStat();
    }
    
    public void RefreshStat()
	{
        playerCtrl.SetStat(PlayerStat);
        
        PlayerUI.InvaligateHP(NormalStat.HP, PlayerStat.HP);
        PlayerUI.InvaligateAttack(PlayerStat.ATK);
        PlayerUI.InvaligateSpeed(PlayerStat.SPEED);
        PlayerUI.InvaligateRange(PlayerStat.AttackRange);
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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    ModifyHPPlus(1);
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    ModifyHPPlus(-1);
        //}
        //
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    ModifyMaxHPPlus(1);
        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    ModifyMaxHPPlus(-1);
        //}
    }

    public void ModifyHPPlus(int value)
    {
        NormalStat.HP += value;
        
        
        if (NormalStat.HP > PlayerStat.MaxHP)
        {
            NormalStat.HP = PlayerStat.MaxHP;
        }
        
        LifeModule.hp = PlayerStat.HP;
        
        PlayerUI.InvaligateHP(PlayerStat.HP, PlayerStat.MaxHP);
    }

    public void ModifyMaxHPPlus(int value, bool _isHealthRegen = false)
    {
        NormalStat.MaxHP += value;
        if (NormalStat.MaxHP > 16)
        {
            NormalStat.MaxHP = 16;
        }
        LifeModule.maxHp = PlayerStat.MaxHP;

        if (NormalStat.HP > NormalStat.MaxHP)
        {
            NormalStat.HP = NormalStat.MaxHP;
            LifeModule.hp = NormalStat.MaxHP;
        }
        
        if (value > 0 && _isHealthRegen)
        {
            NormalStat.HP += value;
        }

        LifeModule.hp = PlayerStat.HP;
        LifeModule.DieCheck();
        
        PlayerUI.InvaligateHP(PlayerStat.HP, PlayerStat.MaxHP);
    }
    
    public void ModifyATKPlus(int value)
    {
        NormalStat.ATK += value;
        PlayerUI.InvaligateAttack(PlayerStat.ATK);
        
    }

    public void ModifySpeedPlus(float value)
    {
        NormalStat.SPEED += value;
        PlayerUI.InvaligateSpeed(PlayerStat.SPEED);
    }

    public void ModifyRangePlus(float value)
    {
        NormalStat.AttackRange += value;
        PlayerUI.InvaligateRange(PlayerStat.AttackRange);
    }
    
}
