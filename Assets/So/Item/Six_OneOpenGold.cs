using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Item/6_SixOneOpenGold")]
public class Six_OneOpenGold : ItemSO
{
    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        base.AttackInvoke(_obj, dec);
        GameManager.Instance.player.GetGold(1);
    }
}
