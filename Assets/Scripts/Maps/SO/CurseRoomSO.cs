using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapGimik/Curse")]
public class CurseRoomSO : MapGimikSO
{

    public override string RoomInit()
    {
        _room = RoomType.Curse;
        
        int Randoms = UnityEngine.Random.Range(0, 6);
        
        if (Randoms == 0)
        {
            float gold = Random.Range(0.00f, 100.0f);
            if(gold < 2.5f)
            {
                GameManager.Instance.player.Gold = 0;
                return "저주 : 골드 전부 소모";
            }
            else if (gold < 7.5f)
            {
                GameManager.Instance.player.Gold = (GameManager.Instance.player.Gold * 1) / 4;
                return "저주 : 골드 3/4 소모";
            }
            else if (gold < 15f)
            {
                GameManager.Instance.player.Gold = (GameManager.Instance.player.Gold * 2) / 4;
                return "저주 : 골드 1/2 소모";
            }
            else if (gold < 30.0f)
            {
                GameManager.Instance.player.Gold = (GameManager.Instance.player.Gold * 3) / 4;
                return "저주 : 골드 1/4 소모";
            }
            else if (gold < 50.0f)
            {
                GameManager.Instance.player.Gold -= 10;
                return "저주 : 골드 10 소모";
            }
            else if (gold >= 50.0f)
            {
                GameManager.Instance.player.Gold -= 1;
                return "저주 : 골드 1 소모";
            }
        }
        else if (Randoms == 1)
        {
            GameManager.Instance.player.ModifyHPPlus(-1);
            return "저주 : 체력 1 감소";
        }
        else if (Randoms == 2)
        {
            GameManager.Instance.player.Gold -= 1;
            return "저주 : 골드 1 소모";
        }
        else if (Randoms == 3)
        {
            GameManager.Instance.player.ModifyMaxHPPlus(-1);
            return "저주 : 영구 최대 체력 1 감소";
        }
        else if (Randoms == 4)
        {
            GameManager.Instance.player.GetItem(null);
            return "저주 : 아이템 소실";
        }
        else if (Randoms == 5)
        {
            Stat ss = new Stat();
            ss.Reset();
            
            int randoms = Random.Range(0, 8);
            if (randoms == 0)
            {
                ss.SPEED = -1;
                GameManager.Instance.player.CurseStatAdd(ss);
                return "저주 : 스피드 감소 1";
            }
            else if (randoms == 1)
            {
                ss.MaxHP = -1;
                GameManager.Instance.player.CurseStatAdd(ss);
                return "저주 : 최대체력 1 감소";
            }
            else if (randoms == 2)
            {
                ss.ATK = -1;
                GameManager.Instance.player.CurseStatAdd(ss);
                return "저주 : 공격력 1 감소";
            }
            else if (randoms == 3)
            {
                GameManager.Instance.player.ModifyATKPlus(-1);

                return "저주 : 영구 공격력 1 감소";
            }
            else if (randoms == 4)
            {
                GameManager.Instance.player.ModifySpeedPlus(-1);
                return "저주 : 영구 스피드 1 감소";
            }
            else if (randoms == 5)
            {
                GameManager.Instance.player.ModifyMaxHPPlus(-1);
                return "저주 : 영구 최대 체력 1 감소";
            }
            else if (randoms == 6)
            {
                GameManager.Instance.player.Inven.AddCurse(CurseList.MadiSeal);
                return "저주 : 눈가리게 ( 마디중 하나 가림 )";
            }
            else if (randoms == 7)
            {
                GameManager.Instance.player.Inven.AddCurse(CurseList.PerfectSeal);
                return "저주 : 퍼팩트 금지";
            }

        }

        return "저주 :";
    }
}
