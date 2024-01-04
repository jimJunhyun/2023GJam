using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapGimik/Rest")]
public class RestRoomSO : MapGimikSO
{
    /// <summary>
    ///
    /// - 1칸 / 20%
    /// - 2칸 / 50%
    /// - 3칸 / 15%
    /// - 4칸 / 7.5%
    /// - 5칸 / 5%
    /// - 6칸 / 2.5%
    ///
    ///
    /// 
    /// </summary>
    public override string RoomInit()
    {
        _room = RoomType.Heal;

        float Randoms = Random.Range(0.00f, 100.0f);

        if(Randoms < 2.5f)
        {
            GameManager.Instance.player.ModifyHPPlus(6);
            return "휴식 : 체럭 6 회복";
        }
        else if (Randoms < 7.5f)
        {
            GameManager.Instance.player.ModifyHPPlus(5);
            return "휴식 : 체력 5 회복";
        }
        else if (Randoms < 15f)
        {
            GameManager.Instance.player.ModifyHPPlus(4);
            return "휴식 : 체력 4 회복";
        }
        else if (Randoms < 30.0f)
        {
            GameManager.Instance.player.ModifyHPPlus(3);
            return "휴식 : 체력 3 회복";
        }
        else if (Randoms < 50.0f)
        {
            GameManager.Instance.player.ModifyHPPlus(1);
            return "휴식 : 체력 1 회복";
        }
        else if (Randoms >= 50.0f)
        {
            GameManager.Instance.player.ModifyHPPlus(2);
            return "휴식 : 체력 2 회복";
        }
        
        Debug.Log("대충 회복 시켜줌");

        return "휴식";
    }
}
