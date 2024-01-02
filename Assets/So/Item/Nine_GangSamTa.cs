using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/9_GangSamTa")]
public class Nine_GangSamTa : ItemSO
{

    [Header("Justdamage")][SerializeField] int dmg =2;
    private int value = 1;
    
    public override void InitEquiped()
    {
        value = 1;
    }

    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        base.AttackInvoke(_obj, dec);
        value++;

        if (value >= 3)
        {
            value = 1;
            _obj.Damage(dmg, dec);
        }
    }
}
