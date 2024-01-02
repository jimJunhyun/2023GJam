using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum ACTIONType
{
    Node,
    Hit
}

public enum ItemType
{
    none,
    Infinity,
    Equipment,
}

public class ItemSO : ScriptableObject
{
    //[Header("BPM = 0 ( default )")] 
    //public int SetBPM = 0;
    //public int AddBPM = 0;

    [Header("ItemInfo")] 
    public ACTIONType _type;
    public ItemType _itemtype;
    public int ExpensiveMoney = 0;

    [FormerlySerializedAs("AddRule")] [Header("AddRule = 1 ( default )")] 
    public int AddBeat = 1;

    [Header("Stat")] 
    public AddStat AddStat;
    
    public int RhythmPassive = 0;

    public virtual void ItemInvoke()
    {
        
    }
}

[System.Serializable]
public class AddStat
{
    public int RegenHP = 0;
    public int AddHP = 0;
    public int AddATK = 0;
    public float AddSpeed = 0;
    public int AddplayerSize = 0;
}