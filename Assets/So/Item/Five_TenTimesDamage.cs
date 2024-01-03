using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/5_TenTimesDamage")]
public class Five_TenTimesDamage : ItemSO
{

    [Header("JustDamage")] [SerializeField] private int dmg = 3;
    public override void ItemInvoke()
    {
        
    }

    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;
        switch (dec)   
        {
            case Detection.Perfect:
                _obj.Damage(_player.ATK *1.5f * dmg, dec);
                break;
            case Detection.Good:
                _obj.Damage(_player.ATK* dmg, dec);
                break;
            case Detection.Bad:
                _obj.Damage(_player.ATK * 0.5f*dmg, dec);
                break;
                                                                    
        }
    }
}
