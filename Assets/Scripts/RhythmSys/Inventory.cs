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
    }

    public Stat ReturnValue(Stat objStat)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            AddStat add = _items[i].AddStat;
            objStat.MaxHP += add.AddHP;
            objStat.HP += add.AddHP;
            
            objStat.HP += add.RegenHP;
            objStat.Size += add.AddplayerSize;
            objStat.ATK += add.AddATK;
            objStat.SPEED += add.AddSpeed;
        }

        return objStat;
    }

    public Stat UseItem(Stat objStat, ItemSO _so)
    {
        objStat.MaxHP += _so.AddStat.AddHP;
        objStat.HP += _so.AddStat.RegenHP;
        objStat.Size += _so.AddStat.AddplayerSize;
        objStat.ATK += _so.AddStat.AddATK;
        objStat.SPEED += _so.AddStat.AddSpeed;

        if (_so.SetBPM > 0)
        {
            objStat.BPM = _so.SetBPM;
        }
        else
        {
            objStat.BPM = (int)BeatSystem.Instance.ReturnBPM;
        }
        objStat.BPM += _so.AddBPM;

        return objStat;
    }

    public Stat RemoveItem(Stat objStat, ItemSO _so)
    {
        if (_items.Contains(_so))
        {
            objStat.MaxHP -= _so.AddStat.AddHP;
            objStat.Size -= _so.AddStat.AddplayerSize;
            objStat.ATK -= _so.AddStat.AddATK;
            objStat.SPEED -= _so.AddStat.AddSpeed;
            
            
            objStat.BPM = (int)BeatSystem.Instance.ReturnBPM;
        }

        return objStat;
    }
}
