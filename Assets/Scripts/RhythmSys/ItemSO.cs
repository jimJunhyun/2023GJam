using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum ACTIONType
{
    none,
    Node,
    Hit
}

public enum ItemType
{
    none,
    Infinity,
    Equipment,
}

[CreateAssetMenu(menuName = "SO/Item/Normal")]
public class ItemSO : ScriptableObject
{
    //[Header("BPM = 0 ( default )")] 
    //public int SetBPM = 0;
    //public int AddBPM = 0;

    [Header("ItemInfo")] 
    public ACTIONType _type;
    public ItemType _itemtype;
    public int ExpensiveMoney = 0;

    [FormerlySerializedAs("AddRule")] [Header("AddRule = 0 ( default )")] 
    public int AddBeat = 0;

    public int RemoveBeat = 0;

    [Header("Stat")] 
    public AddStat AddStat;
    
    public int RhythmPassive = 0;

    public virtual void ItemInvoke()
    {
        
    }

    public virtual void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;
        switch (dec)   
        {
            case Detection.Perfect:
                _obj.Damage(_player.ATK *1.5f);
                break;
            case Detection.Good:
                _obj.Damage(_player.ATK);
                break;
            case Detection.Bad:
                _obj.Damage(_player.ATK * 0.5f);
                break;
                                                                    
        }
    }
}

[System.Serializable]
public class AddStat
{
    //public int RegenHP = 0;
    public int AddHP = 0;
    public int AddATK = 0;
    public float AddSpeed = 0;
    public int AddplayerSize = 0;
    public int AddRange = 0;
}