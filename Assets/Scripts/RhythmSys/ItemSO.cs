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

    [Header("Info")] 
    public string ItemName;
    public Sprite _sprite;
    [TextArea] public string ItemDescription;
    public int ExpensiveMoney = 0;

    [Header("ItemType")] 
    public ACTIONType _type = ACTIONType.none;
    public ItemType _itemtype = ItemType.none;

    [FormerlySerializedAs("AddRule")] [Header("AddRule = 0 ( default )")] 
    public int AddBeat = 0;

    public int RemoveBeat = 0;

    [Header("Stat")] 
    public AddStat AddStat = new();
    
    public int RhythmPassive = 0;

    [Header("Blessing")] public Blessing _passive = Blessing.none;

    public virtual void ItemInvoke()
    {
        
    }

    public virtual void InitEquiped()
    {
        
    }
    

    public virtual void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;
        switch (dec)   
        {
            case Detection.Perfect:
                _obj.Damage(_player.ATK *1.5f, dec);
                break;
            case Detection.Good:
                _obj.Damage(_player.ATK, dec);
                break;
            case Detection.Bad:
                _obj.Damage(_player.ATK * 0.5f, dec);
                break;
                                                                    
        }
        Debug.Log($"{dec} !!!!!! 판정!!!");
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