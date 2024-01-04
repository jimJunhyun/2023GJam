using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapGimik/Bless")]
public class BlessingRoomSO : MapGimikSO
{
    public override string RoomInit()
    {
        _room = RoomType.Blessing;
        GameManager.Instance.curRoom.SetClearState();

        int Randoms = UnityEngine.Random.Range(0, 5);

        if (Randoms == 0)
        {
            float gold = Random.Range(0.00f, 100.0f);
            if(gold < 2.5f)
            {
                GameManager.Instance.player.GetGold(6);
                return "축복 : 골드 6원 획득";
            }
            else if (gold < 7.5f)
            {
                GameManager.Instance.player.GetGold(5);
                return "축복 : 골드 5원 획득";
            }
            else if (gold < 15f)
            {
                GameManager.Instance.player.GetGold(4);
                return "축복 : 골드 4원 획득";
            }
            else if (gold < 30.0f)
            {
                GameManager.Instance.player.GetGold(3);
                return "축복 : 골드 3원 획득";
            }
            else if (gold < 50.0f)
            {
                GameManager.Instance.player.GetGold(1);
                return "축복 : 골드 1원 획득";
            }
            else if (gold >= 50.0f)
            {
                GameManager.Instance.player.GetGold(2);
                return "축복 : 골드 2원 획득";
            }
        }
        else if (Randoms == 1)
        {
            // 저주 제거
            GameManager.Instance.player.Inven.ResetCurse();
            return "축복 : 정화";
        }
        else if (Randoms == 2)
        {
            GameManager.Instance.player.ModifyHPPlus(999);
            return "축복 : 체력회복";
        }
        else if (Randoms == 3)
        {
            int rend = Random.Range(0, 4);
            ItemSO _items = new ItemSO();
            _items._itemtype = ItemType.Infinity;

            if (rend == 0)
            {
                _items.AddStat.AddHP = 1;
                GameManager.Instance.player.GetItem(_items);
                return "축복 : 체력증가 1";
            }
            else if (rend == 1)
            {
                _items.AddStat.AddATK = 30;
                GameManager.Instance.player.GetItem(_items);
                return "축복 : 공격력증가 30";
            }
            else if (rend == 1)
            {
                _items.AddStat.AddSpeed = 1;
                GameManager.Instance.player.GetItem(_items);
                return "축복 : 스피드증가 1";
            }
            

        }
        else if (Randoms == 4)
        {
            GameManager.Instance.player.ModifyMaxHPPlus(2);
            return "축복 : 체력증가 2";
        }
        
        return "축복 : 예외";
    }
}
