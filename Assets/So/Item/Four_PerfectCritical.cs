using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/4_PerfectCritical")]
public class Four_PerfectCritical : ItemSO
{

    public override void ItemInvoke()
    {
        base.ItemInvoke();
    }

    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;

        int att = _player.ATK;

        if (BeatUISystem.Instance.IsPerfect)
        {
            att *= 2;
        }
        
        switch (dec)   
        {
            case Detection.Perfect:
                _obj.Damage(att *1.5f);
                break;
            case Detection.Good:
                _obj.Damage(att);
                break;
            case Detection.Bad:
                _obj.Damage(att * 0.5f);
                break;
                                                                    
        }
        

    }
}
