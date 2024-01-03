using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LifeObject : LifeObject
{
    public override void Damage(float amt, Detection _dec = Detection.none)
    {
        if (!isImmune)
        {
            GameManager.Instance.player.ModifyHPPlus((int)(-amt));
            
            
            SetImmuneFor(0.3f);
            DieCheck();
        }
    }

    public void DieCheck()
    {
        if (hp <= 0)
        {
            OnDead();
        }
    }
}
