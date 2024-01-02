using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/5_TenTimesDamage")]
public class Five_TenTimesDamage : ItemSO
{

    [Header("JustDamage")] [SerializeField] private int dmg = 3;
    public override void ItemInvoke()
    {
        GameManager.Instance.player.PlayerAttack.JustDamage(dmg);       
    }

    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        base.AttackInvoke(_obj,dec);
    }
}
