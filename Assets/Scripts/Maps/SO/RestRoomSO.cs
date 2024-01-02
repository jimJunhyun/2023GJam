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
    public override void RoomInit()
    {
        _room = RoomType.Heal;

        float Randoms = Random.Range(0.00f, 100.0f);

        if(Randoms < 2.5f)
        {
            GameManager.Instance.player.ModifyHPPlus(6);
        }
        else if (Randoms < 7.5f)
        {
            GameManager.Instance.player.ModifyHPPlus(5);
        }
        else if (Randoms < 15f)
        {
            GameManager.Instance.player.ModifyHPPlus(4);
        }
        else if (Randoms < 30.0f)
        {
            GameManager.Instance.player.ModifyHPPlus(3);
        }
        else if (Randoms < 50.0f)
        {
            GameManager.Instance.player.ModifyHPPlus(1);
        }
        else if (Randoms >= 50.0f)
        {
            GameManager.Instance.player.ModifyHPPlus(2);
        }
        
        Debug.Log("대충 회복 시켜줌");
    }
}
