using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    [Header("BPM = 0 ( default )")] 
    public int SetBPM = 0;
    public int AddBPM = 0;

    [Header("AddRule = 1 ( default )")] 
    public int AddRule = 1;

    [Header("Stat")] 
    public AddStat AddStat;
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