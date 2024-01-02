using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapGimik/Bless")]
public class BlessingRoomSO : MapGimikSO
{
    public override void RoomInit()
    {
        _room = RoomType.Blessing;

        int Randoms = UnityEngine.Random.Range(0, 4);

        if (Randoms == 0)
        {
            float gold = Random.Range(0.00f, 100.0f);
            if(gold < 2.5f)
            {
                GameManager.Instance.player.GetGold(6);
            }
            else if (gold < 7.5f)
            {
                GameManager.Instance.player.GetGold(5);
            }
            else if (gold < 15f)
            {
                GameManager.Instance.player.GetGold(4);
            }
            else if (gold < 30.0f)
            {
                GameManager.Instance.player.GetGold(3);
            }
            else if (gold < 50.0f)
            {
                GameManager.Instance.player.GetGold(1);
            }
            else if (gold >= 50.0f)
            {
                GameManager.Instance.player.GetGold(2);
            }
        }
        else if (Randoms == 1)
        {
            // 저주 제거
        }
        else if (Randoms == 2)
        {
            GameManager.Instance.player.ModifyHPPlus(999);
        }
        else if (Randoms == 3)
        {
            int rend = Random.Range(0, 4);
            ItemSO _items = new ItemSO();
            _items._itemtype = ItemType.Infinity;
                
            if (rend == 0)
                _items.AddStat.AddHP = 1;
            else if (rend == 1)
                _items.AddStat.AddATK = 1;
            else if (rend == 1)
                _items.AddStat.AddSpeed = 1;
            
            GameManager.Instance.player.GetItem(_items);
        }
        
       
    }
}
