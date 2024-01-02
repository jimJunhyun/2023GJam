using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/11_SupportKit")]
public class Eleven_SupportKit : ItemSO
{
    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;
        switch (dec)   
        {
            case Detection.Perfect:
                _obj.Damage(_player.ATK *1.5f, Detection.Perfect);
                break;
            case Detection.Good:
                _obj.Damage(_player.ATK *1.5f, Detection.Perfect);
                break;
            case Detection.Bad:
                _obj.Damage(_player.ATK, Detection.Good);
                break;
                                                                    
        }
    }
}
